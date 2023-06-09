using FMODUnity;
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
    [SerializeField] Animator chestAnimator;

    [Header("FMOD")]
    public StudioEventEmitter OpenChestEmitter;


    private void Start()
    {
        Canvas.SetActive(false);
        costText.text = "COST: "+ openPrice;
        unableText.enabled = false;
        chestAnimator = transform.GetComponentInParent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E) && PlayerStatsScript.instance.currentNormalCoin >= openPrice)
        {
            CoinCounterScript.coinCounterInstance.updateNCCounter(-openPrice);
            consumableItemGO.SetActive(true);
            startOpenChestAnimation();
            Destroy(gameObject);
        }
        else if (canTake && Input.GetKeyDown(KeyCode.E) && PlayerStatsScript.instance.currentNormalCoin < openPrice)
        {
            unableText.enabled = true;
        }
    }

    void startOpenChestAnimation()
    {
        chestAnimator.SetBool("open", true);
        OpenChestEmitter.Play();
    }

    public void generateRandomReward()
    {
        ConsumableAsset asset = null;
        int randomNumber = Random.Range(0, 100);
        if (randomNumber <= 50)
        {
            asset = ItemPoolManagerScript.instance.GetCommonItem();
        }
        else if (randomNumber <= 80)
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
