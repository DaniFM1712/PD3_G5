using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonControllerScript : MonoBehaviour
{

    Button resume;
    Button settings;
    Button menu;


    [SerializeField] KeyCode menuKey = KeyCode.Escape;
    GameObject menuPause;

    // Start is called before the first frame update
    void Start()
    {

        resume = GameObject.Find("CanvasPrefab/PauseMenu/ResumeButton").GetComponent<Button>();
        settings = GameObject.Find("CanvasPrefab/PauseMenu/SettingsButton").GetComponent<Button>();
        menu = GameObject.Find("CanvasPrefab/PauseMenu/BackToMenuButton").GetComponent<Button>();
        menuPause = GameObject.Find("CanvasPrefab/PauseMenu");

        resume.onClick.AddListener(delegate () { HideMenuUI(); });
        settings.onClick.AddListener(delegate () { ShowSettingsUI(); });
        menu.onClick.AddListener(delegate () { GoToMainMenu(); });


        menuPause.SetActive(false);

    }

    void Update()
    {

        if (Input.GetKeyDown(menuKey))
        {
            if (Time.timeScale == 1f)
            {
                ShowMenuUI();
            }
            else if (Time.timeScale == 0f && menuPause.activeSelf)
            {
                HideMenuUI();
            }
        }
    }


    public void ShowMenuUI()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
    public void HideMenuUI()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowSettingsUI()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoToMainMenu()
    {
        PlayerStatsScript.instance.ResetStats();
        InventoryManagerScript.InventoryInstance.ResetInventory();
        
        HideMenuUI();
        LevelManager.instance.RestartGame(0);

    }

}
