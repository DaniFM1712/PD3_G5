using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void onStartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void onSettings()
    {

    }

    public void onQuit()
    {
        Application.Quit();
    }
}
