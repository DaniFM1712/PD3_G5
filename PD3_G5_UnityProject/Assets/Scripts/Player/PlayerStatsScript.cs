using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    public static PlayerStatsScript playerStatsInstance { get; private set; }

    //----------BASE--STATS----------//

    public float baseMaxHealth { get; set; }
    public float baseMaxShield { get; set; }
    public int baseDamageBonus { get; set; }
    public int baseSpeedBonus { get; set; }


    //----------MAX--STATS----------//

    public float currentMaxHealth { get; set; }
    public float currentMaxShield { get; set; }


    //----------CURRENT--STATS----------//

    public float currentHealth { get; set; }
    public float currentShield { get; set; }
    public float currentDamageBonus { get; set; }
    public float currentSpeedBonus { get; set; }
    
    


    private void Awake()
    {
        if (playerStatsInstance == null)
        {
            playerStatsInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        //----------BASE--STATS----------//
        baseMaxHealth = 100f;
        baseMaxShield = 0f;
        baseDamageBonus = 0;
        baseSpeedBonus = 0;

        //----------MAX--STATS----------//
        currentMaxHealth = baseMaxHealth;
        currentMaxShield = baseMaxShield;



        //----------CURRENT--STATS----------//
        currentHealth = currentMaxHealth;
        currentShield = currentMaxShield;
        currentDamageBonus = baseDamageBonus;
        currentSpeedBonus = baseSpeedBonus;
    }

    private void Start()
    {

    }
}
