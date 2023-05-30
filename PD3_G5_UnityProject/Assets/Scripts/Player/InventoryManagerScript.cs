using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManagerScript : MonoBehaviour
{
    public static InventoryManagerScript InventoryInstance { get; private set; }

    //SCRIPTS
    private List<ConsumableAsset> commonItems;
    private List<ConsumableAsset> rareItems;
    private List<ConsumableAsset> legendaryItems;

    [SerializeField] GameObject itemsInventory;
    [SerializeField] GameObject blessingsInventory;


    //UI
    [Header("Objects")]
    [SerializeField] List<GameObject> commonItemsSlots;
    [SerializeField] List<GameObject> rareItemsSlots;
    [SerializeField] List<GameObject> legendaryItemsSlots;
    
    [Header("Blessings")]
    [SerializeField] List<ParentBlessing> blessings;
    [SerializeField] TextMeshProUGUI dashBlessings;
    [SerializeField] TextMeshProUGUI weaponsBlessings;
    [SerializeField] TextMeshProUGUI grenadeBlessings;





    private void Awake()
    {
        if (InventoryInstance == null)
        {
            InventoryInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        commonItems = new List<ConsumableAsset>();
        rareItems = new List<ConsumableAsset>();
        legendaryItems = new List<ConsumableAsset>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && !itemsInventory.activeSelf && Time.timeScale == 1f)
        {
            ShowInventoryUI();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

        else if(Input.GetKeyDown(KeyCode.Tab) && (itemsInventory.activeSelf || blessingsInventory.activeSelf))
        {
            HideInventoryUI();
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ShowInventoryUI()
    {
        itemsInventory.SetActive(true);

        int index = 0;
        foreach (ConsumableAsset item in commonItems)
        {
            commonItemsSlots[index].SetActive(true);
            index++;
        }

        index = 0;
        foreach (ConsumableAsset item in rareItems)
        {
            rareItemsSlots[index].SetActive(true);
            index++;
        }

        index = 0;
        foreach (ConsumableAsset item in legendaryItems)
        {
            legendaryItemsSlots[index].SetActive(true);
            index++;
        }
    }

    private void HideInventoryUI()
    {
        foreach (GameObject itemUI in commonItemsSlots)
        {
            itemUI.SetActive(false);
        }

        foreach (GameObject itemUI in rareItemsSlots)
        {
            itemUI.SetActive(false);
        }

        foreach (GameObject itemUI in legendaryItemsSlots)
        {
            itemUI.SetActive(false);
        }
        itemsInventory.SetActive(false);
        blessingsInventory.SetActive(false);
    }

    public void UpdateItemsUI()
    {
        int index = 0;
        foreach(GameObject slot in commonItemsSlots)
        {
            if (commonItems.Count > index)
            {
                slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = commonItems[index].itemName;
                slot.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = commonItems[index].itemDescription;
                slot.transform.Find("ItemBackground").GetComponent<Image>().color = Color.blue;

                index++;
            }
            else
            {
                slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = "";
                slot.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = "";
                slot.SetActive(false);
            }
        }

        index = 0;
        foreach (GameObject slot in rareItemsSlots)
        {
            if (rareItems.Count > index)
            {
                slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = rareItems[index].itemName;
                slot.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = rareItems[index].itemDescription;
                slot.transform.Find("ItemBackground").GetComponent<Image>().color = new Color32(138, 43, 226, 255);
                index++;
            }
            else
            {
                slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = "";
                slot.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = "";
                slot.SetActive(false);
            }
        }
        index = 0;
        foreach (GameObject slot in legendaryItemsSlots)
        {
            if (legendaryItems.Count > index)//commonItems.count < index
            {
                slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = legendaryItems[index].itemName;
                slot.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = legendaryItems[index].itemDescription;
                slot.transform.Find("ItemBackground").GetComponent<Image>().color = Color.yellow;
                index++;
            }
            else
            {
                slot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = "";
                slot.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = "";
                slot.SetActive(false);
            }
        }
    }

    public void UpdateBlessingsUI()
    {
        ParentBlessing[] blessings = FindObjectsByType<ParentBlessing>(FindObjectsSortMode.InstanceID);
        foreach (ParentBlessing blessing in blessings)
        {

            if (blessing.enabled)
            {
                switch (blessing.blessingType)
                {
                    case ParentBlessing.BlessingType.Dash:
                        dashBlessings.text += "� " + blessing.blessingName + "\n";
                        break;
                    case ParentBlessing.BlessingType.RapidFire:
                        weaponsBlessings.text += "� " + blessing.blessingName + "\n";
                        break;
                    case ParentBlessing.BlessingType.ShotGun:
                        weaponsBlessings.text += "� " + blessing.blessingName + "\n";
                        break;
                    case ParentBlessing.BlessingType.Grenade:
                        grenadeBlessings.text += "� " + blessing.blessingName + "\n";
                        break;
                } 
            }
        }
    }



    public bool CanAddItem(ConsumableAsset.Rarity rarity)
    {
        switch (rarity)
        {
            case ConsumableAsset.Rarity.Common:
                return commonItems.Count < 3;

            case ConsumableAsset.Rarity.Rare:
                return rareItems.Count < 2;

            case ConsumableAsset.Rarity.Legendary:
                return legendaryItems.Count < 1;

            default:
                return false;
        }

    }

    public void AddItem(ConsumableAsset.Rarity rarity, ConsumableAsset asset)
    {
        switch (rarity)
        {
            case ConsumableAsset.Rarity.Common:
                commonItems.Add(asset);
                break;

            case ConsumableAsset.Rarity.Rare:
                rareItems.Add(asset);
                break;

            case ConsumableAsset.Rarity.Legendary:
                legendaryItems.Add(asset);
                break;

            default:
                break;
        }

        asset.consume();
        HealthUIScript.healthUIInstance.updateHealth();
        UpdateItemsUI();
    }

    public void ResetInventory()
    {
        foreach (ConsumableAsset item in commonItems)
        {
            item.drop();
        }

        foreach (ConsumableAsset item in rareItems)
        {
            item.drop();
        }

        foreach (ConsumableAsset item in legendaryItems)
        {
            item.drop();
        }

        commonItems.Clear();
        rareItems.Clear();
        legendaryItems.Clear();

        foreach (GameObject itemUI in commonItemsSlots)
        {
            itemUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = "";
            itemUI.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = "";
            itemUI.SetActive(false);
        }

        foreach (GameObject itemUI in rareItemsSlots)
        {
            itemUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = "";
            itemUI.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = "";
            itemUI.SetActive(false);
        }

        foreach (GameObject itemUI in legendaryItemsSlots)
        {
            itemUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = "";
            itemUI.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = "";
            itemUI.SetActive(false);
        }
        dashBlessings.text = "";
        weaponsBlessings.text = ""; 
        grenadeBlessings.text = "";

    }

    public void RemoveCommonItem(int slot)
    {
        Debug.Log("REMOVE COMMON ITEM");
        commonItems[slot-1].drop();
       
        commonItemsSlots[slot - 1].transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = "";
        commonItemsSlots[slot - 1].transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = "";

        commonItems.RemoveAt(slot - 1);

        HealthUIScript.healthUIInstance.updateHealth();
        UpdateItemsUI();
    }

    public void RemoveRareItem(int slot)
    {
        rareItems[slot - 1].drop();   

        rareItemsSlots[slot - 1].transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = "";
        rareItemsSlots[slot - 1].transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = "";

        rareItems.RemoveAt(slot - 1);

        HealthUIScript.healthUIInstance.updateHealth();
        UpdateItemsUI();

    }

    public void RemoveLegendarayItem()
    {
        legendaryItems[0].drop();
        
        legendaryItemsSlots[0].transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = "";
        legendaryItemsSlots[0].transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = "";

        legendaryItems.Clear();

        HealthUIScript.healthUIInstance.updateHealth();
        UpdateItemsUI();
    }

    public bool IsInventoryFull()
    {
        return commonItems.Count == 3 && rareItems.Count == 2 && legendaryItems.Count == 1;
    }

    public void switchInventoryTabsToInv()
    {
        if (!itemsInventory.activeSelf)
        {
            blessingsInventory.SetActive(false);
            itemsInventory.SetActive(true);
        }
    }
    public void switchInventoryTabsToBlessings()
    {
        if (!blessingsInventory.activeSelf)
        {
            blessingsInventory.SetActive(true);
            itemsInventory.SetActive(false);
        }
    }
}
