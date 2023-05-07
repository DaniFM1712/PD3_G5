using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public int currentSelectedWeapon { get; set; }
    public int currentNormalCoin { get; set; }
    public int currentSpecialCoin { get; set; }

    //--------- UPGRADES LIST ---------//
    //public List<bool> currentDashAbilities { get; set; } = new List<bool>(5);
    public List<bool> currentDashAbilities { get; set; }
    public List<bool> currentWeaponAbilities { get; set; }
    public List<bool> currentGrenadeAbilities { get; set; }


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
        baseMaxHealth = 150f;
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
        currentSelectedWeapon = 0;
        currentNormalCoin = 0;  
        currentSpecialCoin = 0;

        //--------- UPGRADES LIST ---------//
        currentDashAbilities = Enumerable.Repeat(false, 5).ToList();
        currentWeaponAbilities = Enumerable.Repeat(false, 5).ToList();
        currentGrenadeAbilities = Enumerable.Repeat(false, 5).ToList();

    }

    private void Start()
    {

    }


    public void ResetStats()
    {
        currentMaxHealth = baseMaxHealth;
        currentMaxShield = baseMaxShield;
        currentHealth = currentMaxHealth;
        currentShield = currentMaxShield;
        currentDamageBonus = baseDamageBonus;
        currentSpeedBonus = baseSpeedBonus;
        currentSelectedWeapon = 0;
        currentNormalCoin = 0;
        currentDashAbilities = Enumerable.Repeat(false, 5).ToList();
        currentWeaponAbilities = Enumerable.Repeat(false, 5).ToList();
        currentGrenadeAbilities = Enumerable.Repeat(false, 5).ToList();
    }
}
