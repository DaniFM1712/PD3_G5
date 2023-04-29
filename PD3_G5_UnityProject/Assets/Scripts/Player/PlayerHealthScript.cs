using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PlayerHealthScript : MonoBehaviour
{
    private PlayerStatsScript playerStats;
    private HealthUIScript healthUI;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStatsScript.playerStatsInstance;
        healthUI = HealthUIScript.healthUIInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ModifyHealth(-20.0f);
        }
    }

    public void ModifyHealth(float modifier)
    {
        playerStats.currentHealth += modifier;
        playerStats.currentHealth = Mathf.Clamp(playerStats.currentHealth, 0, playerStats.currentMaxHealth);
        healthUI.updateHealth();

        if (playerStats.currentHealth == 0.0f)
        {
            Die();
        }
    }

    public void ModifyMaxHealth(float hpMaxPoints)
    {
        playerStats.currentMaxHealth += hpMaxPoints;
        healthUI.updateMaxHealth();
    }


    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(4);
    }

}