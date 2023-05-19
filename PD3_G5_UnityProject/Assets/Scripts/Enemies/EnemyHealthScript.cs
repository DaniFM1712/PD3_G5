using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] float InitialHealth;
    float currentHealth;
    [SerializeField] Image healthBar;
    [SerializeField] Gradient colorGradient;
    
    private Canvas canvas;

    private void Awake()
    {
        currentHealth = InitialHealth;
    }
    private void Start()
    {
        canvas = GameObject.Find("EnemyHealthCanvas").GetComponent<Canvas>();
        healthBar.gameObject.transform.SetParent(canvas.transform);
        healthBar.GetComponent<EnemyHealthBarScript>().target = transform; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddInitialHealth(float healthIncresed)
    {
        InitialHealth += healthIncresed;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(currentHealth);
        if (PlayerStatsScript.playerStatsInstance.dashDamageBlessing)
        {
            damage *= 1.3f;
        }
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth/InitialHealth;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        //ANIMATION
        //SOUND
        Destroy(healthBar.gameObject);
        Destroy(gameObject);

    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void OnDestroy()
    {
        if(GetCurrentHealth() <= 0)
        {
            CoinCounterScript.coinCounterInstance.updateNCCounter(Mathf.CeilToInt(5f * PlayerStatsScript.playerStatsInstance.currentEssenceMultiplyer));
            CoinCounterScript.coinCounterInstance.updateSCCounter(Mathf.CeilToInt(1f * PlayerStatsScript.playerStatsInstance.currentDivinePowerMultiplyer));
        }
    }

}
