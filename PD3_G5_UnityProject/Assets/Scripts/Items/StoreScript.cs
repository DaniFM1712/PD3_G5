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
    
    
    
    [Header("StatsInfo")]
    [SerializeField] List<Button> buttons = new List<Button>();
    [SerializeField] List<int> prices = new List<int>();
    [SerializeField] List<GameObject> unlockStep = new List<GameObject>();
    [SerializeField] TextMeshProUGUI scCounter;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] TextMeshProUGUI itemPrice;


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
                    buttons[i].interactable = false;
                }
                i++;
            }

            buttons[6].interactable = false;
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
        SelectEmitter.Play();
        itemPrice.text ="Cost: " + prices[index - 1];
        buttons[6].interactable = true;
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
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[0])
        {
            if (PlayerStatsScript.instance.unlocks[0] < 5) {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseDivinePowerMultiplyer += 0.3f / 5;
                PlayerStatsScript.instance.currentDivinePowerMultiplyer = PlayerStatsScript.instance.baseDivinePowerMultiplyer;
                BuyEmitter.Play();

                CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[0]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                unlockStep[0].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[0] + " / 5";
            }
            if(PlayerStatsScript.instance.unlocks[0] == 5)
            {
                buttons[6].interactable = false;
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                PlayerStatsScript.instance.permanentUpgrades[0] = true;
                buttons[0].interactable = false;
            }
        }
    }

    private void BuyEssenceUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[1])
        {
            if (PlayerStatsScript.instance.unlocks[1] < 5) {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseEssenceMultiplyer += 0.3f/5;
                PlayerStatsScript.instance.currentEssenceMultiplyer = PlayerStatsScript.instance.baseEssenceMultiplyer;
                CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[1]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                BuyEmitter.Play();
                unlockStep[1].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[1] + " / 5";

            }
            if (PlayerStatsScript.instance.unlocks[1] == 5)
            {
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                buttons[6].interactable = false;
                buttons[1].interactable = false;
                PlayerStatsScript.instance.permanentUpgrades[1] = true;
            }

        }
    }
    private void BuyBaseDamageDealedUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[2])
        {
            if (PlayerStatsScript.instance.unlocks[2] < 5) {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseDamageMultiplyer += 0.2f / 5;
                PlayerStatsScript.instance.currentDamageMultiplyer = PlayerStatsScript.instance.baseDamageMultiplyer;
                CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[2]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                BuyEmitter.Play();
                unlockStep[2].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[2] + " / 5";
            }
            if (PlayerStatsScript.instance.unlocks[2] == 5)
            {
                buttons[6].interactable = false;
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                PlayerStatsScript.instance.permanentUpgrades[2] = true;
                buttons[2].interactable = false;
            }
            
        }
    }
    private void BuyBaseHealthUpgrade()
    {

        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[3])
        {
            if (PlayerStatsScript.instance.unlocks[3] < 5)
            {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseMaxHealthMultiplyer += 0.2f / 5;
                PlayerStatsScript.instance.currentMaxHealthMultiplyer = PlayerStatsScript.instance.baseMaxHealthMultiplyer;
                PlayerStatsScript.instance.currentHealth = PlayerStatsScript.instance.GetCurrentMaxHealth();
                CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[3]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                BuyEmitter.Play();
                unlockStep[3].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[3] + " / 5";
            }
            if (PlayerStatsScript.instance.unlocks[3] == 5)
            {
                buttons[6].interactable = false;
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                PlayerStatsScript.instance.permanentUpgrades[3] = true;
                buttons[3].interactable = false;
            }
            
        }
    }
    private void BuyBaseFirerateUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[4])
        {
            if (PlayerStatsScript.instance.unlocks[4] < 5)
            {
                PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
                PlayerStatsScript.instance.baseFireRateMultiplyer += 0.2f / 5;
                PlayerStatsScript.instance.currentFireRateMultiplyer = PlayerStatsScript.instance.baseFireRateMultiplyer;
                CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[4]);
                scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
                BuyEmitter.Play();
                unlockStep[4].GetComponent<TextMeshProUGUI>().text = PlayerStatsScript.instance.unlocks[4] + " / 5";
            }
            if(PlayerStatsScript.instance.unlocks[4] == 5)
            {
                buttons[6].interactable = false;
                itemDescription.text = "";
                itemPrice.text = "Cost: ";
                PlayerStatsScript.instance.permanentUpgrades[4] = true;
                buttons[4].interactable = false;
            }

        }
    }
    private void BuySecondLifeUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[5])
        {
            PlayerStatsScript.instance.unlocks[itemSelected - 1]++;
            PlayerStatsScript.instance.permanentUpgrades[5] = true;
            buttons[5].interactable = false;
            PlayerStatsScript.instance.secondLifeUnlocked = true;
            PlayerStatsScript.instance.secondLife = true;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[5]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
            BuyEmitter.Play();
            buttons[6].interactable = false;
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
