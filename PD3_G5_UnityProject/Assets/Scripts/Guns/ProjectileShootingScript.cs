using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileShootingScript : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject specialBulletPrefab;

    [Header("Forces")]
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] float specialShootForce;
    [SerializeField] float specialUpwardForce;

    [Header("Stats")]
    [SerializeField] float timeBetweenShooting;
    [SerializeField] float spread;
    [SerializeField] float reloadTime;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float bulletDamage;
    [SerializeField] int magazineSize;
    [SerializeField] int bulletsPerTap;
    [SerializeField] bool allowButtonHold;

    [Header("Special Stats")]
    [SerializeField] UnityEvent<float> startAbilityCooldown;
    [SerializeField] float specialTimeBetweenShooting;
    [SerializeField] float specialSpread;
    [SerializeField] float specialReloadTime;
    [SerializeField] float specialTimeBetweenShots;
    [SerializeField] float specialBulletDamage;
    [SerializeField] int specialBulletsPerTap;

    [Header("Spawn Point")]
    [SerializeField] Camera cam;
    [SerializeField] Transform attackPoint;

    [Header("Debug")]
    [SerializeField] bool allowInvoke;

    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading, shootingSpecial;
    Queue<GameObject> bulletPool;
    Queue<GameObject> specialBulletPool;


    // Start is called before the first frame update
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        bulletPool = new Queue<GameObject>();
        specialBulletPool = new Queue<GameObject>();
        GameObject bullets = new GameObject("Bullets");
        GameObject specialBullets = new GameObject("Special Bullets");

        for (int i = 0; i < magazineSize + 20; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
            bullet.transform.parent = bullets.transform;
        }

        for (int i = 0; i < specialBulletsPerTap + 20; i++)
        {
            GameObject specialBullet = Instantiate(specialBulletPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            specialBullet.SetActive(false);
            specialBulletPool.Enqueue(specialBullet);
            specialBullet.transform.parent = specialBullets.transform;
        }

        bulletsLeft = magazineSize;
        readyToShoot = true;
        shooting = false;
        shootingSpecial = false;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        shootingSpecial = Input.GetKeyDown(KeyCode.Mouse1);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();

        if(readyToShoot && shooting && !reloading)
        {
            bulletsShot = 0;
            if (bulletsLeft > 0)
            {
                Shoot();
            }
            else
                Reload();
        }

        if (readyToShoot && shootingSpecial && !reloading && !shooting)
        {
            bulletsShot = 0;
            //START COOLDOWN SS
            ShootSpecial();
            startAbilityCooldown.Invoke(specialTimeBetweenShooting);
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo; 
        //Vector3 hitPoint = r.GetPoint(30f);
        Vector3 hitPoint;

        //if (Physics.Raycast(r, out hitInfo, maxShootDist, shootingMask))
        
        if (Physics.Raycast(r, out hitInfo))
            //Crec que a vegades les bales surten rares pq això detecta una bala ja disparada.
            hitPoint = hitInfo.point;

        else
            hitPoint = r.GetPoint(100);
        

        Vector3 directionWithoutSpread = hitPoint - attackPoint.position;

        float xSpread = Random.Range(-spread, +spread);
        float ySpread = Random.Range(-spread, +spread);
        float zSpread = Random.Range(-spread, +spread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentBullet = bulletPool.Dequeue();
        currentBullet.SetActive(true);
        currentBullet.transform.position = attackPoint.position;
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<BulletScript>().SetDamage(bulletDamage);

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        bulletPool.Enqueue(currentBullet);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        if(bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ShootSpecial()
    {
        readyToShoot = false;

        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo;
        //Vector3 hitPoint = r.GetPoint(30f);
        Vector3 hitPoint;

        //if (Physics.Raycast(r, out hitInfo, maxShootDist, shootingMask))

        if (Physics.Raycast(r, out hitInfo))
            //Crec que a vegades les bales surten rares pq això detecta una bala ja disparada.
            hitPoint = hitInfo.point;

        else
            hitPoint = r.GetPoint(100);


        Vector3 directionWithoutSpread = hitPoint - attackPoint.position;

        float xSpread = Random.Range(-specialSpread, +specialSpread);
        float ySpread = Random.Range(-specialSpread, +specialSpread);
        float zSpread = Random.Range(-specialSpread, +specialSpread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentBullet = specialBulletPool.Dequeue();
        currentBullet.SetActive(true);
        currentBullet.transform.position = attackPoint.position;
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<SpecialBulletScript>().SetDamage(specialBulletDamage);

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * specialShootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * specialUpwardForce, ForceMode.Impulse);

        specialBulletPool.Enqueue(currentBullet);

        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", specialTimeBetweenShooting);
            allowInvoke = false;
        }
        if (bulletsShot < specialBulletsPerTap)
        {
            Invoke("Shoot", specialTimeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public void changeDamage(float newBulletDamage)
    {
        bulletDamage = newBulletDamage;
    }

}
