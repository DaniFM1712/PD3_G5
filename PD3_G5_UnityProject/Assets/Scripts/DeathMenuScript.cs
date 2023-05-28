using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void restartButton() {
        LevelManager.levelManagerInstance.RestartGame(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
