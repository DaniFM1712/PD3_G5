using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeaponScript : MonoBehaviour
{
    [SerializeField] GameObject rapidFire;
    [SerializeField] GameObject shotGun;
    [SerializeField] GameObject weaponPosition;
    [SerializeField] GameObject weaponCanvas;
    GameObject player;

    private bool canChangeWeapon = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
      if(canChangeWeapon && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None; 
            weaponCanvas.SetActive(true);
        }  
    }


    public void selectRapidFire()
    {
        PlayerStatsScript.playerStatsInstance.currentWeaponIndex = 1;
        player.GetComponent<FPController>().ChangeWeapon();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        weaponCanvas.SetActive(false);
        PlayerStatsScript.playerStatsInstance.currentBlessings.Clear();
        PlayerStatsScript.playerStatsInstance.SaveBlessings();
        PlayerStatsScript.playerStatsInstance.ActivateBlessings();
    }

    public void selectShotGun()
    {
        PlayerStatsScript.playerStatsInstance.currentWeaponIndex = 2;
        player.GetComponent<FPController>().ChangeWeapon();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        weaponCanvas.SetActive(false);
        PlayerStatsScript.playerStatsInstance.currentBlessings.Clear();
        PlayerStatsScript.playerStatsInstance.SaveBlessings();
        PlayerStatsScript.playerStatsInstance.ActivateBlessings();

    }


    private void OnTriggerEnter(Collider other)
    {
        canChangeWeapon = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canChangeWeapon = false;
    }










    /*public void selectRapidFire()
    {
        foreach (Transform child in weaponPosition.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject rapidFire = Instantiate(rapidFirePrefab, weaponPosition.transform.position, weaponPosition.transform.rotation);
        rapidFire.transform.parent = weaponPosition.transform;
        
        
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        weaponCanvas.SetActive(false);
    }

    public void selectShotGun()
    {
        foreach (Transform child in weaponPosition.transform) {
            Destroy(child.gameObject);
        }

        GameObject shotGun = Instantiate(shotGunPrefab, weaponPosition.transform.position, weaponPosition.transform.rotation);
        shotGun.transform.parent = weaponPosition.transform;
        
        
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        weaponCanvas.SetActive(false);
    }
    */
}
