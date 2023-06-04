using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPartScript : MonoBehaviour
{
    [SerializeField] bool isCritical = false;
    private float currentHealth = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage(float damage, GameObject bullet)
    {
        float totalDamage = damage;
        bool chance = Random.Range(0, 100) > 79;
        if (isCritical || (PlayerStatsScript.instance.criticalBuff && chance))
        {
            totalDamage = damage * PlayerStatsScript.instance.currentCriticalMultiplyer;
        }
        if (transform.parent.gameObject.TryGetComponent<EnemyHealthScript>(out EnemyHealthScript health))
        {
            currentHealth = health.GetCurrentHealth();
            currentHealth -= (totalDamage);
            bool dead = health.TakeDamage(totalDamage, isCritical);
            return dead;
        }
        return false;
    }
}
