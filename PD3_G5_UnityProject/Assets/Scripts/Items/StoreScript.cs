using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FMODUnity;

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


    [Header("StatsInfo")]
    [SerializeField] List<Button> statsButtons = new List<Button>();
    [SerializeField] List<int> statsPrices = new List<int>();
    [SerializeField] List<GameObject> unlockStep = new List<GameObject>();


    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] TextMeshProUGUI scCounter;
    private bool canShop = false;
    private int itemSelected = 0;

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
            foreach (bool boolean in PlayerStatsScript.instance.permanentUpgrades)
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
                        itemDescription.text = "Unlock Dash: a short and fast movement towards the direction you're looking.";
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
                itemPrice.text = "Cost: " + toolsPrices[index - 1];
                statsButtons[6].interactable = true;
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
                itemPrice.text = "Cost: " + statsPrices[index - 1];
                statsButtons[6].interactable = true;
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
        switch (itemSelected)
        {
            case 1:
                Debug.Log("BUY ITEM 1");
                BuyDivinePowerUpgrade();
                break;
            case 2:
                Debug.Log("BUY ITEM 2");
                BuyEssenceUpgrade();
                break;
            case 3:
                Debug.Log("BUY ITEM 3");
                BuyBaseDamageDealedUpgrade();
                break;
            case 4:
                Debug.Log("BUY ITEM 4");
                BuyBaseHealthUpgrade();
                break;
            case 5:
                Debug.Log("BUY ITEM 5");
                BuyBaseFirerateUpgrade();
                break;
            case 6:
                Debug.Log("BUY ITEM 6");
                BuySecondLifeUpgrade();
                break;
        }
    }



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
                PlayerStatsScript.instance.permanentUpgrades[0] = true;
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
                PlayerStatsScript.instance.permanentUpgrades[1] = true;
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
                PlayerStatsScript.instance.permanentUpgrades[2] = true;
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
                PlayerStatsScript.instance.permanentUpgrades[3] = true;
                statsButtons[3].interactable = false;
            }
            
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
                PlayerStatsScript.instance.permanentUpgrades[4] = true;
                statsButtons[4].interactable = false;
            }

        }
    }
    private void BuySecondLifeUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= statsPrices[5])
        {
            PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
            PlayerStatsScript.instance.permanentUpgrades[5] = true;
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
    private void buyDashUnlock()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= statsPrices[5])
        {
            PlayerStatsScript.instance.dashUnlocked = true;
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
        tabs[currentTab].SetActive(false);
        tabs[tab].SetActive(true);
        currentTab = tab;
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
