using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileShootingScript : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;

    [Header("Forces")]
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;

    [Header("Stats")]
    [SerializeField] float timeBetweenShooting;
    private float baseTimeBetweenShooting;
    [SerializeField] float spread;
    [SerializeField] float reloadTime;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float bulletDamage;
    [SerializeField] int magazineSize;
    [SerializeField] int bulletsPerTap;
    [SerializeField] bool allowButtonHold;

    [Header("Spawn Point")]
    private Camera cam;
    [SerializeField] Transform bulletOrigin;

    [Header("Debug")]
    [SerializeField] bool allowInvoke;

    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;
    bool dashDamageBlessing = false;
    Queue<GameObject> bulletPool;

    [SerializeField] private UnityEvent<float> PlayerHasShoot; 
    // Start is called before the first frame update
    private void Start()
    {
        baseTimeBetweenShooting = timeBetweenShooting;
        cam = GameObject.Find("Player/PitchController/Main Camera").GetComponent<Camera>();
        bulletPool = new Queue<GameObject>();
        GameObject bullets = new("Bullets");

        for (int i = 0; i < magazineSize + 20; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
            bullet.transform.parent = bullets.transform;
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
        PlayerHasShoot.Invoke(timeBetweenShooting);
        readyToShoot = false;

        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        //RaycastHit hitInfo; 
        //Vector3 hitPoint = r.GetPoint(30f);
        Vector3 hitPoint = r.GetPoint(50);

        //if (Physics.Raycast(r, out hitInfo, maxShootDist, shootingMask))
        
        if (Physics.Raycast(r, out RaycastHit hitInfo))
        {
            //Crec que a vegades les bales surten rares pq aix� detecta una bala ja disparada.
            if (!hitInfo.collider.gameObject.CompareTag("Bullet"))
                hitPoint = hitInfo.point;
        }
            

        Vector3 directionWithoutSpread = hitPoint - bulletOrigin.position;

        float xSpread = Random.Range(-spread, +spread);
        float ySpread = Random.Range(-spread, +spread);
        float zSpread = Random.Range(-spread, +spread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentBullet = bulletPool.Dequeue();
        currentBullet.SetActive(true);
        currentBullet.transform.position = bulletOrigin.position;
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<BulletScript>().SetDamage(bulletDamage+PlayerStatsScript.playerStatsInstance.currentDamageBonus);
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        bulletPool.Enqueue(currentBullet);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke(nameof(ResetShot), timeBetweenShooting*PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer);
            allowInvoke = false;
        }
        if(bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke(nameof(Shoot), timeBetweenShots);
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

    public bool IsReloading()
    {
        return reloading;
    }

    public void SetTimeBetweenShooting(float multiplyer)
    {
        timeBetweenShooting *= multiplyer;
        if (multiplyer == 0)
            timeBetweenShooting = baseTimeBetweenShooting;
    }
    
}
