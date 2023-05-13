using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PlayerHealthScript : MonoBehaviour
{
    private HealthUIScript healthUI;

    // Start is called before the first frame update
    void Start()
    {
        healthUI = HealthUIScript.healthUIInstance;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void ModifyHealth(float modifier)
    {
        PlayerStatsScript.playerStatsInstance.currentHealth += modifier;
        PlayerStatsScript.playerStatsInstance.currentHealth = Mathf.Clamp(PlayerStatsScript.playerStatsInstance.currentHealth, 0, 
            PlayerStatsScript.playerStatsInstance.currentMaxHealth * PlayerStatsScript.playerStatsInstance.currentMaxHealthMultiplyer);

        healthUI.updateHealth();

        if (PlayerStatsScript.playerStatsInstance.currentHealth == 0.0f)
        {
            Die();
        }
    }

    public void ModifyMaxHealth(float hpMaxPoints)
    {
        PlayerStatsScript.playerStatsInstance.currentMaxHealth += hpMaxPoints;
    }


    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(4);
    }

}