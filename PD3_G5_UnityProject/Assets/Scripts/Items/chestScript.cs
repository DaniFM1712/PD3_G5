using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    private PlayerStatsScript playerStats;
    public bool opened = false;
    private bool canTake = false;
    private PlayerHealthScript playerHealth;
    [SerializeField] Consumable consumableItem;
    //[SerializeField] ConsumableAsset commonMaxHealth;
    //[SerializeField] ConsumableAsset commonFireRate;
    //[SerializeField] ConsumableAsset commonEssenceObtained;
    //[SerializeField] ConsumableAsset commonDivinePowerObtained;
    //[SerializeField] ConsumableAsset commonCriticalDamage;
    [SerializeField] List<ConsumableAsset> commonItemPool;
    [SerializeField] List<ConsumableAsset> rareItemPool;
    [SerializeField] List<ConsumableAsset> legendaryItemPool;

    private void Start()
    {
        playerStats = PlayerStatsScript.playerStatsInstance;
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E))
        {
            opened = true;
            Canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void generateRandomReward()
    {
        //int randomNumber = Random.Range(0, 100);
        int randomNumber = 1;
        if(randomNumber <= 60)
        {
            int itemType = Random.Range(0, 10);
            itemType = 1;
            ConsumableAsset asset = null;
            switch (itemType)
            {
                case 1:
                    asset = commonItemPool[0];
                    break;
                case 2:
                    asset = commonItemPool[1];
                    break;
                case 3:
                    asset = commonItemPool[2];
                    break;
                case 4:
                    asset = commonItemPool[3];
                    break;
                case 5:
                    asset = commonItemPool[4];
                    break;
            }
            consumableItem.SetConsumableItem(asset);

        }
        else if (randomNumber <= 90)
        {
            int itemType = Random.Range(0, 10);
            itemType = 1;
            ConsumableAsset asset = null;
            switch (itemType)
            {
                case 1:
                    asset = rareItemPool[0];
                    break;
                case 2:
                    asset = rareItemPool[1];
                    break;
                case 3:
                    asset = rareItemPool[2];
                    break;
                case 4:
                    asset = rareItemPool[3];
                    break;
                case 5:
                    asset = rareItemPool[4];
                    break;
            }
            consumableItem.SetConsumableItem(asset);

        }
        else
        {
            int itemType = Random.Range(0, 10);
            itemType = 1;
            ConsumableAsset asset = null;
            switch (itemType)
            {
                case 1:
                    asset = legendaryItemPool[0];
                    break;
                case 2:
                    asset = legendaryItemPool[1];
                    break;
                case 3:
                    asset = legendaryItemPool[2];
                    break;
                case 4:
                    asset = legendaryItemPool[3];
                    break;
                case 5:
                    asset = legendaryItemPool[4];
                    break;
            }
            consumableItem.SetConsumableItem(asset);

        }

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))        
            canTake = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canTake = false;
    }

    private void OnDestroy()
    {
        Canvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ModifyCurrentMaxHealth(float amount)
    {
        Debug.Log("CHEST ITEM - MAX HEALTH");
        playerHealth.ModifyMaxHealth(amount);
        //Destroy(transform.parent.gameObject);
    }

    public void ModifyCurrentHealth(float amount)
    {
        playerHealth.ModifyHealth(amount);
        Destroy(transform.parent.gameObject);
    }

    public void ModifyCurrentSpeedBonus(int amount)
    {
        Debug.Log("CHEST ITEM - SPEED BONUS");
        playerStats.currentSpeedBonus += amount;
        Destroy(transform.parent.gameObject);
    }

    public void ModifyCurrentDamageBonus(int amount)
    {
        Debug.Log("CHEST ITEM - DAMAGE BONUS");
        playerStats.currentDamageBonus += amount;
        Destroy(transform.parent.gameObject);
    }
}
