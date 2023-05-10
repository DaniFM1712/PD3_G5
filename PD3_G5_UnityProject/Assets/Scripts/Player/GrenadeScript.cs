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
    [SerializeField] float grenadeTimeBetweenShooting;
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
    public int currentGrenadeCharges;
    private Vector3 explosionScale;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = GameObject.Find("CanvasPrefab/Cooldowns").GetComponent<CooldownScript>();
        cam = transform.Find("PitchController/Main Camera").gameObject.GetComponent<Camera>();
        doubleAOEBlessing = GetComponent<DoubleAOEBlessingScript>();
        grenadePool = new Queue<GameObject>();
        GameObject grenades = new("Grenades");
        currentGrenadeCharges = PlayerStatsScript.playerStatsInstance.currentMaxGrenadeCharges;


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
        if (readyToShootGrenade && shootingGrenade)
        {

            shootingGrenade = false;
            grenadesShot = 0;
            //START COOLDOWN SS
            ShootGrenade();
            if (currentGrenadeCharges < 1)
            {
                cooldown.StartGrenadeCooldown(grenadeTimeBetweenShooting);
                currentGrenadeCharges = PlayerStatsScript.playerStatsInstance.currentMaxGrenadeCharges;
            }
            else
            {
                currentGrenadeCharges--;
            }
        }
    }

    private void ShootGrenade()
    {
        Debug.Log("GRENADE 3");
        readyToShootGrenade = PlayerStatsScript.playerStatsInstance.currentMaxGrenadeCharges == currentGrenadeCharges;

        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo;
        Vector3 hitPoint;


        if (Physics.Raycast(r, out hitInfo))
            hitPoint = hitInfo.point;

        else
            hitPoint = r.GetPoint(100);


        Vector3 directionWithoutSpread = hitPoint - grenadeOrigin.position;

        float xSpread = Random.Range(-grenadeSpread, +grenadeSpread);
        float ySpread = Random.Range(-grenadeSpread, +grenadeSpread);
        float zSpread = Random.Range(-grenadeSpread, +grenadeSpread);


        directionWithoutSpread += new Vector3(xSpread, ySpread, zSpread);

        GameObject currentGrenade = grenadePool.Dequeue();
        currentGrenade.SetActive(true);
        currentGrenade.transform.position = grenadeOrigin.position;
        currentGrenade.transform.forward = directionWithoutSpread.normalized;

        if(doubleAOEBlessing.enabled)
            currentGrenade.GetComponent<GrenadeBulletScript>().SetAreaMulitplier(doubleAOEBlessing.areaMultiplyer);

        currentGrenade.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * grenadeShootForce, ForceMode.Impulse);
        currentGrenade.GetComponent<Rigidbody>().AddForce(cam.transform.up * grenadeUpwardForce, ForceMode.Impulse);

        grenadePool.Enqueue(currentGrenade);

        grenadesShot++;

        if (allowInvokeGrenade)
        {
            if (currentGrenadeCharges < 1)
            {
                Invoke(nameof(ResetGrenadeShot), grenadeTimeBetweenShooting);
                allowInvokeGrenade = false;
            }
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
