using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    public static PlayerStatsScript instance { get; private set; }

    //----------BASE--STATS----------//

    public float baseMaxHealth = 150f;
    public float baseMaxShield = 0;
    public int baseDamageBonus = 0;
    public int baseSpeedBonus = 0;
    public int baseMaxDashCharges = 1;
    public int baseMaxGrenadeCharges = 1;
    public int baseMaxTrapCharges = 1;
    public int maxBlessings = 8;
    public bool secondLifeUnlocked = false;
    public bool secondLife = false;
    public List<bool> permanentUpgrades = new List<bool>();

    //public IEnumerable<bool> permanentUpgrades= Enumerable.Repeat(false, 6);


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
    public int currentWeaponIndex;
    public ProjectileShootingScript currentWeapon;
    public int currentNormalCoin;
    public int currentSpecialCoin;
    public int activatedBlessings = 0;
    public bool isReloading = false;

    //----------SHOTGUN----------//

    //--------- BLESSING---------//
    public List<bool> currentBlessings;

    //DASH
    public bool dashDamageBlessing = false;
    public bool dashCooldownBlessing = false;
    public bool dashReloadWeaponBlessing = false;
    public bool dashHealBlessing = false;
    public bool dashDoubleChargesBlessing = false;

    //GRENADE
    public bool fireDOTBlessing = false;
    public bool killEnemyGrenadeBlessing = false;



    //SHOTGUN
    public bool killEnemyAbilityCooldownBlessing = false;
    public bool killEnemyDamageBuffBlessing = false;
    public bool distanceDamageBlessing = false;



    //RAPIDFIRE
    public bool trappedEnemyDamageIncreasedBlessing = false;
    public bool trapDealsDamageBlessing = false;
    public bool trapTrapsMultipleEnemiesBlessing = false;
    public bool trapSlowsBlessing = false;


    //---------ITEMS---------//
    public List<bool> currentDashAbilities;
    public List<bool> currentWeaponAbilities;
    public List<bool> currentGrenadeAbilities;


    //COMMON
    public float baseFireRateMultiplyer = 1f;
    public float currentFireRateMultiplyer;

    public float baseEssenceMultiplyer = 1f;
    public float currentEssenceMultiplyer;

    public float baseDivinePowerMultiplyer = 1f;
    public float currentDivinePowerMultiplyer;

    public float baseCriticalMultiplyer = 2f;
    public float currentCriticalMultiplyer;

    public float baseMaxHealthMultiplyer = 1f;
    public float currentMaxHealthMultiplyer;
    
    
    public float baseDamageMultiplyer = 1f;
    public float currentDamageMultiplyer;
    
    public float baseSpeedMultiplyer = 1f;
    public float currentSpeedMultiplyer;    
    




    //RARE

    public bool highHealthDamageBuff = false;
    public bool highHealthDamageApplied = false;
    public bool threeShotBuff = false;
    public bool reloadDamageBuff = false;
    public bool speedBuffAfterKilling = false;
    public bool speedBuffActivated = false;

    //LEGENDARY

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        if (secondLifeUnlocked)
        {
            secondLife = true;
        }

        for (int i = 0; i<6;i++)
        {
            permanentUpgrades.Add(false);
        }

        //----------MAX--STATS----------//
        currentMaxHealth = baseMaxHealth;
        currentMaxShield = baseMaxShield;
        currentMaxDashCharges = baseMaxDashCharges;
        currentMaxGrenadeCharges = baseMaxGrenadeCharges;
        currentMaxTrapCharges = baseMaxTrapCharges;


    //----------CURRENT--STATS----------//
    currentHealth = currentMaxHealth;
        currentShield = currentMaxShield;
        //currentDamageBonus = baseDamageBonus;
        currentSpeedBonus = baseSpeedBonus;
        currentWeaponIndex = 0;
        currentNormalCoin = 0;
        currentSpecialCoin = 0;
        currentWeapon = null;

        //---------BLESSINGS---------//

        currentBlessings = new List<bool>();
        dashDamageBlessing = false;
        fireDOTBlessing = false;
        dashCooldownBlessing = false;
        dashReloadWeaponBlessing = false;
        dashHealBlessing = false;
        dashDoubleChargesBlessing = false;
        killEnemyGrenadeBlessing = false;
        killEnemyAbilityCooldownBlessing = false;
        distanceDamageBlessing = false;
        trappedEnemyDamageIncreasedBlessing = false;
        trapDealsDamageBlessing = false;
        trapTrapsMultipleEnemiesBlessing = false;
        trapSlowsBlessing = false;

        //---------ITEMS---------//

        currentFireRateMultiplyer = baseFireRateMultiplyer;
        currentEssenceMultiplyer = baseEssenceMultiplyer;
        currentDivinePowerMultiplyer = baseDivinePowerMultiplyer;
        currentCriticalMultiplyer = baseCriticalMultiplyer;
        currentDamageMultiplyer = baseDamageMultiplyer;
        currentMaxHealthMultiplyer = baseMaxHealthMultiplyer;
        currentSpeedMultiplyer = baseSpeedMultiplyer;
        highHealthDamageBuff = false;
        highHealthDamageApplied = false;
        threeShotBuff = false;
        reloadDamageBuff = false;
        speedBuffAfterKilling = false;
        speedBuffActivated = false;

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
        currentWeaponIndex = 0;
        currentWeapon = null;
        currentNormalCoin = 0;
        currentMaxDashCharges = baseMaxDashCharges;
        currentMaxGrenadeCharges = baseMaxGrenadeCharges;
        currentMaxTrapCharges = baseMaxTrapCharges;
        currentBlessings.Clear();
        isReloading = false;
        highHealthDamageBuff = false; 
        highHealthDamageApplied = false;
        threeShotBuff = false;
        reloadDamageBuff = false;
        speedBuffAfterKilling = false;
        speedBuffActivated = false;
        activatedBlessings = 0;
        dashDamageBlessing = false;
        fireDOTBlessing = false;
        dashCooldownBlessing = false;
        dashReloadWeaponBlessing = false;
        dashHealBlessing = false;
        dashDoubleChargesBlessing = false;
        killEnemyGrenadeBlessing = false;
        killEnemyAbilityCooldownBlessing = false;
        distanceDamageBlessing = false;
        trappedEnemyDamageIncreasedBlessing = false;
        trapDealsDamageBlessing = false;
        trapTrapsMultipleEnemiesBlessing = false;
        trapSlowsBlessing = false;
        currentFireRateMultiplyer = baseFireRateMultiplyer;
        currentEssenceMultiplyer = baseEssenceMultiplyer;
        currentDivinePowerMultiplyer = baseDivinePowerMultiplyer;
        currentCriticalMultiplyer = baseCriticalMultiplyer;
        currentDamageMultiplyer = baseDamageMultiplyer;
        currentMaxHealthMultiplyer = baseMaxHealthMultiplyer;
        currentSpeedMultiplyer = baseSpeedMultiplyer;
        if (secondLifeUnlocked)
        {
            secondLife = true;
        }

        /*
        currentDashAbilities = Enumerable.Repeat(false, 5).ToList();
        currentWeaponAbilities = Enumerable.Repeat(false, 5).ToList();
        currentGrenadeAbilities = Enumerable.Repeat(false, 5).ToList();
        */
    }

    public void ActivateBlessings()
    {
        int index = 0;
        ParentBlessing[] blessings = FindObjectsByType<ParentBlessing>(FindObjectsSortMode.InstanceID);
        string bools = "";
        string blessingFound = "";

        foreach(bool boolean in currentBlessings)
        {

            bools += boolean + " ";
        }

        foreach(ParentBlessing blessing in blessings)
        {
            blessingFound +=blessing+ " ";
        }

        Debug.Log("Active Blessings: " + bools);
        Debug.Log("Blessings: "+blessingFound);

        foreach (bool activeBlessing in currentBlessings)
        {
            if (activeBlessing)
            {
                blessings[index].enabled = true;
                Debug.Log("Index: " + index + "  Blessing: " + blessings[index] + blessings[index].enabled);
            }
            index++;
        }
        currentBlessings.Clear();
    }

    public void SaveBlessings()
    {
        ParentBlessing[] blessings = FindObjectsByType<ParentBlessing>(FindObjectsSortMode.InstanceID);
        foreach (ParentBlessing blessing in blessings)
        {
            if (blessing.enabled)
            {
                currentBlessings.Add(true);
            }
            else
            {
                currentBlessings.Add(false);
            }
        }

        string savedBless="";

        foreach (bool textBlessing in currentBlessings)
        {
            savedBless += textBlessing + " ";
        }

        Debug.Log("Saved Blessings: " + savedBless);

    }

    public float GetCurrentMaxHealth()
    {
        return currentMaxHealth * currentMaxHealthMultiplyer;
    }
}
