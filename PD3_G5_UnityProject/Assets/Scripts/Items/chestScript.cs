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
        ConsumableAsset asset;
        int randomNumber = Random.Range(0, 100);
        if(randomNumber <= 60)
        {
            asset = ItemPoolManagerScript.instance.GetCommonItem(); 
        }
        else if (randomNumber <= 90)
        {
            asset = ItemPoolManagerScript.instance.GetRareItem();
        }
        else
        {
            asset = ItemPoolManagerScript.instance.GetLegendaryItem();
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
