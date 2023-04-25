using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuScript : MonoBehaviour
{
    public void restartButton() {
        LevelManager.levelManagerInstance.RestartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
