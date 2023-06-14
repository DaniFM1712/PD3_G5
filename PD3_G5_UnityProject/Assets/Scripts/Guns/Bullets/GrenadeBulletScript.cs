using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBulletScript : MonoBehaviour
{
    [SerializeField] GameObject specialEffectPrefab;
    [SerializeField] float lifeTime = 5f;
    public bool doubleDamage;
    float timeToDestroy;
    float areaMultiplyer = 1f;
    Vector3 originPosition = new Vector3(0f, 0f, 0f);
    public bool big;

    [Header("FMOD")]
    public StudioEventEmitter ExplosionEmitter;

    private void Awake()
    {
        timeToDestroy = lifeTime;
        big = false;
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
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Terrain"))
        {
            ExplosionEmitter.Play();
            
            GameObject specialEffect = Instantiate(specialEffectPrefab,transform.position, Quaternion.identity);         
            specialEffect.GetComponent<ExplosionScript>().doubleDamage = doubleDamage;
            if (big)
                specialEffect.GetComponent<Animator>().SetBool("big",true);
            else
                specialEffect.GetComponent<Animator>().SetBool("big", false);

            specialEffect.transform.localScale *= areaMultiplyer;
            ReturnToOrigin();
        }   
    }

    public void SetAreaMulitplier(float areaMultiplyer)
    {
        this.areaMultiplyer = areaMultiplyer;
    }

    private void ReturnToOrigin()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        transform.position = originPosition;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}
