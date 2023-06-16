using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManagerScript : MonoBehaviour
{
    public static ItemPoolManagerScript instance { get; private set; }

    // Start is called before the first frame update

    [SerializeField] List<ConsumableAsset> commonItemPool;
    [SerializeField] List<ConsumableAsset> rareItemPool;
    [SerializeField] List<ConsumableAsset> legendaryItemPool;    
    private List<ConsumableAsset> commonItemsAvailable;
    private List<ConsumableAsset> rareItemsAvailable;
    private List<ConsumableAsset> legendaryItemsAvailable;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        commonItemsAvailable = new List<ConsumableAsset>(commonItemPool);
        rareItemsAvailable = new List<ConsumableAsset>(rareItemPool);
        legendaryItemsAvailable = new List<ConsumableAsset>(legendaryItemPool);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public ConsumableAsset GetCommonItem()
    {
        ConsumableAsset asset = null;
        int itemIndex = Random.Range(0, commonItemsAvailable.Count);
        asset = commonItemsAvailable[itemIndex];

        if (!commonItemsAvailable[itemIndex].repeatable)
        {
            commonItemsAvailable.Remove(asset);
        }
        return asset;
    }
    public ConsumableAsset GetRareItem()
    {
        ConsumableAsset asset = null;
        int itemIndex = Random.Range(0, rareItemsAvailable.Count);
        asset = rareItemsAvailable[itemIndex];

        if (!rareItemsAvailable[itemIndex].repeatable)
        {
            rareItemsAvailable.Remove(asset);
        }
        return asset;
    }
    public ConsumableAsset GetLegendaryItem()
    {
        ConsumableAsset asset = null;
        int itemIndex = Random.Range(0, legendaryItemsAvailable.Count);
        asset = legendaryItemsAvailable[itemIndex];

        if (!legendaryItemsAvailable[itemIndex].repeatable)
        {
            legendaryItemsAvailable.Remove(asset);
        }
        return asset;
    }

}
