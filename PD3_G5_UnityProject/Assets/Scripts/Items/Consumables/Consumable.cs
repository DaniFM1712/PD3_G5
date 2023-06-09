using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using TMPro;
using FMODUnity;

public class Consumable : MonoBehaviour
{
    [SerializeField] private ConsumableAsset _consumableAsset;
    [SerializeField] GameObject takeItemUI;
    [SerializeField] GameObject itemInfoUI;
    [SerializeField] GameObject nameUI;
    [SerializeField] GameObject descriptionUI;
    [SerializeField] GameObject backgroundUI;
    [SerializeField] GameObject slotsFullText;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;
    private bool canTake = false;
    
    [Header("FMOD")]
    public StudioEventEmitter TakeItemEmitter;
    public StudioEventEmitter LeaveItemEmitter;

    [Header("Particles")]
    [SerializeField] ChangeConsumableParticles consumableParticles;

    [Header("Colours")]
    [SerializeField] Color32 commonColour;
    [SerializeField] Color32 rareColour;
    [SerializeField] Color32 legendaryColour;

    // Start is called before the first frame update

    private void Start()
    {
        //_consumableAsset = new MaxHealthAsset();
        takeItemUI.SetActive(false);
        nameText = nameUI.GetComponent<TextMeshProUGUI>();
        descriptionText = descriptionUI.GetComponent<TextMeshProUGUI>();
        Debug.Log(_consumableAsset.rarity.ToString());
        switch (_consumableAsset.rarity)
        {
            case ConsumableAsset.Rarity.Common:
                consumableParticles.setItemCommon();
                break;

            case ConsumableAsset.Rarity.Rare:
                consumableParticles.setItemRare();
                break;
            case ConsumableAsset.Rarity.Legendary:
                consumableParticles.setItemLegendary();
                break;
        }

    }

    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E))
        {
            itemInfoUI.SetActive(true);
            setItemData();
            switch (_consumableAsset.rarity)
            {
                case ConsumableAsset.Rarity.Common:
                    backgroundUI.GetComponent<Image>().color = commonColour; // new Color32(0, 164, 255, 255);
                    break;

                case ConsumableAsset.Rarity.Rare:
                    backgroundUI.GetComponent<Image>().color = rareColour; // new Color32(138, 43, 226, 255);
                    break;

                case ConsumableAsset.Rarity.Legendary:
                    backgroundUI.GetComponent<Image>().color = legendaryColour; // new Color32(255, 182, 0, 255);
                    break;

            }
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void Take()
    {
        
        if (InventoryManagerScript.InventoryInstance.CanAddItem(_consumableAsset.rarity))
        {
            TakeItemEmitter.Play();
            InventoryManagerScript.InventoryInstance.AddItem(_consumableAsset.rarity, _consumableAsset);
            Destroy(gameObject);
        }
        else
        {
            LeaveItemEmitter.Play();
            slotsFullText.SetActive(true);
        }


    }

    public void Leave()
    {
        LeaveItemEmitter.Play();
        itemInfoUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        slotsFullText.SetActive(false);
    }

    public void SetConsumableItem(ConsumableAsset asset)
    {
        _consumableAsset = asset;
    }

    public void setItemData()
    {
        nameText.text = _consumableAsset.itemName;
        descriptionText.text = _consumableAsset.itemDescription;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTake = true;
            takeItemUI.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTake = false;
            takeItemUI.SetActive(false);
        }
    }


    private void OnDestroy()
    {
        takeItemUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
