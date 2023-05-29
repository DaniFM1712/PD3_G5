using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Rares/highHealthDamageBuff")]
public class HighHealthBuffAsset : ConsumableAsset
{

    public override void consume()
    {
        PlayerStatsScript.instance.highHealthDamageBuff = true;
        if (PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth() >= 0.9 && !PlayerStatsScript.instance.highHealthDamageApplied)
        {
            PlayerStatsScript.instance.currentDamageMultiplyer += 0.2f;
            PlayerStatsScript.instance.highHealthDamageApplied = true;
        }
    }

    public override void drop()
    {
        PlayerStatsScript.instance.highHealthDamageBuff = false;
        if (PlayerStatsScript.instance.highHealthDamageApplied)
        {
            PlayerStatsScript.instance.currentDamageMultiplyer -= 0.2f;
            PlayerStatsScript.instance.highHealthDamageApplied = false;
        }

    }


}
