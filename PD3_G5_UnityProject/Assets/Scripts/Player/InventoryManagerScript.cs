using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerScript : MonoBehaviour
{
    public static InventoryManagerScript InventoryInstance { get; private set; }

    private List<ConsumableAsset> commonItems;
    private List<ConsumableAsset> rareItems;
    private List<ConsumableAsset> legendaryItems;

    [SerializeField] GameObject inventoryBackground;

    [SerializeField] List<GameObject> commonItemsSlots;
    [SerializeField] List<GameObject> rareItemsSlots;
    [SerializeField] List<GameObject> legendaryItemsSlots;
    public enum Rarity { Common, Rare, Legendary };

    //1 ITEM:
    //1 remove buttons
    //1 backgrounds (no cal pq sempre estaràn allà)
    //1 imatges
    //2 textos (name + descrip)


    //3 Common Items GO

    //2 Rare Items GO

    //1 Legendary Items GO

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("1. TAB PRESSED");
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !inventoryBackground.activeSelf)
        {
            Debug.Log("2. TAB PRESSED");
            inventoryBackground.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

        else if(Input.GetKeyDown(KeyCode.Tab) && inventoryBackground.activeSelf)
        {
            Debug.Log("3. TAB PRESSED");
            inventoryBackground.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void UpdateInventoryUI()
    {
        int index = 0;
        foreach(ConsumableAsset item in commonItems)
        {
            commonItemsSlots[index].SetActive(true);
            //commonItemsSlots[index].SetItemData();
            index++;
        }

        index = 0;
        foreach (ConsumableAsset item in rareItems)
        {
            rareItemsSlots[index].SetActive(true);
            //rareItemsSlots[index].SetItemData();
            index++;
        }

        index = 0;
        foreach (ConsumableAsset item in legendaryItems)
        {
            legendaryItemsSlots[index].SetActive(true);
            //legendaryItemsSlots[index].SetItemData();
            index++;
        }
    }



    public bool CanAddItem(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return commonItems.Count < 3;

            case Rarity.Rare:
                return rareItems.Count < 2;

            case Rarity.Legendary:
                return legendaryItems.Count < 1;

            default:
                return false;
        }

    }

    public void AddItem(Rarity rarity, ConsumableAsset asset)
    {
        switch (rarity)
        {
            case Rarity.Common:
                commonItems.Add(asset);
                break;

            case Rarity.Rare:
                rareItems.Add(asset);
                break;

            case Rarity.Legendary:
                legendaryItems.Add(asset);
                break;

            default:
                break;
        }
    }

    public void ResetInventory()
    {
        commonItems.Clear();
        rareItems.Clear();
        legendaryItems.Clear();
    }

    public void RemoveCommonItem(int slot)
    {
        switch (slot)
        {
            case 1:
                commonItemsSlots.RemoveAt(0);
                break;
            case 2:
                commonItemsSlots.RemoveAt(1);
                break;
            case 3:
                commonItemsSlots.RemoveAt(2);
                break;
        }
    }

    public void RemoveRareItem(int slot)
    {
        switch (slot)
        {
            case 1:
                rareItemsSlots.RemoveAt(0);
                break;
            case 2:
                rareItemsSlots.RemoveAt(1);
                break;
        }
    }

    public void RemoveLegendarayItem()
    {
        legendaryItemsSlots.Clear();
    }
}
