using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class HealthUIScript : MonoBehaviour
{
    [SerializeField] Image healthAmount;
    [SerializeField] GameObject hpCounter; 
    private TextMeshProUGUI hpText;


    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        hpText = hpCounter.GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth(float hpPoints)
    {
        hpText.text = (int) hpPoints/maxHealth+" HP";
        healthAmount.fillAmount = hpPoints / maxHealth;
    }

    public void updateMaxHealth(float maxHpPoints)
    {
        maxHealth = maxHpPoints;
    }
}
