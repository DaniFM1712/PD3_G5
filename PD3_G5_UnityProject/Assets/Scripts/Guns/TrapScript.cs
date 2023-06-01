using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrapScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float freezeDuration = 5f;
    [SerializeField] float damage = 0f;
    [SerializeField] GameObject slowPrefab;
    float timeToDestroy;
    bool damageDealt = false;

    [Header("FMOD")]
    public StudioEventEmitter TrapActiveEmitter;

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
            TrapActiveEmitter.Play();
            if (other.gameObject.TryGetComponent(out EnemyHealthScript enemyHealth) && !damageDealt)
            {
                Debug.Log("DAMAGE DEALT: "+damage);
                damageDealt = true;
                enemyHealth.TakeDamage(damage, false);
            }
            if (other.gameObject.transform.parent.gameObject.TryGetComponent(out ParentEnemyIAScript enemyIA))
            {
                if(PlayerStatsScript.instance.trapTrapsMultipleEnemiesBlessing)
                    enemyIA.GetStunned(freezeDuration/3);
                enemyIA.GetStunned(freezeDuration);
            }
            if (PlayerStatsScript.instance.trapSlowsBlessing)
            {
                Instantiate(slowPrefab,transform.position,Quaternion.identity);
            }
            if(!PlayerStatsScript.instance.trapTrapsMultipleEnemiesBlessing)
                Destroy(gameObject);
        }

    }

    public void SetTrapDamage(float newDamage)
    {
        damage = newDamage;
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
