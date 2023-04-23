using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image healthAmount;
    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth(float hpPoints)
    {
        healthAmount.fillAmount = hpPoints / maxHealth;
    }

    public void updateMaxHealth(float maxHpPoints)
    {
        maxHealth = maxHpPoints;
    }
}
