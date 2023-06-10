using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using TMPro;
using FMODUnity;

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
    [SerializeField] float threeShotBuffDamage = 30f;
    [SerializeField] float reloadDamageBuff = 30f;

    [Header("Spawn Point")]
    private Camera cam;
    private TextMeshProUGUI bulletCounterText;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] ParticleSystem[] particles;

    [Header("Debug")]
    [SerializeField] bool allowInvoke;
    
    AnimatorEventConsumerScript animatorConsumer;

    

    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot;
    bool dashDamageBlessing = false;
    bool highHealthDamageBuff = false;
    Queue<GameObject> bulletPool;

    [SerializeField] private UnityEvent<float> PlayerHasShoot;
    private GameObject player;
    private int shotCounter = 0;
    private bool reloadBuff = false;
    [SerializeField] float ammoBuffCooldown = 20f;
    private float ammoBuffTimer;
    private bool ammoBuffInCooldown;


    [Header("FMOD")]
    public StudioEventEmitter ShootEmitter;
    public StudioEventEmitter ReloadEmitter;


    // Start is called before the first frame update
    private void Start()
    {
        ammoBuffTimer = ammoBuffCooldown;
        //animatorConsumer = GameObject.Find("AnimatorConsumerPrefab").GetComponent<AnimatorEventConsumerScript>();
        baseTimeBetweenShooting = timeBetweenShooting;
        cam = GameObject.Find("Player/PitchController/Main Camera").GetComponent<Camera>();
        bulletCounterText = GameObject.Find("CanvasPrefab/Bullets/BulletCounter").GetComponent<TextMeshProUGUI>();

        bulletPool = new Queue<GameObject>();
        GameObject bullets = new("Bullets");

        for (int i = 0; i < (int)(magazineSize * PlayerStatsScript.instance.currentAmmoMultiplyer) + 20; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
            bullet.transform.parent = bullets.transform;
        }

        bulletsLeft = (int) (magazineSize * PlayerStatsScript.instance.currentAmmoMultiplyer);
        bulletCounterText.text = bulletsLeft / bulletsPerTap + " / " + (int)(magazineSize * PlayerStatsScript.instance.currentAmmoMultiplyer) / bulletsPerTap;

        readyToShoot = true;
        shooting = false;

        if(PlayerStatsScript.instance.currentWeaponIndex == 0)
        {
            bulletCounterText.text = "0 / 0" ;
        }

        player = transform.parent.parent.parent.gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if (ammoBuffInCooldown)
        {
            ammoBuffTimer -= Time.deltaTime;
            if(ammoBuffTimer < 0){
                ammoBuffInCooldown = false;
                ammoBuffTimer = ammoBuffCooldown;

            }
        }
        CheckInput();
    }

    private void CheckInput()
    {
        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < (int)(magazineSize * PlayerStatsScript.instance.currentAmmoMultiplyer) && !PlayerStatsScript.instance.isReloading)
            Reload();

        if(readyToShoot && shooting && !PlayerStatsScript.instance.isReloading && Time.timeScale == 1f)
        {
            bulletsShot = 0;
            if (bulletsLeft > 0)
            {
                //if(PlayerStatsScript.playerStatsInstance.currentWeaponIndex == 1)
                //{
                //    animatorConsumer.RapidFireEvent();
                //}
                //else if(PlayerStatsScript.playerStatsInstance.currentWeaponIndex == 2)
                //{
                //    animatorConsumer.ShotGunEvent();
                //}
                AnimatorEventConsumerScript.instance.shooting = true;
                ShootEmitter.Play();
                for (int i = 0; i < particles.Length; i++)
                {
                    if (particles[i] != null)
                        particles[i].Play();
                }
                    
                Shoot();
                if (PlayerStatsScript.instance.threeShotBuff)
                    shotCounter++;
                else
                    shotCounter = 0;
                AnimatorEventConsumerScript.instance.startShootAnimation();
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
            if (hitInfo.collider.gameObject.CompareTag("Enemy") || hitInfo.collider.gameObject.CompareTag("Terrain"))
                hitPoint = hitInfo.point;
        }
            

        Vector3 directionWithoutSpread = hitPoint - bulletOrigin.position;

        float xSpread = UnityEngine.Random.Range(-spread, +spread);
        float ySpread = UnityEngine.Random.Range(-spread, +spread);
        float zSpread = UnityEngine.Random.Range(-spread, +spread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentBullet = bulletPool.Dequeue();
        currentBullet.SetActive(true);
        currentBullet.transform.position = bulletOrigin.position;
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        float damage = (bulletDamage + PlayerStatsScript.instance.currentDamageBonus) *PlayerStatsScript.instance.currentDamageMultiplyer;
        if (shotCounter == 2)
        {
            damage += threeShotBuffDamage;
            shotCounter = 0;
        }
        if(reloadBuff)
        {
            damage += reloadDamageBuff;
            reloadBuff = false;
        }

        currentBullet.GetComponent<BulletScript>().SetDamage(damage);
        currentBullet.GetComponent<BulletScript>().player = player;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        bulletPool.Enqueue(currentBullet);

        bulletsLeft--;
        bulletsShot++;

        bulletCounterText.text = bulletsLeft/bulletsPerTap + " / " + (int)(magazineSize * PlayerStatsScript.instance.currentAmmoMultiplyer) / bulletsPerTap;

        if (allowInvoke)
        {
            Invoke(nameof(ResetShot), timeBetweenShooting*PlayerStatsScript.instance.currentFireRateMultiplyer);
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
        AnimatorEventConsumerScript.instance.shooting = false;
    }

    private void Reload()
    {
        if ((PlayerStatsScript.instance.ammoBuff && ammoBuffInCooldown) || !PlayerStatsScript.instance.ammoBuff)
        {
            AnimatorEventConsumerScript.instance.reloading = true;
            ReloadEmitter.Play();
            AnimatorEventConsumerScript.instance.startReloadAnimation();
            PlayerStatsScript.instance.isReloading = true;
            Invoke(nameof(ReloadFinished), reloadTime);
        }
        else if(PlayerStatsScript.instance.ammoBuff && !ammoBuffInCooldown)
        {
            ammoBuffInCooldown = true;
            ReloadEmitter.Play();
            ReloadFinished();
        }

    }

    public void ReloadFinished()
    {
        AnimatorEventConsumerScript.instance.reloading = false;
        PlayerStatsScript.instance.isReloading = false;
        bulletsLeft = (int)(magazineSize * PlayerStatsScript.instance.currentAmmoMultiplyer);
        bulletCounterText.text = bulletsLeft / bulletsPerTap + " / " + (int)(magazineSize * PlayerStatsScript.instance.currentAmmoMultiplyer) / bulletsPerTap;
        if (PlayerStatsScript.instance.reloadDamageBuff)
        {
            reloadBuff = true;
        }
    }

    public void changeDamage(float newBulletDamage)
    {
        bulletDamage = newBulletDamage;
    }


    public void SetTimeBetweenShooting(float multiplyer)
    {
        timeBetweenShooting *= multiplyer;
        if (multiplyer == 0)
            timeBetweenShooting = baseTimeBetweenShooting;
    }


    private void OnEnable()
    {
        try
        {
            if (PlayerStatsScript.instance.currentWeaponIndex != 0)
            {
                bulletCounterText.text = bulletsLeft / bulletsPerTap + " / " + (int)(magazineSize * PlayerStatsScript.instance.currentAmmoMultiplyer) / bulletsPerTap;
            }
        } catch (Exception e) {

        }
        
    }

}
