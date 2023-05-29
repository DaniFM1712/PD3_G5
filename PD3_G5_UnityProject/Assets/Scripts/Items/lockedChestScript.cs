using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lockedChestScript : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    private bool canTake = false;
    [SerializeField] Consumable consumableItem;
    [SerializeField] GameObject consumableItemGO;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI unableText;
    [SerializeField] int openPrice = 20;

    [SerializeField] List<ConsumableAsset> commonItemPool;
    [SerializeField] List<ConsumableAsset> rareItemPool;
    [SerializeField] List<ConsumableAsset> legendaryItemPool;

    private void Start()
    {
        Canvas.SetActive(false);
        costText.text = "COST: "+ openPrice;
        unableText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E) && PlayerStatsScript.instance.currentNormalCoin >= openPrice)
        {
            consumableItemGO.SetActive(true);
            Destroy(gameObject);
        }
        else if (canTake && Input.GetKeyDown(KeyCode.E) && PlayerStatsScript.instance.currentNormalCoin < openPrice)
        {
            unableText.enabled = true;
        }
    }

    public void generateRandomReward()
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
        else if (randomNumber <= 80)
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
        consumableItem.SetConsumableItem(asset);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTake = true;
            Canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTake = false;
            unableText.enabled = false;
            Canvas.SetActive(false);
        }

    }

    private void OnDestroy()
    {
        Canvas.SetActive(false);
    }


    private void OnEnable()
    {
        generateRandomReward();
    }

}
