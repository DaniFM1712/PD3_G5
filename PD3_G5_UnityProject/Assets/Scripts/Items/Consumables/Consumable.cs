using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Consumable : MonoBehaviour
{
    [SerializeField] private ConsumableAsset _consumableAsset;
    [SerializeField] GameObject takeItemUI;
    [SerializeField] GameObject itemInfoUI;
    [SerializeField] GameObject nameUI;
    [SerializeField] GameObject descriptionUI;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;

    private bool canTake = false;
    // Start is called before the first frame update

    private void Start()
    {
        //_consumableAsset = new MaxHealthAsset();
        takeItemUI.SetActive(false);
        nameText = nameUI.GetComponent<TextMeshProUGUI>();
        descriptionText = descriptionUI.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E))
        {
            setItemData();
            itemInfoUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void Take()
    {

        Debug.Log("CONSUME");
        GameObject player = GameObject.Find("Player");
        if (_consumableAsset.consume())
        {
            Destroy(gameObject);
        }
    }



    public void Leave()
    {
        itemInfoUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
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
