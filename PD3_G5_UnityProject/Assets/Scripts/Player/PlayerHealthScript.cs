using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PlayerHealthScript : MonoBehaviour
{
    private HealthUIScript healthUI;
    private float dashHealBlessingTimer = 3f;
    private float currentDashHealTimer = 0f;
    private float accumulatedHeal = 0f;

    [Header("FMOD")]
    public StudioEventEmitter lowHpEmitter;
    public StudioEventEmitter muerteEmitter;
    public StudioEventEmitter recibirDañoEmitter;
    bool lifeActive = false;
    // Start is called before the first frame update
    void Start()
    {
        if (((PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth()) > 0.2f) || (PlayerStatsScript.instance.currentHealth <= 0))
        {
            lowHpEmitter.Stop();
        }
        else if (((PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth()) < 0.2f) && ((PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth()) > 0))
        {
            lowHpEmitter.Play();
        }
        currentDashHealTimer = dashHealBlessingTimer;
        healthUI = HealthUIScript.instance;
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
        if (PlayerStatsScript.instance.dashHealBlessing)
        {
            currentDashHealTimer -= Time.deltaTime;
            if(currentDashHealTimer <= 0)
            {
                currentDashHealTimer = dashHealBlessingTimer;
                accumulatedHeal = 0;
            }
        }   
    }

    public void ModifyHealth(float modifier)
    {
        if (modifier < 0)
        {
            recibirDañoEmitter.Play();
        }
        if (PlayerStatsScript.instance.dashHealBlessing && modifier<0)
        {
            accumulatedHeal += (modifier*-1);
        }
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
        if(((PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth()) > 0.2f) || (PlayerStatsScript.instance.currentHealth  <= 0))
        {
            lowHpEmitter.Stop();
        }
        else if (((PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth()) < 0.2f) && ((PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth()) > 0) && !lifeActive)
        {
            lowHpEmitter.Play();
            lifeActive = true;
        }


        if (PlayerStatsScript.instance.currentHealth == 0.0f)
        {
            muerteEmitter.Play();
            lowHpEmitter.Stop();
            Die();
        }

    }

    public void ModifyMaxHealth(float hpMaxPoints)
    {
        PlayerStatsScript.instance.currentMaxHealth += hpMaxPoints;
    }

    public void Die()
    {
        if (PlayerStatsScript.instance.secondLife)
        {
            PlayerStatsScript.instance.secondLife = false;
            ModifyHealth(PlayerStatsScript.instance.GetCurrentMaxHealth());
            LevelManager.instance.RestartLevel();
        }
        else
        {
            LevelManager.instance.GoToDeathMenu();
        }

    }

    public void ActivateDashHeal()
    {
        ModifyHealth(accumulatedHeal/2);
        accumulatedHeal = 0;
        currentDashHealTimer = dashHealBlessingTimer;
    }
}