using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float trappedDamageIncreased = 0.3f;
    float damage;
    float timeToDestroy;
    Vector3 originPosition = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        timeToDestroy = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if(timeToDestroy <= 0f)
        {
            timeToDestroy = lifeTime;
            ReturnToOrigin();
            //Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnergyEnemyShield"))
        {
            ReturnToOrigin();
        }

        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Terrain"))
        {
            bool dead = false;

            if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
            {
                if (PlayerStatsScript.instance.trappedEnemyDamageIncreasedBlessing)
                {
                    damage *= (1f+trappedDamageIncreased);
                }
                dead = enemyPart.TakeDamage(damage, null);
            }
            if (dead)
            {
                if (PlayerStatsScript.instance.dashCooldownBlessing)
                {
                    player.GetComponent<FPController>().reduceDashCooldown(2f);

                }
                if (PlayerStatsScript.instance.killEnemyGrenadeBlessing)
                {
                    int i = Random.Range(0,100);
                    if(i>=90)
                        Instantiate(grenadePrefab, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y+2f, other.gameObject.transform.position.z), Quaternion.identity);
                }
            }


            ReturnToOrigin();
        }
    }

    public void SetDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }

    private void ReturnToOrigin()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        transform.SetPositionAndRotation(originPosition, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
