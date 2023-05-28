using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Rares/highHealthDamageBuff")]
public class HighHealthBuffAsset : ConsumableAsset
{

    public override void consume()
    {
        PlayerStatsScript.playerStatsInstance.highHealthDamageBuff = true;
        if (PlayerStatsScript.playerStatsInstance.currentHealth / PlayerStatsScript.playerStatsInstance.GetCurrentMaxHealth() >= 0.9 && !PlayerStatsScript.playerStatsInstance.highHealthDamageApplied)
        {
            PlayerStatsScript.playerStatsInstance.currentDamageMultiplyer += 0.2f;
            PlayerStatsScript.playerStatsInstance.highHealthDamageApplied = true;
        }
    }

    public override void drop()
    {
        PlayerStatsScript.playerStatsInstance.highHealthDamageBuff = false;
        if (PlayerStatsScript.playerStatsInstance.highHealthDamageApplied)
        {
            PlayerStatsScript.playerStatsInstance.currentDamageMultiplyer -= 0.2f;
            PlayerStatsScript.playerStatsInstance.highHealthDamageApplied = false;
        }

    }


}
