using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LorePanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject loreUIGO;
    [SerializeField] GameObject interactMessageGO;
    [SerializeField] GameObject scriptureModel;
    [SerializeField] GameObject pickableModel;
    [SerializeField] GameObject canvasLore;
    [SerializeField] int panelIndex;
    public bool unlocked;//SOLO PARA TEST

    bool canInteract = false;
    void Start()
    {
        //unlocked = playerstats.instance.lore[panelIndex]
        if (PlayerStatsScript.instance.loreUnlocked[panelIndex] == true) { 
            scriptureModel.SetActive(true); 
            pickableModel.SetActive(true);
            interactMessageGO.GetComponent<TextMeshProUGUI>().text = "Press E to read!";
         }

    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && PlayerStatsScript.instance.loreUnlocked[panelIndex] == true && Input.GetKeyDown(KeyCode.E))
        {
            loreUIGO.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactMessageGO.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactMessageGO.SetActive(false);
            canInteract = false;
        }
    }

    public void ClosePanel()
    {
        loreUIGO.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
