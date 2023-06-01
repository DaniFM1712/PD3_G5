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
        if (isCritical)
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
