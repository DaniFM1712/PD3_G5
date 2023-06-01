using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NextLevelScript : MonoBehaviour
{

    private bool goNextLevel = false;
    private bool stopLoad = false;
    private TextMeshProUGUI exitLevelText;
    [SerializeField] GameObject exitLevel;
    private void Start()
    {
        if(exitLevel != null)
            exitLevelText = exitLevel.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (goNextLevel && Input.GetKeyDown(KeyCode.E) && PlayerStatsScript.instance.currentWeaponIndex != 0 && !stopLoad)
        {
            stopLoad = true;
            LevelManager.instance.LoadLevel();
            if (exitLevel != null)
                exitLevelText.enabled = false;
        }       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            goNextLevel = true;
            if (exitLevel != null)
                exitLevelText.enabled = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            goNextLevel = false;
            if (exitLevel != null)
                exitLevelText.enabled = false;
        }
    }
}
