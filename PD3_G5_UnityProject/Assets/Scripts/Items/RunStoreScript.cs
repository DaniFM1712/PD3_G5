using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class RunStoreScript : MonoBehaviour
{
    [SerializeField] GameObject storeCanvas;
    [SerializeField] TextMeshProUGUI invFullText;
    [SerializeField] TextMeshProUGUI NCText;

    [SerializeField] int healthCost = 20;
    [SerializeField] int healthValue = 20;
    [SerializeField] int randomItemCost = 40;
    [SerializeField] int randomRareCost = 50;
    [SerializeField] int randomLegendaryCost = 60;

    private GameObject player;
    private bool canShop = false;

    [SerializeField] List<ConsumableAsset> commonItemPool;
    [SerializeField] List<ConsumableAsset> rareItemPool;
    [SerializeField] List<ConsumableAsset> legendaryItemPool;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (canShop && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            NCText.text = "NC: "+ PlayerStatsScript.playerStatsInstance.currentNormalCoin;
            NCText.enabled = true;
            storeCanvas.SetActive(true);
        }
    }

    public void BuyHealth()
    {
        if (PlayerStatsScript.playerStatsInstance.currentNormalCoin >= healthCost && PlayerStatsScript.playerStatsInstance.currentHealth < PlayerStatsScript.playerStatsInstance.currentMaxHealth * PlayerStatsScript.playerStatsInstance.currentMaxHealthMultiplyer)
        {

            player.GetComponent<PlayerHealthScript>().ModifyHealth(healthValue);

            CoinCounterScript.coinCounterInstance.updateNCCounter(-healthCost);
            NCText.text = "NC: " + PlayerStatsScript.playerStatsInstance.currentNormalCoin;

        }
    }
    public void BuyRandomItem()
    {
        if (PlayerStatsScript.playerStatsInstance.currentNormalCoin >= randomItemCost)
        {
            if (InventoryManagerScript.InventoryInstance.IsInventoryFull())
            {
                invFullText.enabled = true;
            }

            else
            {
                ConsumableAsset asset = GenerateRandomItem();
                while (!InventoryManagerScript.InventoryInstance.CanAddItem(asset.rarity))
                {
                    asset = GenerateRandomItem();
                }

                InventoryManagerScript.InventoryInstance.AddItem(asset.rarity, asset);

                CoinCounterScript.coinCounterInstance.updateNCCounter(-randomItemCost);
                NCText.text = "NC: " + PlayerStatsScript.playerStatsInstance.currentNormalCoin;
            }

        }
    }

    public void BuyRareItem()
    {
        if (PlayerStatsScript.playerStatsInstance.currentNormalCoin >= randomRareCost)
        {
            if (!InventoryManagerScript.InventoryInstance.CanAddItem(ConsumableAsset.Rarity.Rare))
            {
                invFullText.enabled = true;
            }

            else
            {
                ConsumableAsset asset = GenerateRareItem();

                InventoryManagerScript.InventoryInstance.AddItem(asset.rarity, asset);

                CoinCounterScript.coinCounterInstance.updateNCCounter(-randomRareCost);
                NCText.text = "NC: " + PlayerStatsScript.playerStatsInstance.currentNormalCoin;

            }

        }
    }

    public void BuyLegendaryItem()
    {
        if (PlayerStatsScript.playerStatsInstance.currentNormalCoin >= randomLegendaryCost)
        {
            if (!InventoryManagerScript.InventoryInstance.CanAddItem(ConsumableAsset.Rarity.Legendary))
            {
                invFullText.enabled = true;
            }

            else
            {
                ConsumableAsset asset = GenerateLegendaryItem();

                InventoryManagerScript.InventoryInstance.AddItem(asset.rarity, asset);

                CoinCounterScript.coinCounterInstance.updateNCCounter(-randomLegendaryCost);
                NCText.text = "NC: " + PlayerStatsScript.playerStatsInstance.currentNormalCoin;

            }

        }
    }

    public void CloseStore()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        invFullText.enabled = false;
        storeCanvas.SetActive(false);
    }

    private ConsumableAsset GenerateRandomItem()
    {
        ConsumableAsset asset = null;
        int randomNumber = Random.Range(0, 100);
        if (randomNumber <= 60)
        {
            int itemType = Random.Range(0, 4);
            switch (itemType)
            {
                case 0:
                    asset = commonItemPool[0];
                    break;
                case 1:
                    asset = commonItemPool[1];
                    break;
                case 2:
                    asset = commonItemPool[2];
                    break;
                case 3:
                    asset = commonItemPool[3];
                    break;
            }
        }
        else if (randomNumber <= 90)
        {
            int itemType = Random.Range(0, 4);
            switch (itemType)
            {
                case 0:
                    asset = rareItemPool[0];
                    break;
                case 1:
                    asset = rareItemPool[1];
                    break;
                case 2:
                    asset = rareItemPool[2];
                    break;
                case 3:
                    asset = rareItemPool[3];
                    break;
            }
        }
        else
        {
            int itemType = Random.Range(0, 4);
            switch (itemType)
            {
                case 0:
                    asset = legendaryItemPool[0];
                    break;
                case 1:
                    asset = legendaryItemPool[1];
                    break;
                case 2:
                    asset = legendaryItemPool[2];
                    break;
                case 3:
                    asset = legendaryItemPool[3];
                    break;
            }
        }

        return asset;
    }

    private ConsumableAsset GenerateRareItem()
    {
        int itemType = Random.Range(0, 4);
        switch (itemType)
        {
            case 0:
                return rareItemPool[0];
            case 1:
                return rareItemPool[1];
            case 2:
                return rareItemPool[2];
            case 3:
                return rareItemPool[3];
            default:
                return null;
        }
    }

    private ConsumableAsset GenerateLegendaryItem()
    {
        int itemType = Random.Range(0, 4);
        switch (itemType)
        {
            case 0:
                return legendaryItemPool[0];
            case 1:
                return legendaryItemPool[1];
            case 2:
                return legendaryItemPool[2];
            case 3:
                return legendaryItemPool[3];
            default:
                return null;
        }

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
