using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    public static PlayerStatsScript playerStatsInstance { get; private set; }

    //----------BASE--STATS----------//

    public float baseMaxHealth = 150f;
    public float baseMaxShield = 0;
    public int baseDamageBonus = 0;
    public int baseSpeedBonus = 0;
    public int baseMaxDashCharges = 1;
    public int baseMaxGrenadeCharges = 1;
    public int baseMaxTrapCharges = 1;


    //----------MAX--STATS----------//

    public float currentMaxHealth;
    public float currentMaxShield;
    public int currentMaxDashCharges;
    public int currentMaxGrenadeCharges;
    public int currentMaxTrapCharges;


    //----------CURRENT--STATS----------//

    public float currentHealth;
    public float currentShield;
    public float currentDamageBonus;
    public float currentSpeedBonus;
    public int currentSelectedWeapon;
    public int currentNormalCoin;
    public int currentSpecialCoin;

    //--------- UPGRADES LIST ---------//
    //public List<bool> currentDashAbilities; = new List<bool>(5);
    public List<bool> currentDashAbilities;
    public List<bool> currentWeaponAbilities;
    public List<bool> currentGrenadeAbilities;
    public bool dashDamageBlessing = false;


    private void Awake()
    {
        if (playerStatsInstance == null)
        {
            playerStatsInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);


        //----------MAX--STATS----------//
        currentMaxHealth = baseMaxHealth;
        currentMaxShield = baseMaxShield;
        currentMaxDashCharges = baseMaxDashCharges;
        currentMaxGrenadeCharges = baseMaxGrenadeCharges;
        currentMaxTrapCharges = baseMaxTrapCharges;

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
        currentMaxDashCharges = baseMaxDashCharges;
        currentMaxGrenadeCharges = baseMaxGrenadeCharges;
        currentMaxTrapCharges = baseMaxTrapCharges;

        /*
        currentDashAbilities = Enumerable.Repeat(false, 5).ToList();
        currentWeaponAbilities = Enumerable.Repeat(false, 5).ToList();
        currentGrenadeAbilities = Enumerable.Repeat(false, 5).ToList();
        */
    }
}
