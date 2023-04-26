using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeaponScript : MonoBehaviour
{
    [SerializeField] GameObject rapidFirePrefab;
    [SerializeField] GameObject shotGunPrefab;
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
        foreach (Transform child in weaponPosition.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject rapidFire = Instantiate(rapidFirePrefab);
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

        GameObject shotGun = Instantiate(shotGunPrefab);
        shotGun.transform.parent = weaponPosition.transform;
        
        
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        weaponCanvas.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        canChangeWeapon = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canChangeWeapon = false;
    }


}
