using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
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
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Terrain"))
        {

            if (other.gameObject.TryGetComponent<PlayerHealthScript>(out PlayerHealthScript playerHealth))
            {
                Debug.Log(damage);
                playerHealth.ModifyHealth(-damage);
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
        transform.position = originPosition;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}
