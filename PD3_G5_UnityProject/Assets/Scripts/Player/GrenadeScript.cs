using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrenadeScript : MonoBehaviour
{
    [SerializeField] GameObject grenadePrefab;

    [Header("Forces")]
    [SerializeField] float grenadeShootForce;
    [SerializeField] float grenadeUpwardForce;

    [Header("Special Stats")]
    [SerializeField] float grenadeCooldown;
    [SerializeField] float grenadeSpread;
    [SerializeField] float grenadeReloadTime;
    [SerializeField] float grenadeTimeBetweenShots;
    [SerializeField] int grenadesPerTap;

    [Header("Spawn Point")]
    private Camera cam;
    [SerializeField] Transform grenadeOrigin;

    [Header("Debug")]
    [SerializeField] bool allowInvokeGrenade;

    int grenadesShot;

    bool readyToShootGrenade, shootingGrenade;
    Queue<GameObject> grenadePool;
    CooldownScript cooldown;
    DoubleAOEBlessingScript doubleAOEBlessing;
    MultipleTargetsDamageBuffBlessingScript multipleTargetsDamageBuffBlessing;
    public int currentGrenadeCharges;
    private Vector3 explosionScale;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = GameObject.Find("CanvasPrefab/Cooldowns").GetComponent<CooldownScript>();
        cam = transform.Find("PitchController/Main Camera").gameObject.GetComponent<Camera>();
        doubleAOEBlessing = GetComponent<DoubleAOEBlessingScript>();
        multipleTargetsDamageBuffBlessing = GetComponent<MultipleTargetsDamageBuffBlessingScript>();
        grenadePool = new Queue<GameObject>();
        GameObject grenades = new("Grenades");
        currentGrenadeCharges = PlayerStatsScript.instance.currentMaxGrenadeCharges;


        for (int i = 0; i < grenadesPerTap + 5; i++)
        {
            GameObject grenade = Instantiate(grenadePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            grenade.SetActive(false);
            grenadePool.Enqueue(grenade);
            grenade.transform.parent = grenades.transform;
        }

        readyToShootGrenade = true;
        shootingGrenade = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {

        shootingGrenade = Input.GetKeyDown(KeyCode.F);

        //CHECKEAR SI SE ESTA RECARGANDO Y NO PERMITIR UTILIZAR LA HABLIDAD
        if (readyToShootGrenade && shootingGrenade && Time.timeScale == 1f && !PlayerStatsScript.instance.isReloading)
        {

            shootingGrenade = false;
            grenadesShot = 0;
            //START COOLDOWN SS
            ShootGrenade();
            if (currentGrenadeCharges > 1)
            {
                currentGrenadeCharges--;
            }
            else
            {
                cooldown.StartGrenadeCooldown(grenadeCooldown);
                currentGrenadeCharges = PlayerStatsScript.instance.currentMaxGrenadeCharges;
            }
        }
    }

    private void ShootGrenade()
    {
        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo;
        Vector3 hitPoint = r.GetPoint(50);

        if (Physics.Raycast(r, out hitInfo))
        {
            hitPoint = hitInfo.point;
            if (hitInfo.collider.gameObject.CompareTag("Enemy") || hitInfo.collider.gameObject.CompareTag("Terrain"))
                hitPoint = hitInfo.point;
        }
   
        Vector3 directionWithoutSpread = hitPoint - grenadeOrigin.position;

        float xSpread = Random.Range(-grenadeSpread, +grenadeSpread);
        float ySpread = Random.Range(-grenadeSpread, +grenadeSpread);
        float zSpread = Random.Range(-grenadeSpread, +grenadeSpread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentGrenade = grenadePool.Dequeue();
        currentGrenade.SetActive(true);
        currentGrenade.transform.position = grenadeOrigin.position;
        currentGrenade.transform.forward = directionWithoutSpread.normalized;

        if (doubleAOEBlessing.enabled)
            currentGrenade.GetComponent<GrenadeBulletScript>().SetAreaMulitplier(doubleAOEBlessing.areaMultiplyer);

        if(multipleTargetsDamageBuffBlessing.enabled)
            currentGrenade.GetComponent<GrenadeBulletScript>().multipleTargetsBlessing = true;


        currentGrenade.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * grenadeShootForce, ForceMode.Impulse);
        currentGrenade.GetComponent<Rigidbody>().AddForce(cam.transform.up * grenadeUpwardForce, ForceMode.Impulse);

        grenadePool.Enqueue(currentGrenade);

        grenadesShot++;

        if (currentGrenadeCharges <= 1)
        {
            allowInvokeGrenade = false;
            readyToShootGrenade = false;
            Invoke(nameof(ResetGrenadeShot), grenadeCooldown);

        }

        if (grenadesShot < grenadesPerTap)
        {
            Invoke(nameof(ShootGrenade), grenadeTimeBetweenShots);
        }
    }

    private void ResetGrenadeShot()
    {
        readyToShootGrenade = true;
        allowInvokeGrenade = true;
    }
}