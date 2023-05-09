using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGSpecialScript : MonoBehaviour
{
    [SerializeField] GameObject specialBulletPrefab;

    [Header("Forces")]
    [SerializeField] float specialShootForce;
    [SerializeField] float specialUpwardForce;

    [Header("Stats")]
    [SerializeField] float specialCooldownTime;
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

    bool cooldownBlessing = false;

    // Start is called before the first frame update
    private void Start()
    {
        baseSpecialBulletsPerTap = specialBulletsPerTap;
        cooldown = GameObject.Find("CanvasPrefab/Cooldowns").GetComponent<CooldownScript>();
        cam = GameObject.Find("Player/PitchController/Main Camera").GetComponent<Camera>();
        specialBulletPool = new Queue<GameObject>();
        GameObject specialBullets = new("Special Bullets");

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

    public void ResetCooldown()
    {
        //reducir cooldown
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {

        shootingSpecial = Input.GetKeyDown(KeyCode.Mouse1);

        //CHECKEAR SI SE ESTA RECARGANDO Y NO PERMITIR UTILIZAR EL ESPECIAL

        if (readyToShootSpecial && shootingSpecial && !reloading)
        {
            specialBulletsShot = 0;
            ShootSpecial();
            cooldown.StartAbilityCooldown(specialCooldownTime);

        }
    }

    private void ShootSpecial()
    {
        readyToShootSpecial = false;

        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo;
        Vector3 hitPoint = r.GetPoint(50);

        if (Physics.Raycast(r, out hitInfo))
            //Crec que a vegades les bales surten rares pq això detecta una bala ja disparada.
            if(hitInfo.collider.gameObject.CompareTag("Player"))
                hitPoint = hitInfo.point;



        Vector3 directionWithoutSpread = hitPoint - specialBulletOrigin.position;

        float xSpread = Random.Range(-specialSpread, +specialSpread);
        float ySpread = Random.Range(-specialSpread, +specialSpread);
        float zSpread = Random.Range(-specialSpread, +specialSpread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentBullet = specialBulletPool.Dequeue();
        currentBullet.SetActive(true);
        currentBullet.transform.position = specialBulletOrigin.position;
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<SGSpecialBulletScript>().SetDamage(specialBulletDamage + PlayerStatsScript.playerStatsInstance.currentDamageBonus);
        if (cooldownBlessing)
        {
            currentBullet.GetComponent<SGSpecialBulletScript>().weaponScript = this;
        }

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * specialShootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * specialUpwardForce, ForceMode.Impulse);

        specialBulletPool.Enqueue(currentBullet);

        specialBulletsShot++;

        if (allowInvokeSpecial)
        {
            Invoke(nameof(ResetSpecialShot), specialCooldownTime);
            allowInvokeSpecial = false;
        }
        if (specialBulletsShot < specialBulletsPerTap)
        {
            Invoke(nameof(ShootSpecial), specialTimeBetweenShots);
        }
    }

    private void ResetSpecialShot()
    {
        readyToShootSpecial = true;
        allowInvokeSpecial = true;
    }

    public void SetSBulletsPerTap(int bPerTap)
    {
        specialBulletsPerTap = bPerTap;

        if (bPerTap == 0)
            specialBulletsPerTap = baseSpecialBulletsPerTap;
    }
}
