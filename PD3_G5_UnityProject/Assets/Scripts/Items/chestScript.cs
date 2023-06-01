using FMODUnity;
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
    [SerializeField] Animator chestAnimator;

    [Header("FMOD")]
    public StudioEventEmitter OpenChestEmitter;

    private void Start()
    {
        Canvas.SetActive(false);
        chestAnimator = transform.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E))
        {
            consumableItemGO.SetActive(true);
            startOpenChestAnimation();
            Destroy(gameObject);
        }
    }

    void startOpenChestAnimation()
    {
        chestAnimator.SetBool("open", true);
        OpenChestEmitter.Play();
        StartCoroutine(ChestItemAppear());
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

    IEnumerator ChestItemAppear()
    {
        int i = 0;
        while (i < 10f)
        {
            consumableItemGO.transform.position = new Vector3(consumableItemGO.transform.position.x, consumableItemGO.transform.position.y+1f, consumableItemGO.transform.position.z);
            yield return new WaitForEndOfFrame();
            i++;
        }
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
