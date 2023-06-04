using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float baseDamage = 50f;
    [SerializeField] GameObject fireDOTPrefab;
    float timeToDestroy;
    private bool firstTarget = false;
    public bool doubleDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        timeToDestroy = lifeTime;
        if (PlayerStatsScript.instance.fireDOTBlessing)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y+1, transform.position.z), Vector3.down, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.CompareTag("Terrain"))
                {
                    Instantiate(fireDOTPrefab, transform.position, Quaternion.identity);
                }
            }
        }
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
                float damage = baseDamage * PlayerStatsScript.instance.currentGrenadeDamageMultiplyer;
                firstTarget = true;
                if (other.gameObject.TryGetComponent<EnemyHealthScript>(out EnemyHealthScript enemyHealth))
                {
                    if (firstTarget && PlayerStatsScript.instance.multipleTargetsGrenadeBlessing)
                    {
                        damage *= 1.3f;
                    }
                    if (doubleDamage && PlayerStatsScript.instance.spawnGrenadeOnShoot) { 
                        damage *= 2f;
                    }
                    enemyHealth.TakeDamage(damage, false);
                }
            }
        }
        else if (gameObject.name == "GolemExplosionPrefab(Clone)")
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.TryGetComponent<PlayerHealthScript>(out PlayerHealthScript player))
                {
                    player.ModifyHealth(-baseDamage);
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
