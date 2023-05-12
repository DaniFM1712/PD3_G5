using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerScript : MonoBehaviour
{
    public static InventoryManagerScript inventoryInstance { get; private set; }

    private List<ConsumableAsset> commonItems;
    private List<ConsumableAsset> rareItems;
    private List<ConsumableAsset> legendaryItems;

    private List<GameObject> commonItemsSlots;
    private List<GameObject> rareItemsSlots;
    private List<GameObject> legendaryItemsSlots;
    public enum Rarity { Common, Rare, Legendary };

    //1 ITEM:
    //1 remove buttons
    //1 backgrounds (no cal pq sempre estaràn allà)
    //1 imatges
    //2 textos (name + descrip)


    //4 Common Items GO

    //3 Rare Items GO

    //1 Legendary Items GO

    private void Awake()
    {
        if (inventoryInstance == null)
        {
            inventoryInstance = this;
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
                return commonItems.Count < 4;

            case Rarity.Rare:
                return rareItems.Count < 3;

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
}
