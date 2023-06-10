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
    [SerializeField] ChangeConsumableParticles consumableParticles;

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
            StartCoroutine(ChestItemAppear());
            startOpenChestAnimation();
            Destroy(gameObject);
        }
    }

    void startOpenChestAnimation()
    {
        chestAnimator.SetBool("open", true);
        OpenChestEmitter.Play();
    }

    public void generateRandomReward()
    {
        ConsumableAsset asset;
        int randomNumber = Random.Range(0, 100);
        if(randomNumber <= 60)
        {
            asset = ItemPoolManagerScript.instance.GetCommonItem();
            consumableParticles.setItemCommon();
        }
        else if (randomNumber <= 90)
        {
            asset = ItemPoolManagerScript.instance.GetRareItem();
            consumableParticles.setItemRare();
        }
        else
        {
            asset = ItemPoolManagerScript.instance.GetLegendaryItem();
            consumableParticles.setItemLegendary();
        }
        consumableItem.SetConsumableItem(asset);
    }

    IEnumerator ChestItemAppear()
    {
        consumableItemGO.SetActive(true);
        yield return new WaitForSeconds(0f);

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
