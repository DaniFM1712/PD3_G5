using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFSpecialScript : MonoBehaviour
{
    [SerializeField] GameObject specialBulletPrefab;

    [Header("Forces")]
    [SerializeField] float specialShootForce;
    [SerializeField] float specialUpwardForce;

    [Header("Special Stats")]
    [SerializeField] float specialCooldowmTime;
    [SerializeField] float specialSpread;
    [SerializeField] float specialReloadTime;
    [SerializeField] float specialTimeBetweenShots;
    [SerializeField] float specialBulletDamage;
    [SerializeField] int specialBulletsPerTap;

    [Header("Spawn Point")]
    private Camera cam;
    [SerializeField] Transform bulletOrigin;

    [Header("Debug")]
    [SerializeField] bool allowInvokeSpecial;

    int specialBulletsShot;

    bool readyToShootSpecial, shootingSpecial;
    Queue<GameObject> specialBulletPool;
    CooldownScript cooldown;


    // Start is called before the first frame update
    void Start()
    {
        cooldown = GameObject.Find("CanvasPrefab/Cooldowns").GetComponent<CooldownScript>();
        cam = GameObject.Find("Player/PitchController/Main Camera").GetComponent<Camera>();
        specialBulletPool = new Queue<GameObject>();
        GameObject specialBullets = new("RF Special Bullets");


        for (int i = 0; i < specialBulletsPerTap + 5; i++)
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
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {

        shootingSpecial = Input.GetKeyDown(KeyCode.Mouse1);

        //CHECKEAR SI SE ESTA RECARGANDO Y NO PERMITIR UTILIZAR LA HABLIDAD
        if (readyToShootSpecial && shootingSpecial)
        {
            shootingSpecial = false;
            specialBulletsShot = 0;
            //START COOLDOWN SS
            ShootSpecial();
            cooldown.StartAbilityCooldown(specialCooldowmTime);
        }
    }

    private void ShootSpecial()
    {
        readyToShootSpecial = false;

        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo;
        Vector3 hitPoint;


        if (Physics.Raycast(r, out hitInfo))
            hitPoint = hitInfo.point;

        else
            hitPoint = r.GetPoint(100);


        Vector3 directionWithoutSpread = hitPoint - bulletOrigin.position;

        float xSpread = Random.Range(-specialSpread, +specialSpread);
        float ySpread = Random.Range(-specialSpread, +specialSpread);
        float zSpread = Random.Range(-specialSpread, +specialSpread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentSpecialBullet = specialBulletPool.Dequeue();
        currentSpecialBullet.SetActive(true);
        currentSpecialBullet.transform.position = bulletOrigin.position;
        currentSpecialBullet.transform.forward = directionWithoutSpread.normalized;

        currentSpecialBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * specialShootForce, ForceMode.Impulse);
        currentSpecialBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * specialUpwardForce, ForceMode.Impulse);

        specialBulletPool.Enqueue(currentSpecialBullet);

        specialBulletsShot++;

        if (allowInvokeSpecial)
        {
            Invoke(nameof(ResetSpecialShot), specialCooldowmTime);
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
}