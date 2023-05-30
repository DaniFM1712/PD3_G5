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
    [SerializeField] private GameObject damageTextPrefab;


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

    public bool TakeDamage(float damage)
    {
        Debug.Log(currentHealth);
        if (PlayerStatsScript.instance.dashDamageBlessing)
        {
            damage *= 1.3f;
        }
        currentHealth -= damage;
        if(damage!=0)
            Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<DamageTextScript>().Initialise(damage);

        if (healthBar.gameObject.activeSelf == false)
        {
            healthBar.gameObject.SetActive(true);
        }
        healthBar.fillAmount = currentHealth/InitialHealth;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            Debug.Log("RETURN BOOL");
            return true;
        }

        return false;
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
            CoinCounterScript.coinCounterInstance.updateNCCounter(Mathf.CeilToInt(5f * PlayerStatsScript.instance.currentEssenceMultiplyer));
            CoinCounterScript.coinCounterInstance.updateSCCounter(Mathf.CeilToInt(1f * PlayerStatsScript.instance.currentDivinePowerMultiplyer));
        }
    }

}
