using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGSpecialBulletScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    public SGSpecialScript weaponScript;
    public KillEnemyDamageBuffBlessingScript damageBuffBlessing;
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

        if (timeToDestroy <= 0f)
        {
            timeToDestroy = lifeTime;
            ReturnToOrigin();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnergyEnemyShield"))
        {
            ReturnToOrigin();
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Terrain"))
        {
            bool dead = false;
            float damageMult = 1f;

            if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
            {
                if (PlayerStatsScript.instance.distanceDamageBlessing)
                {
                    damageMult = (timeToDestroy / 2f);
                }
                dead = enemyPart.TakeDamage(damage* damageMult, gameObject);

            }
            if (dead)
            {
                if (PlayerStatsScript.instance.killEnemyAbilityCooldownBlessing)
                {
                    weaponScript.ResetSpecialCooldown();
                }
                if (PlayerStatsScript.instance.killEnemyDamageBuffBlessing)
                {
                    damageBuffBlessing.StartDamageTimer();
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
