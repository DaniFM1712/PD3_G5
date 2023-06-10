using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorePanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject loreGO;
    [SerializeField] GameObject interactMessageGO;
    bool canInteract = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            loreGO.SetActive(true);
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
}
