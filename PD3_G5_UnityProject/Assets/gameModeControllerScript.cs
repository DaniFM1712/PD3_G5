using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameModeControllerScript : MonoBehaviour
{
    [SerializeField] GameObject canvasGameMode;
    [SerializeField] GameObject buttonNoc;
    [SerializeField] GameObject buttonCao;
    [SerializeField] GameObject portalCao;
    [SerializeField] GameObject portalNoc;
    [SerializeField] GameObject portalDiu;
    [SerializeField] GameObject canvas;
    private bool canChange = false;

    [Header("FMOD")]
    public StudioEventEmitter SelectEmitter;
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
        SelectEmitter.Play();
        switch (mode)
        {
            case 0:
                portalDiu.SetActive(true);
                portalNoc.SetActive(false);
                portalCao.SetActive(false);
                break;
            case 1:
                portalDiu.SetActive(false);
                portalNoc.SetActive(true);
                portalCao.SetActive(false);
                break;
            case 2:
                portalDiu.SetActive(false);
                portalNoc.SetActive(false);
                portalCao.SetActive(true);
                break;
        }
        //SETEAR TEMA DE PORTALES
    }
    

    private void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
        canChange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
        canChange = false;
    }



}
