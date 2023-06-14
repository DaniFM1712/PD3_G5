using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float trappedDamageIncreased = 0.3f;
    [SerializeField] float speedBufMultiplyer= 0.15f;
    float damage;
    float timeToDestroy;
    Vector3 originPosition = new Vector3(0f, 0f, 0f);

    [SerializeField] GameObject hitEffectPrefab;
    GameObject hitEffect;

    private void Awake()
    {
        timeToDestroy = lifeTime;
    }

    private void Start()
    {
        if(hitEffectPrefab != null)
            hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

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
            if (hitEffect != null)
            {
                hitEffect.transform.position = transform.position;
                hitEffect.transform.rotation = transform.rotation;
                hitEffect.GetComponent<ParticleController>().playParticlesTrap();
            }
            ReturnToOrigin();
        }

        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Terrain"))
        {
            if (hitEffect != null)
            {
                hitEffect.transform.position = transform.position;
                hitEffect.transform.rotation = transform.rotation;
                hitEffect.GetComponent<ParticleController>().playParticlesTrap();
            }
            bool dead = false;

            if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
            {
                if (PlayerStatsScript.instance.trappedEnemyDamageIncreasedBlessing && other.gameObject.transform.parent.gameObject.GetComponent<ParentEnemyIAScript>().isTrapped)
                {
                    damage *= (1f+trappedDamageIncreased);
                }
                dead = enemyPart.TakeDamage(damage, null);
            }

            if (PlayerStatsScript.instance.spawnGrenadeOnShoot)
            {
                if(Random.Range(0,100) >= 89)
                {
                    GameObject grenade =  Instantiate(grenadePrefab, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 2f, other.gameObject.transform.position.z), Quaternion.identity);
                    grenade.GetComponent<GrenadeBulletScript>().doubleDamage = true;
                }
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
                if (PlayerStatsScript.instance.speedBuffAfterKilling)
                {
                    PlayerStatsScript.instance.speedBuffActivated = true;
                    PlayerStatsScript.instance.currentFireRateMultiplyer += speedBufMultiplyer;
                    PlayerStatsScript.instance.currentSpeedMultiplyer += speedBufMultiplyer;
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
