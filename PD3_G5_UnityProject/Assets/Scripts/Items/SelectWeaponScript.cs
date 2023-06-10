using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeaponScript : MonoBehaviour
{
    [SerializeField] GameObject rapidFire;
    [SerializeField] GameObject shotGun;
    [SerializeField] GameObject weaponPosition;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject weaponCanvas;
    [SerializeField] GameObject shotgunButton;
    GameObject player;

    private bool canChangeWeapon = false;
    public StudioEventEmitter SelectEmitter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (!PlayerStatsScript.instance.shotgunUnlocked)
        {
            shotgunButton.GetComponent<Button>().interactable = false;
            shotGun.SetActive(false);
        }
        else
        {
            shotGun.SetActive(true);
            shotgunButton.GetComponent<Button>().interactable = true;
        }
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
        rapidFire.SetActive(false);
        if (PlayerStatsScript.instance.shotgunUnlocked)
        {
            shotGun.SetActive(true);
            shotgunButton.GetComponent<Button>().interactable = true;
        }
        PlayerStatsScript.instance.currentWeaponIndex = 1;
        player.GetComponent<FPController>().ChangeWeapon();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        weaponCanvas.SetActive(false);
        PlayerStatsScript.instance.currentBlessings.Clear();
        PlayerStatsScript.instance.SaveBlessings();
        PlayerStatsScript.instance.ActivateBlessings();
        SelectEmitter.Play();
    }

    public void selectShotGun()
    {
        shotGun.SetActive(false);
        rapidFire.SetActive(true);
        PlayerStatsScript.instance.currentWeaponIndex = 2;
        player.GetComponent<FPController>().ChangeWeapon();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        weaponCanvas.SetActive(false);
        PlayerStatsScript.instance.currentBlessings.Clear();
        PlayerStatsScript.instance.SaveBlessings();
        PlayerStatsScript.instance.ActivateBlessings();
        SelectEmitter.Play();
    }


    public void activeShotGunElements()
    {
        Debug.Log("ENTRO AL EVENTO");
        shotGun.SetActive(true);
        shotgunButton.GetComponent<Button>().interactable = true;
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
