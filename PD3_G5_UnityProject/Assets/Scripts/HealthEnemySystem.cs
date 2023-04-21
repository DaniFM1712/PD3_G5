using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemySystem : MonoBehaviour
{
    [SerializeField] float InitialHealth;
    float currentHealth;
    // Start is called before the first frame update

    private void Awake()
    {
        currentHealth = InitialHealth;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void addInitialHealth(float healthIncresed)
    {
        InitialHealth += healthIncresed;
    }

    void takeDamage(float damage)
    {
        currentHealth -= damage;
    }


    public float getCurrentHealth()
    {
        return currentHealth;
    }

}
