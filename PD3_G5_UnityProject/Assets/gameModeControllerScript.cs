using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameModeControllerScript : MonoBehaviour
{
    [SerializeField] GameObject canvasGameMode;
    [SerializeField] GameObject buttonNoc;
    [SerializeField] GameObject buttonCao;
    private bool canChange = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canChange && Input.GetKey(KeyCode.E))
        {
            canvasGameMode.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            if (PlayerStatsScript.instance.nocturnalUnlocked)
            {
                buttonNoc.GetComponent<Button>().interactable = true;
            }
            if (PlayerStatsScript.instance.caoticUnlocked)
            {
                buttonCao.GetComponent<Button>().interactable = true;
            }
        }
    }
    
    public void setGameMode(int mode)
    {
        LevelManager.instance.currentGameMode = mode;
        canvasGameMode.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        //SETEAR TEMA DE PORTALES
    }
    

    private void OnTriggerEnter(Collider other)
    {
        canChange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        canChange = false;
    }



}
