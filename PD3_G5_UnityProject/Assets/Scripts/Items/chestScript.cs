using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    private bool canTake = false;
    [SerializeField] Consumable consumableItem;
    [SerializeField] GameObject consumableItemGO;
    
    [SerializeField] List<ConsumableAsset> commonItemPool;
    [SerializeField] List<ConsumableAsset> rareItemPool;
    [SerializeField] List<ConsumableAsset> legendaryItemPool;

    private void Start()
    {
        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E))
        {
            consumableItemGO.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void generateRandomReward()
    {
        ConsumableAsset asset = null;
        int randomNumber = Random.Range(0, 100);
        if(randomNumber <= 60)
        {
            int itemType = Random.Range(0, 5);
            switch (itemType)
            {
                case 1:
                    asset = commonItemPool[0];
                    break;
                case 2:
                    asset = commonItemPool[1];
                    break;
                case 3:
                    asset = commonItemPool[2];
                    break;
                case 4:
                    asset = commonItemPool[3];
                    break;
                case 5:
                    asset = commonItemPool[4];
                    break;
            }
        }
        else if (randomNumber <= 90)
        {
            int itemType = Random.Range(0, 5);
            switch (itemType)
            {
                case 1:
                    asset = rareItemPool[0];
                    break;
                case 2:
                    asset = rareItemPool[1];
                    break;
                case 3:
                    asset = rareItemPool[2];
                    break;
                case 4:
                    asset = rareItemPool[3];
                    break;
                case 5:
                    asset = rareItemPool[4];
                    break;
            }
        }
        else
        {
            int itemType = Random.Range(0, 5);
            switch (itemType)
            {
                case 1:
                    asset = legendaryItemPool[0];
                    break;
                case 2:
                    asset = legendaryItemPool[1];
                    break;
                case 3:
                    asset = legendaryItemPool[2];
                    break;
                case 4:
                    asset = legendaryItemPool[3];
                    break;
                case 5:
                    asset = legendaryItemPool[4];
                    break;
            }
        }
        Debug.Log(asset);
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
