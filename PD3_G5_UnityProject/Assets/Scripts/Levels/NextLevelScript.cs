using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using FMODUnity;

public class NextLevelScript : MonoBehaviour
{

    private bool goNextLevel = false;
    private bool stopLoad = false;
    private TextMeshProUGUI exitLevelText;
    [SerializeField] GameObject exitLevel;
    [Header("FMOD")]
    public StudioEventEmitter ExitLevelEmitter;
    [Header("Particles")]
    [SerializeField] ParticleSystem particles;
    private void Start()
    {
        if(exitLevel != null)
            exitLevelText = exitLevel.GetComponent<TextMeshProUGUI>();
        particles.Play();
    }

    private void Update()
    {
        if (goNextLevel && Input.GetKeyDown(KeyCode.E) && PlayerStatsScript.instance.currentWeaponIndex != 0 && !stopLoad)
        {

            ExitLevelEmitter.Play();
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
