using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDotScript : MonoBehaviour
{

    [SerializeField] float lifeTime = 8f;
    [SerializeField] float damage = 5f;
    [SerializeField] float damageTime = 2f;

    private List<EnemyHealthScript> enemies;
    float timeToDestroy;
    float timeToDamage;
    // Start is called before the first frame update
    void Start()
    {
        timeToDestroy = lifeTime;
        timeToDamage = damageTime;
        enemies = new List<EnemyHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;
        
        if (timeToDestroy <= 0f)
        {
            Destroy(gameObject);
        }

        timeToDamage -= Time.deltaTime;
        if (timeToDamage <= 0f)
        {
            timeToDamage = damageTime;
            DealDamage();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out EnemyHealthScript enemyHealth))
        {
            enemies.Add(enemyHealth);
        }
    }    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out EnemyHealthScript enemyHealth))
        {
            enemies.Remove(enemyHealth);
        }
    }

    private void DealDamage()
    {
        foreach(EnemyHealthScript enemyHealth in enemies)
        {
            if(enemyHealth != null)
                enemyHealth.TakeDamage(damage);
        }
    }



}
