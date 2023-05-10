using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrapScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float freezeDuration = 5f;
    [SerializeField] float baseTrapDamage = 10f;
    float timeToDestroy;
    bool damageDealt = false;
    float damage;

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
                Debug.Log("DAMAGE DEALT: "+damage);
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

    public void SetTrapDamage(float increasedDamage)
    {
        Debug.Log("INCREASED DAMAGE: "+increasedDamage);
        damage = increasedDamage;
        Debug.Log("1. DAMAGE: " + damage);
        if (increasedDamage == 0)
            damage = baseTrapDamage;

        Debug.Log("2. DAMAGE: " + damage);

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
