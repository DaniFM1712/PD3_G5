using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShootingScript : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;

    [Header("Forces")]
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;

    [Header("Stats")]
    [SerializeField] float timeBetweenShooting;
    [SerializeField] float spread;
    [SerializeField] float reloadTime;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float bulletDamage;
    [SerializeField] int magazineSize;
    [SerializeField] int bulletsPerTap;
    [SerializeField] bool allowButtonHold;

    [Header("Spawn Point")]
    [SerializeField] Camera cam;
    [SerializeField] Transform attackPoint;

    [Header("Debug")]
    [SerializeField] bool allowInvoke;

    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;
    Queue<GameObject> bulletPool;

    // Start is called before the first frame update
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < magazineSize + 20; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }

        bulletsLeft = magazineSize;
        readyToShoot = true;
        shooting = false;
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
}
