using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFSpecialBulletScript : MonoBehaviour
{
    [SerializeField] GameObject specialEffectPrefab;
    [SerializeField] float lifeTime = 15f;
    float damage;
    float timeToDestroy;
    Vector3 originPosition = new Vector3(0f, 0f, 0f);
    private float trapDamage = 0f;
    [SerializeField]float areaMultiplyerBlessing = 2f;

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
            //Destroy(gameObject);
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (other.gameObject.CompareTag("Terrain"))
        {
            GameObject specialEffect = Instantiate(specialEffectPrefab, other.ClosestPoint(transform.position), Quaternion.identity);
            if(PlayerStatsScript.instance.trapTrapsMultipleEnemiesBlessing)
                specialEffect.transform.localScale *= areaMultiplyerBlessing;
            specialEffect.GetComponent<TrapScript>().SetTrapDamage(trapDamage);
            ReturnToOrigin();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.transform.parent.gameObject.TryGetComponent(out ParentEnemyIAScript enemyIA))
            {
                enemyIA.GetStunned(5f);
                other.gameObject.transform.parent.gameObject.GetComponent<EnemyHealthScript>().TakeDamage(damage, false);
            }
            ReturnToOrigin();
        }
        

    }

    public void SetTrapDamage(float trapDamage)
    {
        this.trapDamage = trapDamage;
    }

    public void SetBulletDamage(float damage)
    {
        this.damage = damage;
    }

    private void ReturnToOrigin()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        transform.SetPositionAndRotation(originPosition, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
