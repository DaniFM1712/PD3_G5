using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    [SerializeField] GameObject storeCanvas;
    [SerializeField] TextMeshProUGUI scCounter;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] List<Button> buttons = new List<Button>();
    [SerializeField] List<int> prices = new List<int>();
    private bool canShop = false;
    private int itemSelected = 0;
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
        buttons[6].interactable = false;
        itemDescription.text = "";
        itemPrice.text = "Cost: ";

    }



    private void BuyDivinePowerUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[0])
        {
            PlayerStatsScript.instance.permanentUpgrades[0] = true;
            buttons[0].interactable = false;
            PlayerStatsScript.instance.baseDivinePowerMultiplyer += 0.3f;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[0]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
        }
    }

    private void BuyEssenceUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[1])
        {
            PlayerStatsScript.instance.permanentUpgrades[1] = true;
            buttons[1].interactable = false;
            PlayerStatsScript.instance.baseEssenceMultiplyer += 0.3f;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[1]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
        }
    }
    private void BuyBaseDamageDealedUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[2])
        {
            PlayerStatsScript.instance.permanentUpgrades[2] = true;
            buttons[2].interactable = false;
            PlayerStatsScript.instance.baseDamageMultiplyer += 0.2f;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[2]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
        }
    }
    private void BuyBaseHealthUpgrade()
    {

        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[3])
        {
            PlayerStatsScript.instance.permanentUpgrades[3] = true;
            buttons[3].interactable = false;
            PlayerStatsScript.instance.baseMaxHealthMultiplyer += 0.2f;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[3]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
        }
    }
    private void BuyBaseFirerateUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[4])
        {
            PlayerStatsScript.instance.permanentUpgrades[4] = true;
            buttons[4].interactable = false;
            PlayerStatsScript.instance.baseFireRateMultiplyer += 0.2f;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[4]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
        }
    }
    private void BuySecondLifeUpgrade()
    {
        if (PlayerStatsScript.instance.currentSpecialCoin >= prices[5])
        {
            PlayerStatsScript.instance.permanentUpgrades[5] = true;
            buttons[5].interactable = false;
            PlayerStatsScript.instance.secondLifeUnlocked = true;
            PlayerStatsScript.instance.secondLife = true;
            CoinCounterScript.coinCounterInstance.updateSCCounter(-prices[5]);
            scCounter.text = PlayerStatsScript.instance.currentSpecialCoin + "";
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
