using FMODUnity;
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
    [SerializeField] Transform damageTextPosition;
    [SerializeField] float ncReward = 3;
    [SerializeField] float scReward = 1;
    private Canvas canvas;
    [Header("FMOD")]
    public StudioEventEmitter DeathEmitter;

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

    public bool TakeDamage(float damage, bool critical)
    {
        Debug.Log(currentHealth);
        if (PlayerStatsScript.instance.dashDamageBlessing)
        {
            damage *= 1.3f;
        }
        currentHealth -= damage;
        if(damage!=0)
            Instantiate(damageTextPrefab, damageTextPosition.position, Quaternion.identity).GetComponent<DamageTextScript>().Initialise(damage,critical);

        if (healthBar.gameObject.activeSelf == false)
        {
            healthBar.gameObject.SetActive(true);
        }
        healthBar.fillAmount = currentHealth/InitialHealth;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            return true;
        }

        return false;
    }

    private void Die()
    {
        //ANIMATION
        //SOUND
        DeathEmitter.Play();
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
            CoinCounterScript.coinCounterInstance.updateNCCounter(Mathf.CeilToInt(ncReward * PlayerStatsScript.instance.currentEssenceMultiplyer));
            CoinCounterScript.coinCounterInstance.updateSCCounter(Mathf.CeilToInt(scReward * PlayerStatsScript.instance.currentDivinePowerMultiplyer));
        }
    }

}
