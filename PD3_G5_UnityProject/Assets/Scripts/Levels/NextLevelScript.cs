using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextLevelScript : MonoBehaviour
{

    private bool goNextLevel = false;

    private void Update()
    {
        if (goNextLevel && Input.GetKeyDown(KeyCode.E))
        {
            LevelManager.levelManagerInstance.LoadLevel();
        }       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            goNextLevel = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            goNextLevel = false;
    }
}
