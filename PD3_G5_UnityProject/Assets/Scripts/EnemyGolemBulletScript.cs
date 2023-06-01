using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemBulletScript : MonoBehaviour
{
    [SerializeField] GameObject specialEffectPrefab;
    [SerializeField] float lifeTime = 5f;
    float damage;
    float timeToDestroy;
    float areaMultiplyer = 1f;
    Vector3 originPosition = new Vector3(0f, 0f, 0f);
    [Header("FMOD")]
    public StudioEventEmitter ExplosionEmitter;
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
            Vector3 vector3 = transform.position;

            if (other.gameObject.TryGetComponent<PlayerHealthScript>(out PlayerHealthScript playerHealth))
            {
                playerHealth.ModifyHealth(-damage);
            }
            ExplosionEmitter.Play();
            GameObject specialEffect = Instantiate(specialEffectPrefab, vector3, Quaternion.identity);
            specialEffect.transform.localScale *= areaMultiplyer;
            ReturnToOrigin();
        }
    }

    public void SetAreaMulitplier(float areaMultiplyer)
    {
        this.areaMultiplyer = areaMultiplyer;
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
