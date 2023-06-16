using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonControllerScript : MonoBehaviour
{

    Button resumeBtn;
    Button menuBtn;


    [SerializeField] KeyCode menuKey = KeyCode.Escape;
    GameObject pauseBG;
    GameObject pauseUI;

    [Header("FMOD")]
    public StudioEventEmitter CancelEmitter;
    public StudioEventEmitter SelectEmitter;


    // Start is called before the first frame update
    void Start()
    {

        resumeBtn = GameObject.Find("CanvasPrefab/PauseMenu/PauseUI/ResumeButton").GetComponent<Button>();
        menuBtn = GameObject.Find("CanvasPrefab/PauseMenu/PauseUI/BackToMenuButton").GetComponent<Button>();
        pauseBG = GameObject.Find("CanvasPrefab/PauseMenu/BackgroundImage");
        pauseUI = GameObject.Find("CanvasPrefab/PauseMenu/PauseUI");

        resumeBtn.onClick.AddListener(delegate () { HideMenuUI(); });
        menuBtn.onClick.AddListener(delegate () { GoToMainMenu(); });

        Debug.Log("PAUSEBG: "+pauseBG);
        pauseBG.SetActive(false);

    }

    void Update()
    {

        if (Input.GetKeyDown(menuKey))
        {
            if (Time.timeScale == 1f)
            {
                ShowMenuUI();
            }
            else if (Time.timeScale == 0f && pauseBG.activeSelf)
            {
                HideMenuUI();
            }
        }
    }


    public void ShowMenuUI()
    {
        pauseBG.SetActive(true);

        LeanTween.moveLocal(pauseUI, new Vector3(0f, 0f, 0f), 0.5f).setOnStart(() => {
            //ShowMenuUI();
            CancelEmitter.Play();
            SelectEmitter.Play();
        }).setOnComplete(() => {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        });


    }
    public void HideMenuUI()
    {
        Time.timeScale = 1f;
        LeanTween.moveLocal(pauseUI, new Vector3(0f, 1050f, 0f), 0.5f).setOnStart(() => {
            CancelEmitter.Play();
            Cursor.lockState = CursorLockMode.Locked;
        }).setOnComplete(() => {
            pauseBG.SetActive(false);
        });
    }

    public void ShowSettingsUI()
    {
        pauseBG.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        SelectEmitter.Play();
    }

    public void GoToMainMenu()
    {
        PlayerStatsScript.instance.ResetStats();
        InventoryManagerScript.InventoryInstance.ResetInventory();
        
        HideMenuUI();
        LevelManager.instance.RestartGame(0);
        SelectEmitter.Play();
    }
}
