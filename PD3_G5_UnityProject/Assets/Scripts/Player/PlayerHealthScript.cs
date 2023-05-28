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
        if (PlayerStatsScript.playerStatsInstance.highHealthDamageBuff && !PlayerStatsScript.playerStatsInstance.highHealthDamageApplied)
        {
            if (PlayerStatsScript.playerStatsInstance.currentHealth / PlayerStatsScript.playerStatsInstance.GetCurrentMaxHealth() >= 0.9)
            {
                PlayerStatsScript.playerStatsInstance.currentDamageMultiplyer += 0.2f;
                PlayerStatsScript.playerStatsInstance.highHealthDamageApplied = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void ModifyHealth(float modifier)
    {
        PlayerStatsScript.playerStatsInstance.currentHealth += modifier;
        PlayerStatsScript.playerStatsInstance.currentHealth = Mathf.Clamp(PlayerStatsScript.playerStatsInstance.currentHealth, 0, 
            PlayerStatsScript.playerStatsInstance.GetCurrentMaxHealth());

        if (PlayerStatsScript.playerStatsInstance.highHealthDamageBuff)
        {
            if (PlayerStatsScript.playerStatsInstance.currentHealth / PlayerStatsScript.playerStatsInstance.GetCurrentMaxHealth() >= 0.9 && !PlayerStatsScript.playerStatsInstance.highHealthDamageApplied)
            {
                PlayerStatsScript.playerStatsInstance.highHealthDamageApplied = true;
                PlayerStatsScript.playerStatsInstance.currentDamageMultiplyer += 0.2f;
            }
            else if(PlayerStatsScript.playerStatsInstance.currentHealth / PlayerStatsScript.playerStatsInstance.GetCurrentMaxHealth() < 0.9 && PlayerStatsScript.playerStatsInstance.highHealthDamageApplied)
            {
                PlayerStatsScript.playerStatsInstance.currentDamageMultiplyer -= 0.2f;
                PlayerStatsScript.playerStatsInstance.highHealthDamageApplied = false;
            }
        }

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
        SceneManager.LoadScene(3);
    }

}