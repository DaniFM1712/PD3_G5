using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunStoreScript : MonoBehaviour
{
    [SerializeField] GameObject storeCanvas;
    [SerializeField] TextMeshProUGUI invFullText;
    [SerializeField] TextMeshProUGUI NCText;

    [SerializeField] Button randomItemBtn;
    [SerializeField] Button rareItemBtn;
    [SerializeField] Button legendaryItemBtn;

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

    private ConsumableAsset randomItem;
    private ConsumableAsset rareItem;
    private ConsumableAsset legendaryItem;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GenerateRandomItem();
        GenerateRareItem();
        GenerateLegendaryItem();
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

            if(InventoryManagerScript.InventoryInstance.CanAddItem(randomItem.rarity))
            {
                randomItemBtn.interactable = false;

                InventoryManagerScript.InventoryInstance.AddItem(randomItem.rarity, randomItem);

                CoinCounterScript.coinCounterInstance.updateNCCounter(-randomItemCost);
                NCText.text = "NC: " + PlayerStatsScript.playerStatsInstance.currentNormalCoin;
            }

        }

    }

    public void BuyRareItem()
    {

        if (PlayerStatsScript.playerStatsInstance.currentNormalCoin >= randomRareCost)
        {

            if (InventoryManagerScript.InventoryInstance.CanAddItem(rareItem.rarity))
            {
                rareItemBtn.interactable = false;

                InventoryManagerScript.InventoryInstance.AddItem(rareItem.rarity, rareItem);

                CoinCounterScript.coinCounterInstance.updateNCCounter(-randomRareCost);
                NCText.text = "NC: " + PlayerStatsScript.playerStatsInstance.currentNormalCoin;

            }

        }

    }

    public void BuyLegendaryItem()
    {

        if (PlayerStatsScript.playerStatsInstance.currentNormalCoin >= randomLegendaryCost)
        {

            if (InventoryManagerScript.InventoryInstance.CanAddItem(legendaryItem.rarity))
            {
                legendaryItemBtn.interactable = false;

                InventoryManagerScript.InventoryInstance.AddItem(legendaryItem.rarity, legendaryItem);

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

    private void GenerateRandomItem()
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

        randomItem = asset;
        randomItemBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RANDOM COM ITEM\n" + randomItem.itemDescription + "\nCOST: "+randomItemCost+"NC";
    }

    private void GenerateRareItem()
    {
        ConsumableAsset asset = null;
        int itemType = Random.Range(0, 4);
        switch (itemType)
        {
            case 0:
                asset =  rareItemPool[0];
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

        rareItem = asset;
        rareItemBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RANDOM RARE ITEM\n" + rareItem.itemDescription + "\nCOST: "+randomRareCost+"NC";

    }

    private void GenerateLegendaryItem()
    {
        ConsumableAsset asset = null;
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
        legendaryItem = asset;
        legendaryItemBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RANDOM LEG ITEM\n" + legendaryItem.itemDescription + "\nCOST: " + randomLegendaryCost + "NC";


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
