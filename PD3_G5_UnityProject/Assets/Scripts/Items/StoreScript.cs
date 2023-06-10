using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FMODUnity;
using UnityEngine.Events;

public class StoreScript : MonoBehaviour
{
    [SerializeField] GameObject storeCanvas;

    [Header ("Tabs")]
    [SerializeField] List<GameObject> tabs;
    private int currentTab = 0;

    [Header("Button Tabs")]
    [SerializeField] GameObject toolsButton;
    [SerializeField] GameObject inventoryButton;
    [SerializeField] GameObject statsButton;
    [SerializeField] GameObject gamemodesButton;
    [SerializeField] GameObject loreButton;


    [Header("WeaponInfo")]
    [SerializeField] List<Button> toolsButtons = new List<Button>();
    [SerializeField] List<int> toolsPrices = new List<int>();

    [Header("InventoryInfo")]
    [SerializeField] List<Button> inventoryButtons = new List<Button>();
    [SerializeField] List<int> inventoryPrices = new List<int>();


    [Header("StatsInfo")]
    [SerializeField] List<Button> statsButtons = new List<Button>();
    [SerializeField] List<int> statsPrices = new List<int>();
    [SerializeField] List<GameObject> unlockStep = new List<GameObject>();


    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] TextMeshProUGUI scCounter;
    private bool canShop = false;
    private int itemSelected = 0;
    public UnityEvent shotgunActive;


    [Header("FMOD")]
    public StudioEventEmitter SelectEmitter;
    public StudioEventEmitter BuyEmitter;
    public StudioEventEmitter CloseEmitter;

    // Start is called before the first frame update
    void Start()
    {
        scCounter.text = PlayerStatsScript.instance.currentSpecialCoin+"";
        updateStatsTab();
    }

    void Update()
    {
        if (canShop && Input.GetKeyDown(KeyCode.E))
        {
            int i = 0;
            foreach (bool boolean in PlayerStatsScript.instance.toolsUpgrades)
            {
                if (boolean)
                {
                    toolsButtons[i].interactable = false;
                }
                i++;
            }
            i = 0;
            foreach (bool boolean in PlayerStatsScript.instance.statsUpgrades)
            {
                if (boolean)
                {
                    statsButtons[i].interactable = false;
                }
                i++;
            }

            statsButtons[6].interactable = false;

            itemDescription.text = "";
            itemPrice.text = "Cost: ";
            itemSelected = 0;
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            storeCanvas.SetActive(true);
        }
    }

    public void SelectItem(int index)
    {
        itemSelected = index;
        switch (currentTab)
        {
            case 0:
                switch (itemSelected)
                {
                    case 1:
                        itemDescription.text = "Unlock Dash [LShift]: a short and fast movement towards the direction you're looking.";
                        break;
                    case 2:
                        itemDescription.text = "+1 Dash charges.";
                        break;
                    case 3:
                        itemDescription.text = "Unlock Divine Orb [F]: a projectile that creates an explosion upon impact.";
                        break;
                    case 4:
                        itemDescription.text = "+1 Divine Orb charges.";
                        break;
                    case 5:
                        itemDescription.text = "Unlock Demon Purger: a weapon with high damage, high spread and low range.";
                        break;
                    case 6:
                        itemDescription.text = "¿mejora comun de las dos armas?";
                        break;
                }
                itemPrice.text = "Cost: " + toolsPrices[index - 1];
                toolsButtons[6].interactable = true;
                break;
            case 1:
                switch (itemSelected)
                {
                    case 1:
                        itemDescription.text = "Increase Divine Power obtanied by 30%.";
                        break;
                    case 2:
                        itemDescription.text = "Increase Essence obtanied by 30%.";
                        break;
                    case 3:
                        itemDescription.text = "Increase damage dealt by 20%";
                        break;
                    case 4:
                        itemDescription.text = "Increase base max health by 20%";
                        break;
                    case 5:
                        itemDescription.text = "Increase firerate by 20%";
                        break;
                    case 6:
                        itemDescription.text = "Unlocks a second life. Once for run, if you die the current level will restart.";
                        break;
                }
                itemPrice.text = "Cost: " + inventoryPrices[index - 1];
                inventoryButtons[3].interactable = true;
                break;
            case 2:
                switch (itemSelected)
                {
                    case 1:
                        itemDescription.text = "Increase Divine Power obtanied by 30%.";
                        break;
                    case 2:
                        itemDescription.text = "Increase Essence obtanied by 30%.";
                        break;
                    case 3:
                        itemDescription.text = "Increase damage dealt by 20%";
                        break;
                    case 4:
                        itemDescription.text = "Increase base max health by 20%";
                        break;
                    case 5:
                        itemDescription.text = "Increase firerate by 20%";
                        break;
                    case 6:
                        itemDescription.text = "Unlocks a second life. Once for run, if you die the current level will restart.";
                        break;
                }
                itemPrice.text = "Cost: " + statsPrices[index - 1];
                statsButtons[6].interactable = true;
                break;
        }
        
        SelectEmitter.Play();
    }



    public void BuySelectedItem()
    {
        switch (currentTab)
        {
            case 0:
                switch (itemSelected)
                {
                    case 1:
                        BuyDashUnlock();
                        break;
                    case 2:
                        BuyDashUpgradeUnlock();
                        break;
                    case 3:
                        BuyGrenadeUnlock();
                        break;
                    case 4:
                        BuyGrenadehUpgradeUnlock();
                        break;
                    case 5:
                        BuyShotgunUnlock();
                        break;
                    case 6:
                        BuySecondLifeUpgrade();
                        break;
                }
                break;
            case 1:
                switch (itemSelected)
                {
                    case 1:
                        BuyCommonSlot();
                        break;
                    case 2:
                        BuyRareSlot();
                        break;
                    case 3:
                        BuyLegendarySlot();
                        break;
                }
                break;
            case 2:
                switch (itemSelected)
                {
                    case 1:
                        BuyDivinePowerUpgrade();
                        break;
                    case 2:
                        BuyEssenceUpgrade();
                        break;
                    case 3:
                        BuyBaseDamageDealedUpgrade();
                        break;
                    case 4:
                        BuyBaseHealthUpgrade();
                        break;
                    case 5:
                        BuyBaseFirerateUpgrade();
                        break;
                    case 6:
                        BuySecondLifeUpgrade();
                        break;
                }
                break;
            case 3:
                break;
            case 4:
                break;

        }    
        




    }

    //TOOLS BUYS
    private void BuyDashUnlock()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= toolsPrices[0])
        {
            PlayerStatsScript.instance.dashUnlocked = true;
            toolsButtons[0].interactable = false;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-toolsPrices[0]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            toolsButtons[6].interactable = false;
            itemDescription.text = "";
            itemPrice.text = "Cost: ";
            BuyEmitter.Play();
            PlayerStatsScript.instance.toolsUpgrades[0] = true;
        }
    }
    private void BuyDashUpgradeUnlock()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= toolsPrices[1])
        {

            PlayerStatsScript.instance.currentMaxDashCharges++;
            PlayerStatsScript.instance.baseMaxDashCharges++;
            PlayerStatsScript.instance.currentDashCharges = PlayerStatsScript.instance.currentMaxDashCharges;
            toolsButtons[1].interactable = false;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-toolsPrices[1]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            toolsButtons[6].interactable = false;
            itemDescription.text = "";
            itemPrice.text = "Cost: ";
            BuyEmitter.Play();
            PlayerStatsScript.instance.toolsUpgrades[1] = true;
        }
    }

    private void BuyGrenadeUnlock()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= toolsPrices[2])
        {
            PlayerStatsScript.instance.grenadeUnlocked = true;
            toolsButtons[2].interactable = false;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-toolsPrices[2]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            toolsButtons[6].interactable = false;
            itemDescription.text = "";
            itemPrice.text = "Cost: ";
            BuyEmitter.Play();
            PlayerStatsScript.instance.toolsUpgrades[2] = true;
        }
    }

    private void BuyGrenadehUpgradeUnlock()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= toolsPrices[3])
        {

            PlayerStatsScript.instance.currentMaxGrenadeCharges++;
            PlayerStatsScript.instance.baseMaxGrenadeCharges++;
            PlayerStatsScript.instance.currentGrenadeCharges = PlayerStatsScript.instance.currentMaxGrenadeCharges;
            toolsButtons[3].interactable = false;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-toolsPrices[3]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            toolsButtons[6].interactable = false;
            itemDescription.text = "";
            itemPrice.text = "Cost: ";
            BuyEmitter.Play();
            PlayerStatsScript.instance.toolsUpgrades[3] = true;
        }
    }

    private void BuyShotgunUnlock()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= toolsPrices[4])
        {
            PlayerStatsScript.instance.shotgunUnlocked = true;
            toolsButtons[4].interactable = false;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-toolsPrices[4]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            toolsButtons[6].interactable = false;
            itemDescription.text = "";
            itemPrice.text = "Cost: ";
            BuyEmitter.Play();
            PlayerStatsScript.instance.toolsUpgrades[4] = true;

        }
    }


    //INVENTORY BUYS

    private void BuyCommonSlot()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= inventoryPrices[0])
        {
            PlayerStatsScript.instance.commonSlots++;
            inventoryButtons[0].interactable = false;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-inventoryPrices[0]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            inventoryButtons[3].interactable = false;
            itemDescription.text = "";
            itemPrice.text = "Cost: ";
            BuyEmitter.Play();
            PlayerStatsScript.instance.inventroyUpgrades[0] = true;
        }
    }
    private void BuyRareSlot()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= inventoryPrices[1])
        {
            PlayerStatsScript.instance.rareSlots++;
            inventoryButtons[0].interactable = false;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-inventoryPrices[1]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            inventoryButtons[3].interactable = false;
            itemDescription.text = "";
            itemPrice.text = "Cost: ";
            BuyEmitter.Play();
            PlayerStatsScript.instance.inventroyUpgrades[1] = true;
        }
    }
    private void BuyLegendarySlot()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= inventoryPrices[2])
        {
            PlayerStatsScript.instance.legendarySlots++;
            inventoryButtons[2].interactable = false;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-inventoryPrices[2]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            inventoryButtons[3].interactable = false;
            itemDescription.text = "";
            itemPrice.text = "Cost: ";
            BuyEmitter.Play();
            PlayerStatsScript.instance.inventroyUpgrades[2] = true;
        }
    }

    //STATS BUYS

    private void BuyDivinePowerUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= statsPrices[0])
        {
            if (PlayerStatsScript.instance.unlocks[0] < 5) {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseDivinePowerMultiplyer += 0.3f / 5;
                PlayerStatsScript.instance.currentDivinePowerMultiplyer = PlayerStatsScript.instance.baseDivinePowerMultiplyer;
                BuyEmitter.Play();

                CoinCounterScript.coinCounterInstance.updateSCCounter(-statsPrices[0]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                unlockStep[0].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[0] + " / 5";
            }
            if(PlayerStatsScript.instance.unlocks[0] == 5)
            {
                statsButtons[6].interactable = false;
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                PlayerStatsScript.instance.statsUpgrades[0] = true;
                statsButtons[0].interactable = false;
            }
        }
    }

    private void BuyEssenceUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= statsPrices[1])
        {
            if (PlayerStatsScript.instance.unlocks[1] < 5) {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseEssenceMultiplyer += 0.3f/5;
                PlayerStatsScript.instance.currentEssenceMultiplyer = PlayerStatsScript.instance.baseEssenceMultiplyer;
                CoinCounterScript.coinCounterInstance.updateSCCounter(-statsPrices[1]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                BuyEmitter.Play();
                unlockStep[1].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[1] + " / 5";

            }
            if (PlayerStatsScript.instance.unlocks[1] == 5)
            {
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                statsButtons[6].interactable = false;
                statsButtons[1].interactable = false;
                PlayerStatsScript.instance.statsUpgrades[1] = true;
            }

        }
    }
    private void BuyBaseDamageDealedUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= statsPrices[2])
        {
            if (PlayerStatsScript.instance.unlocks[2] < 5) {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseDamageMultiplyer += 0.2f / 5;
                PlayerStatsScript.instance.currentDamageMultiplyer = PlayerStatsScript.instance.baseDamageMultiplyer;
                CoinCounterScript.coinCounterInstance.updateSCCounter(-statsPrices[2]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                BuyEmitter.Play();
                unlockStep[2].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[2] + " / 5";
            }
            if (PlayerStatsScript.instance.unlocks[2] == 5)
            {
                statsButtons[6].interactable = false;
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                PlayerStatsScript.instance.statsUpgrades[2] = true;
                statsButtons[2].interactable = false;
            }
            
        }
    }
    private void BuyBaseHealthUpgrade()
    {

        if (PlayerStatsScript.instance.currentSpecialCoin >= statsPrices[3])
        {
            if (PlayerStatsScript.instance.unlocks[3] < 5)
            {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseMaxHealthMultiplyer += 0.2f / 5;
                PlayerStatsScript.instance.currentMaxHealthMultiplyer = PlayerStatsScript.instance.baseMaxHealthMultiplyer;
                PlayerStatsScript.instance.currentHealth = PlayerStatsScript.instance.GetCurrentMaxHealth();
                CoinCounterScript.coinCounterInstance.updateSCCounter(-statsPrices[3]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                BuyEmitter.Play();
                unlockStep[3].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[3] + " / 5";
            }
            if (PlayerStatsScript.instance.unlocks[3] == 5)
            {
                statsButtons[6].interactable = false;
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                PlayerStatsScript.instance.statsUpgrades[3] = true;
                statsButtons[3].interactable = false;
            }
            HealthUIScript.instance.updateHealth(false);
        }
    }
    private void BuyBaseFirerateUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= statsPrices[4])
        {
            if (PlayerStatsScript.instance.unlocks[4] < 5)
            {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseFireRateMultiplyer += 0.2f / 5;
                PlayerStatsScript.instance.currentFireRateMultiplyer = PlayerStatsScript.instance.baseFireRateMultiplyer;
                CoinCounterScript.coinCounterInstance.updateSCCounter(-statsPrices[4]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                BuyEmitter.Play();
                unlockStep[4].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[4] + " / 5";
            }
            if(PlayerStatsScript.instance.unlocks[4] == 5)
            {
                statsButtons[6].interactable = false;
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                PlayerStatsScript.instance.statsUpgrades[4] = true;
                statsButtons[4].interactable = false;
            }

        }
    }
    private void BuySecondLifeUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= statsPrices[5])
        {
            PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
            PlayerStatsScript.instance.statsUpgrades[5] = true;
            statsButtons[5].interactable = false;
            PlayerStatsScript.instance.secondLifeUnlocked = true;
            PlayerStatsScript.instance.secondLife = true;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-statsPrices[5]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            BuyEmitter.Play();
            statsButtons[6].interactable = false;
            itemDescription.text = "";
            itemPrice.text = "Cost: ";
        }
    }



    
    
    
    public void changeTab(int tab)
    {
        statsButtons[6].interactable = false;
        itemDescription.text = "";
        itemPrice.text = "Cost: ";
        tabs[currentTab].SetActive(false);
        tabs[tab].SetActive(true);
        currentTab = tab;
    }


    private void updateToolsTab()
    {

    }
    private void updateInvetoryTab()
    {

    }
    private void updateStatsTab()
    {
        int index = 0;
        foreach(GameObject unlock in unlockStep)
        {
            unlock.GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[index] + " / 5";
            index++;
        }
    }

    public void CloseStore()
    {
        CloseEmitter.Play();
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
