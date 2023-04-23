using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] float initialHealth;
    [SerializeField] float maxHealth;
    [SerializeField] UnityEvent<float> updateHealth;
    [SerializeField] UnityEvent<float> updateMaxHealth;
    float currentHealth;

    private void Awake()
    {
        currentHealth = initialHealth;
        updateMaxHealth.Invoke(maxHealth);
        updateHealth.Invoke(currentHealth);
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHealth.Invoke(currentHealth);
        if (currentHealth == 0.0f)
        {
            die();
        }


    }


    public void die()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);
    }


    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public void modifyMaxHealth(float hpMaxPoints)
    {
        maxHealth += hpMaxPoints;
        updateMaxHealth.Invoke(maxHealth);
    }
}