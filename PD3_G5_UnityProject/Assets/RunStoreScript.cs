using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunStoreScript : MonoBehaviour
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

    // Update is called once per frame
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
        if (playerStats.currentNormalCoin >= 20)
        {
            playerStats.baseMaxHealth += 10;

            player.GetComponent<PlayerHealthScript>().ModifyMaxHealth(10);
            player.GetComponent<PlayerHealthScript>().ModifyHealth(10);


            CoinCounterScript.coinCounterInstance.updateNCCounter(-20);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            storeCanvas.SetActive(false);

        }

    }
    public void IncreaseDamage()
    {
        if (playerStats.currentNormalCoin >= 20)
        {
            playerStats.baseDamageBonus += 10;
            playerStats.currentDamageBonus += 10;
            CoinCounterScript.coinCounterInstance.updateNCCounter(-20);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            storeCanvas.SetActive(false);
        }
    }
    public void IncreaseSpeed()
    {
        if (playerStats.currentNormalCoin >= 20)
        {
            playerStats.baseSpeedBonus += 10;
            playerStats.currentSpeedBonus += 10;
            CoinCounterScript.coinCounterInstance.updateNCCounter(-20);
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
