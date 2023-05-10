using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrapScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float freezeDuration = 5f;
    [SerializeField] float damage = 50f;
    float timeToDestroy;
    bool damageDealt = false;

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


        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart) && !damageDealt)
            {
                damageDealt = true;
                enemyPart.TakeDamage(damage, null);
            }
            if (other.gameObject.TryGetComponent<MeleChaserEnemy>(out MeleChaserEnemy enemyIA))
            {
                enemyIA.GetStunned(freezeDuration);
            }
            Destroy(gameObject);
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
