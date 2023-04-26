using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float damage = 50f;
    float timeToDestroy;

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
            Debug.Log("1.1 - EXPLOSION TRIGGER");
            if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
            {
                Debug.Log("1.2 - EXPLOSION DAMAGE");
                enemyPart.TakeDamage(damage);
            }
        }
   
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("2.1 - EXPLOSION TRIGGER");
        if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
        {
            Debug.Log("2.2 - EXPLOSION DAMAGE");
            enemyPart.TakeDamage(damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("3.1 - EXPLOSION TRIGGER");
        if (other.gameObject.TryGetComponent<EnemyPartScript>(out EnemyPartScript enemyPart))
        {
            Debug.Log("3.2 - EXPLOSION DAMAGE");
            enemyPart.TakeDamage(damage);
        }
    }
}
