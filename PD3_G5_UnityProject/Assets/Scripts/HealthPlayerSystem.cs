using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class HealthPlayerSystem : MonoBehaviour
{
    [SerializeField] float initialHealth;
    [SerializeField] float maxHealth;
    [SerializeField] UnityEvent<float> updateHealth;
    [SerializeField] UnityEvent<float> updateMaxHealth;
    float currentHealth;

    private void Awake()
    {
        currentHealth = initialHealth;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            modifyHealth(-20.0f);
        }
    }


    public void modifyHealth(float modifier)
    {
        currentHealth += modifier;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0.0f)
        {
            die();
        }
    }


    public void die()
    {
        //Descomentar després    Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public float getCurrentHealth()
    {
        return currentHealth;
    }

        
}