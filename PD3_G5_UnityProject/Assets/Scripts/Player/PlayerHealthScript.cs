using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PlayerHealthScript : MonoBehaviour
{
    private PlayerStatsScript playerStats;
    [SerializeField] UnityEvent<float> updateHealth;
    [SerializeField] UnityEvent<float> updateMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStatsScript.playerStatsInstance;
        updateMaxHealth.Invoke(playerStats.currentMaxHealth);
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
        updateHealth.Invoke(playerStats.currentHealth);

        if (playerStats.currentHealth == 0.0f)
        {
            Die();
        }
    }

    public void ModifyMaxHealth(float hpMaxPoints)
    {
        playerStats.currentMaxHealth += hpMaxPoints;
        updateMaxHealth.Invoke(playerStats.currentMaxHealth);
    }


    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(4);
    }

}