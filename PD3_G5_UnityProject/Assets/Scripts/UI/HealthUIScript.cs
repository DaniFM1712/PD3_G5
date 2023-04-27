using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    [SerializeField] Image healthAmount;
    [SerializeField] GameObject hpCounter;
    private TextMeshProUGUI hpText;
    PlayerStatsScript playerStats;


    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStatsScript.playerStatsInstance;
        hpText = hpCounter.GetComponent<TextMeshProUGUI>();
        updateHealth(playerStats.currentHealth); 
        updateMaxHealth(playerStats.currentMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth(float hpPoints)
    {
        float hp = (hpPoints / maxHealth) * 100;
        string hpString = (int)hp + " %";
        hpText.text = hpString;
        healthAmount.fillAmount = hpPoints / maxHealth;

    }

    public void updateMaxHealth(float maxHpPoints)
    {
        maxHealth = maxHpPoints;
    }

    

}
