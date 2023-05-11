using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Consumable : MonoBehaviour
{
    [SerializeField] private ConsumableAsset _consumableAsset;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject takeItemUI;
    private TextMeshProUGUI takeItemText;
    private bool canTake = false;
    // Start is called before the first frame update

    public void consume(GameObject consumer)
    {
        if (_consumableAsset.consume(consumer)) //si s'ha consumit, destruim
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (takeItemUI != null)
            takeItemText = takeItemUI.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E))
        {
            Canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            canTake = true;
            if (takeItemUI != null)
                takeItemText.enabled = true;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            canTake = false;
            if (takeItemUI != null)
                takeItemText.enabled = false;
        }
    }
}
