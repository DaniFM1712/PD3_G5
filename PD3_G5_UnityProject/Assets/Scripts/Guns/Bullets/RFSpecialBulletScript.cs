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
        if (other.gameObject.CompareTag("Terrain"))
        {

            if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
            {
                enemyPart.TakeDamage(damage, null);
            }

            GameObject specialEffect = Instantiate(specialEffectPrefab, other.ClosestPoint(transform.position), Quaternion.identity);
            ReturnToOrigin();
        }
    }


    private void ReturnToOrigin()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        transform.SetPositionAndRotation(originPosition, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
