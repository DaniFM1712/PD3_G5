using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    [SerializeField] GameObject storeCanvas;
    private PlayerHealthScript playerHealth;
    private bool canShop = false;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealthScript>();
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
        if(PlayerStatsScript.instance.currentSpecialCoin >= 10)
        {
            PlayerStatsScript.instance.baseMaxHealth += 10;

            playerHealth.ModifyMaxHealth(10);
            playerHealth.ModifyHealth(10);


            CoinCounterScript.coinCounterInstance.updateSCCounter(-10);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            storeCanvas.SetActive(false);

        }

    }
    public void IncreaseDamage() {
        if (PlayerStatsScript.instance.currentSpecialCoin >= 10)
        {
            PlayerStatsScript.instance.baseDamageBonus += 10;
            PlayerStatsScript.instance.currentDamageBonus += 10;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-10);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            storeCanvas.SetActive(false);
        }
    }
    public void IncreaseSpeed() {
        if (PlayerStatsScript.instance.currentSpecialCoin >= 10)
        {
            PlayerStatsScript.instance.baseSpeedBonus += 10;
            PlayerStatsScript.instance.currentSpeedBonus += 10;
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
