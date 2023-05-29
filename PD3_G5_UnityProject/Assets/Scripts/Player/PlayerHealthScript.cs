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
        if (PlayerStatsScript.instance.highHealthDamageBuff && !PlayerStatsScript.instance.highHealthDamageApplied)
        {
            if (PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth() >= 0.9)
            {
                PlayerStatsScript.instance.currentDamageMultiplyer += 0.2f;
                PlayerStatsScript.instance.highHealthDamageApplied = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void ModifyHealth(float modifier)
    {
        PlayerStatsScript.instance.currentHealth += modifier;
        PlayerStatsScript.instance.currentHealth = Mathf.Clamp(PlayerStatsScript.instance.currentHealth, 0, 
            PlayerStatsScript.instance.GetCurrentMaxHealth());

        if (PlayerStatsScript.instance.highHealthDamageBuff)
        {
            if (PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth() >= 0.9 && !PlayerStatsScript.instance.highHealthDamageApplied)
            {
                PlayerStatsScript.instance.highHealthDamageApplied = true;
                PlayerStatsScript.instance.currentDamageMultiplyer += 0.2f;
            }
            else if(PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth() < 0.9 && PlayerStatsScript.instance.highHealthDamageApplied)
            {
                PlayerStatsScript.instance.currentDamageMultiplyer -= 0.2f;
                PlayerStatsScript.instance.highHealthDamageApplied = false;
            }
        }

        healthUI.updateHealth();

        if (PlayerStatsScript.instance.currentHealth == 0.0f)
        {
            Die();
        }
    }

    public void ModifyMaxHealth(float hpMaxPoints)
    {
        PlayerStatsScript.instance.currentMaxHealth += hpMaxPoints;
    }


    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);
    }

}