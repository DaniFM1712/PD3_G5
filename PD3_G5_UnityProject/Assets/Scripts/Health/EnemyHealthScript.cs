using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] float InitialHealth;
    float currentHealth;

    private void Awake()
    {
        currentHealth = InitialHealth;
    }


    // Update is called once per frame
    void Update()
    {

    }

    void AddInitialHealth(float healthIncresed)
    {
        InitialHealth += healthIncresed;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        //ANIMATION
        //SOUND
        Destroy(gameObject);
    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }

}
