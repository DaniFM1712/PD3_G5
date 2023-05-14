using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float damage = 50f;
    float timeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        timeToDestroy = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 0f)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.name == "AoEPrefab(Clone)")
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
                {
                    enemyPart.TakeDamage(damage, null);
                }
            }
        }
        else if (gameObject.name == "GolemExplosionPrefab(Clone)")
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.TryGetComponent<PlayerHealthScript>(out PlayerHealthScript player))
                {
                    player.ModifyHealth(-damage);
                }
            }
        }

        
   
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
        {
            enemyPart.TakeDamage(damage, null);
        }
    }
    */

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
        {
            enemyPart.TakeDamage(damage, null);
        }
    }
    */
}
