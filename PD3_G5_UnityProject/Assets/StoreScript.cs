using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    private PlayerStatsScript playerStats;
    [SerializeField] GameObject storeCanvas;
    private GameObject player;
    private bool canShop = false;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStatsScript.playerStatsInstance;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (canShop && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            storeCanvas.SetActive(true);
        }
    }

    public void IncreaseMaxHealth() 
    {
        if(playerStats.currentSpecialCoin >= 10)
        {
            playerStats.baseMaxHealth += 10;
            
            player.GetComponent<PlayerHealthScript>().ModifyMaxHealth(10);
            player.GetComponent<PlayerHealthScript>().ModifyHealth(10);


            CoinCounterScript.coinCounterInstance.updateSCCounter(-10);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            storeCanvas.SetActive(false);
            
        }

    }
    public void IncreaseDamage() {
        if (playerStats.currentSpecialCoin >= 10)
        {
            playerStats.baseDamageBonus += 10;
            playerStats.currentDamageBonus += 10;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-10);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            storeCanvas.SetActive(false);
        }
    }
    public void IncreaseSpeed() {
        if (playerStats.currentSpecialCoin >= 10)
        {
            playerStats.baseSpeedBonus += 10;
            playerStats.currentSpeedBonus += 10;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-10);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            storeCanvas.SetActive(false);
        }
    }

    public void CloseStore()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        storeCanvas.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        canShop = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canShop = false;
    }

}
