using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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
    private GameObject hitMarker;
    [Header("FMOD")]
    public StudioEventEmitter DeathEmitter;
    private bool hitMarkerActive = false; 

    private void Awake()
    {
        currentHealth = InitialHealth;
    }
    private void Start()
    {
        canvas = GameObject.Find("EnemyHealthCanvas").GetComponent<Canvas>();
        hitMarker = GameObject.Find("CanvasPrefab/HitMarker");
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
        StartCoroutine(showHitMarker());
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
            StartCoroutine(deathHitMarker());
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


    IEnumerator showHitMarker()
    {
        hitMarkerActive = true;
        hitMarker.GetComponent<Image>().enabled = true;
        if (hitMarkerActive)
        {
            hitMarkerActive = false;
            yield return new WaitForSeconds(0.25f);
            if (!hitMarkerActive && hitMarker.GetComponent<Image>().enabled)
            {
                hitMarker.GetComponent<Image>().enabled = false;
            }
        }
    }
    IEnumerator deathHitMarker()
    {
        Debug.Log(gameObject);
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
        hitMarker.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        hitMarker.GetComponent<Image>().enabled = false;
        Die();
    }



}
