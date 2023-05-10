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
    bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        timeToDestroy = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            timeToDestroy -= Time.deltaTime;
            
            if (timeToDestroy <= 0f)
            {
                Debug.Log("DEATH");
                Destroy(gameObject);
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Enemy"))
        {
            /*if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
            {
                enemyPart.TakeDamage(damage, null);
            }*/
            activated = true;
            if (other.gameObject.TryGetComponent<NavMeshAgent>(out _))
            {
                Debug.Log("1. NAV AGENT");

                StartCoroutine(FreezeEffect(other.gameObject));
            }

        }

    }

    IEnumerator FreezeEffect(GameObject agent)
    {
        if(agent.TryGetComponent<MeleChaserEnemy>(out MeleChaserEnemy meleeStop))
        {
            Debug.Log("2. MELEE SCRIPT");
            meleeStop.StopAgent();
        }
        
        yield return new WaitForSeconds(freezeDuration);

        if (agent.TryGetComponent<MeleChaserEnemy>(out MeleChaserEnemy meleeRestart))
        {
            meleeRestart.RestartAgent();
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
