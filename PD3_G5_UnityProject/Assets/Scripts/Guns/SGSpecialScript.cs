using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SGSpecialScript : MonoBehaviour
{
    [SerializeField] GameObject specialBulletPrefab;

    [Header("Forces")]
    [SerializeField] float specialShootForce;
    [SerializeField] float specialUpwardForce;

    [Header("Stats")]
    [SerializeField] float specialCooldownTime;
    private float currentCooldownTime;
    private bool specialInCooldown = false;
    [SerializeField] float specialSpread;
    [SerializeField] float specialTimeBetweenShots;
    [SerializeField] float specialBulletDamage;
    [SerializeField] int specialBulletsPerTap;
    private int baseSpecialBulletsPerTap;

    [Header("Spawn Point")]
    private Camera cam;
    [SerializeField] Transform specialBulletOrigin;

    [Header("Debug")]
    [SerializeField] bool allowInvokeSpecial;

    int specialBulletsShot;

    bool shootingSpecial, readyToShootSpecial, reloading;
    Queue<GameObject> specialBulletPool;
    CooldownScript cooldown;

    private int totalBullets = 0;

    DoubleShotBlessingScript doubleShotBlessing;
    KillEnemyDamageBuffBlessingScript damageBuffBlessing;

    [SerializeField] UnityEvent resetAbilityCooldownEvent;
    [Header("FMOD")]
    public StudioEventEmitter SGShootEmitter;

    // Start is called before the first frame update
    private void Start()
    {
        currentCooldownTime = specialCooldownTime;
        baseSpecialBulletsPerTap = specialBulletsPerTap;
        cooldown = GameObject.Find("CanvasPrefab/Cooldowns").GetComponent<CooldownScript>();
        doubleShotBlessing = GetComponent<DoubleShotBlessingScript>();
        damageBuffBlessing = GetComponent<KillEnemyDamageBuffBlessingScript>();
        cam = GameObject.Find("Player/PitchController/Main Camera").GetComponent<Camera>();
        specialBulletPool = new Queue<GameObject>();
        GameObject specialBullets = new("SG Special Bullets");

        for (int i = 0; i < specialBulletsPerTap + 20; i++)
        {
            GameObject specialBullet = Instantiate(specialBulletPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            specialBullet.SetActive(false);
            specialBulletPool.Enqueue(specialBullet);
            specialBullet.transform.parent = specialBullets.transform;
        }

        readyToShootSpecial = true;
        shootingSpecial = false;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();

        if (specialInCooldown)
        {
            currentCooldownTime -= Time.deltaTime;
        }
        if(currentCooldownTime <= 0)
        {
            specialInCooldown = false;
            currentCooldownTime = specialCooldownTime;
            ResetSpecialShot();
        }
    }

    private void CheckInput()
    {

        shootingSpecial = Input.GetKeyDown(KeyCode.Mouse1);

        //CHECKEAR SI SE ESTA RECARGANDO Y NO PERMITIR UTILIZAR EL ESPECIAL

        if (readyToShootSpecial && shootingSpecial && !reloading && !PlayerStatsScript.instance.isReloading)
        {
            specialBulletsShot = 0;
            ShootSpecial();
            cooldown.StartAbilityCooldown(specialCooldownTime);
            Debug.Log(totalBullets);
            totalBullets  = 0;

        }
    }

    private void ShootSpecial()
    {
        SGShootEmitter.Play();
        readyToShootSpecial = false;

        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        //RaycastHit hitInfo;
        Vector3 hitPoint = r.GetPoint(50);

        //if (Physics.Raycast(r, out hitInfo, maxShootDist, shootingMask))

        if (Physics.Raycast(r, out RaycastHit hitInfo))
        {
            //Crec que a vegades les bales surten rares pq aix� detecta una bala ja disparada.
            if (hitInfo.collider.gameObject.CompareTag("Enemy") || hitInfo.collider.gameObject.CompareTag("Terrain"))
                hitPoint = hitInfo.point;
        }



        Vector3 directionWithoutSpread = hitPoint - specialBulletOrigin.position;

        float xSpread = Random.Range(-specialSpread, +specialSpread);
        float ySpread = Random.Range(-specialSpread, +specialSpread);
        float zSpread = Random.Range(-specialSpread, +specialSpread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentBullet = specialBulletPool.Dequeue();
        currentBullet.SetActive(true);
        currentBullet.transform.position = specialBulletOrigin.position;
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<SGSpecialBulletScript>().SetDamage(specialBulletDamage + PlayerStatsScript.instance.currentDamageMultiplyer);
        if (PlayerStatsScript.instance.killEnemyAbilityCooldownBlessing)
        {
            currentBullet.GetComponent<SGSpecialBulletScript>().weaponScript = this;
        }

        if (PlayerStatsScript.instance.killEnemyDamageBuffBlessing)
        {
            currentBullet.GetComponent<SGSpecialBulletScript>().damageBuffBlessing = damageBuffBlessing;
        }

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * specialShootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * specialUpwardForce, ForceMode.Impulse);

        specialBulletPool.Enqueue(currentBullet);

        specialBulletsShot++;
        totalBullets++;

        if (allowInvokeSpecial)
        {
            specialInCooldown = true;
            allowInvokeSpecial = false;
        }
        if (specialBulletsShot < specialBulletsPerTap)
        {
            Invoke(nameof(ShootSpecial), specialTimeBetweenShots);
        }
        else
        {
            if (doubleShotBlessing.enabled && doubleShotBlessing.DoubleShot() && doubleShotBlessing.canDoubleShot)
            {
                doubleShotBlessing.canDoubleShot = false;
                specialBulletsShot = 0;
                Invoke(nameof(ShootSpecial), 0.5f);
            }
        }
    }

    private void ResetSpecialShot()
    {
        readyToShootSpecial = true;
        allowInvokeSpecial = true;
        doubleShotBlessing.canDoubleShot = true;
    }

    public void SetSBulletsPerTap(int bPerTap)
    {
        specialBulletsPerTap = bPerTap;

        if (bPerTap == 0)
            specialBulletsPerTap = baseSpecialBulletsPerTap;
    }

    public void ResetSpecialCooldown()
    {
        if (specialInCooldown)
        {
            currentCooldownTime = 0;
            resetAbilityCooldownEvent.Invoke();
        }
    }
}
