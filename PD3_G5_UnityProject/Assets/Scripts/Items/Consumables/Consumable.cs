using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Consumable : MonoBehaviour
{
    [SerializeField] private ConsumableAsset _consumableAsset;
    [SerializeField] GameObject takeItemUI;
    [SerializeField] GameObject itemInfoUI;

    private bool canTake = false;
    // Start is called before the first frame update

    private void Start()
    {
        //_consumableAsset = new MaxHealthAsset();
        takeItemUI.SetActive(false);
    }

    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E))
        {
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

    private void OnDestroy()
    {
        takeItemUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Leave()
    {
        itemInfoUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
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

    public void SetConsumableItem(ConsumableAsset asset)
    {
        _consumableAsset = asset;
    }
}
