using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseMenuScript : MonoBehaviour
{
    [SerializeField] KeyCode menuKey = KeyCode.Escape;
     GameObject menuPause;
    // Start is called before the first frame update
    void Start()
    {
        menuPause = GameObject.Find("CanvasPrefab/PauseMenu");
        menuPause.SetActive(false);
        DontDestroyOnLoad(menuPause);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(menuKey))
        {
            if (Time.timeScale == 1f)
            {
                ShowMenuUI();
            }
            else if(Time.timeScale == 0f && menuPause.activeSelf)
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
