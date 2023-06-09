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
    [SerializeField] GameObject healthOrbPrefab;
    [SerializeField] int healthOrbPrefabAmount;
    private Canvas canvas;
    private GameObject hitMarker;
    [Header("FMOD")]
    public StudioEventEmitter DeathEmitter;
    private bool hitMarkerActive = false;

    private float hitMarkerTimer = 0f;

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
        if (hitMarkerActive)
        {
            hitMarkerTimer -= Time.deltaTime;
            if (hitMarkerTimer > 0)
            {
                hitMarker.GetComponent<Image>().enabled = true;

            }
            else if (hitMarkerTimer <= 0)
            {
                hitMarkerActive = false;
                hitMarkerTimer = 0f;
                hitMarker.GetComponent<Image>().enabled = false;
            }
        }

    }

    void AddInitialHealth(float healthIncresed)
    {
        InitialHealth += healthIncresed;
    }

    public bool TakeDamage(float damage, bool critical)
    {
        hitMarkerActive = true;
        hitMarkerTimer = 0.30f;
            //StopCoroutine(showHitMarker());

        //StartCoroutine(showHitMarker());
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
            int i = 0;
            while(healthOrbPrefabAmount > i)
            {
                int random = Random.Range(0, 100);
                if(random > 50)
                {
                    Instantiate(healthOrbPrefab, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
                }
                i++;
                //MODO NOCHE - PROB
            }
        }
    }

    IEnumerator showHitMarker()
    {
        hitMarkerActive = true;
        hitMarker.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        hitMarkerActive = false;
        hitMarker.GetComponent<Image>().enabled = false;
    }

    /*
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
    }*/
    IEnumerator deathHitMarker()
    {
        hitMarkerActive = true;
        hitMarkerTimer = 0.30f;
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
        yield return new WaitForSeconds(0.3f);
        Die();
    }



}
