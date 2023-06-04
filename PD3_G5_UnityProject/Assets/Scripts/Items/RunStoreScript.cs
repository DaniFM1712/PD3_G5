using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class RunStoreScript : MonoBehaviour
{
    [SerializeField] GameObject storeCanvas;
    [SerializeField] TextMeshProUGUI invFullText;
    [SerializeField] TextMeshProUGUI NCText;

    [SerializeField] Button randomItemBtn;
    [SerializeField] Button rareItemBtn;
    [SerializeField] Button commonItemBtn;

    [SerializeField] int healthCost = 20;
    [SerializeField] int healthValue = 20;
    [SerializeField] int randomItemCost = 40;
    [SerializeField] int randomRareCost = 50;
    [SerializeField] int randomCommonCost = 60;

    private GameObject player;
    private bool canShop = false;
    private bool randomItemBought = false;

    [SerializeField] List<ConsumableAsset> commonItemPool;
    [SerializeField] List<ConsumableAsset> rareItemPool;
    [SerializeField] List<ConsumableAsset> legendaryItemPool;

    private ConsumableAsset randomItem;
    private ConsumableAsset rareItem;
    private ConsumableAsset commonItem;

    [Header("FMOD")]
    public StudioEventEmitter BuyEmitter;
    public StudioEventEmitter CloseEmitter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GenerateRandomItem();
        GenerateRareItem();
        GenerateCommonItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShop && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            NCText.text = "NC: "+ PlayerStatsScript.instance.currentNormalCoin;
            NCText.enabled = true;
            storeCanvas.SetActive(true);
        }
    }

    public void BuyHealth()
    {
        if (PlayerStatsScript.instance.currentNormalCoin >= healthCost && PlayerStatsScript.instance.currentHealth < PlayerStatsScript.instance.GetCurrentMaxHealth())
        {

            player.GetComponent<PlayerHealthScript>().ModifyHealth(healthValue);

            CoinCounterScript.coinCounterInstance.updateNCCounter(-healthCost);
            NCText.text = "NC: " + PlayerStatsScript.instance.currentNormalCoin;
            BuyEmitter.Play();
        }
    }
    public void BuyRandomItem()
    {

        if (PlayerStatsScript.instance.currentNormalCoin >= randomItemCost)
        {

            if(InventoryManagerScript.InventoryInstance.CanAddItem(randomItem.rarity) && !randomItemBought)
            {
                randomItemBought = true;
                randomItemBtn.interactable = false;

                InventoryManagerScript.InventoryInstance.AddItem(randomItem.rarity, randomItem);

                CoinCounterScript.coinCounterInstance.updateNCCounter(-randomItemCost);
                NCText.text = "NC: " + PlayerStatsScript.instance.currentNormalCoin;
                BuyEmitter.Play();

            }

        }

    }

    public void BuyRareItem()
    {

        if (PlayerStatsScript.instance.currentNormalCoin >= randomRareCost)
        {

            if (InventoryManagerScript.InventoryInstance.CanAddItem(rareItem.rarity))
            {
                rareItemBtn.interactable = false;

                InventoryManagerScript.InventoryInstance.AddItem(rareItem.rarity, rareItem);

                CoinCounterScript.coinCounterInstance.updateNCCounter(-randomRareCost);
                NCText.text = "NC: " + PlayerStatsScript.instance.currentNormalCoin;
                BuyEmitter.Play();

            }

        }

    }

    public void BuyCommonItem()
    {

        if (PlayerStatsScript.instance.currentNormalCoin >= randomCommonCost)
        {

            if (InventoryManagerScript.InventoryInstance.CanAddItem(commonItem.rarity))
            {
                commonItemBtn.interactable = false;

                InventoryManagerScript.InventoryInstance.AddItem(commonItem.rarity, commonItem);

                CoinCounterScript.coinCounterInstance.updateNCCounter(-randomCommonCost);
                NCText.text = "NC: " + PlayerStatsScript.instance.currentNormalCoin;
                BuyEmitter.Play();

            }

        }

    }

    public void CloseStore()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        invFullText.enabled = false;
        storeCanvas.SetActive(false);
        CloseEmitter.Play();
    }

    private void GenerateRandomItem()
    {
        ConsumableAsset asset = null;
        int randomNumber = Random.Range(0, 100);
        if (randomNumber <= 60)
        {
            int itemType = Random.Range(0, commonItemPool.Count);
            asset = commonItemPool[itemType];
        }
        else if (randomNumber <= 90)
        {
            int itemType = Random.Range(0, rareItemPool.Count);
            asset = rareItemPool[itemType];
        }
        else
        {
            int itemType = Random.Range(0, legendaryItemPool.Count);
            asset = legendaryItemPool[itemType];
        }

        randomItem = asset;
        randomItemBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RANDOM ITEM\n" + randomItem.itemDescription + "\nCOST: "+randomItemCost+"NC";
    }

    private void GenerateRareItem()
    {
        ConsumableAsset asset = null;
        int itemType = Random.Range(0, rareItemPool.Count);
        asset = rareItemPool[itemType];
        rareItem = asset;
        rareItemBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RANDOM RARE ITEM\n" + rareItem.itemDescription + "\nCOST: "+randomRareCost+"NC";
    }

    private void GenerateCommonItem()
    {
        ConsumableAsset asset = null;
        int itemType = Random.Range(0, commonItemPool.Count);
        asset = commonItemPool[itemType];
        commonItem = asset;
        commonItemBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RANDOM COM ITEM\n" + commonItem.itemDescription + "\nCOST: " + randomCommonCost + "NC";
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
