using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Rares/vitalityBuff")]
public class VitalityBuffAsset : ConsumableAsset
{
    [SerializeField] float healingMuliplyerIncrease = 0.2f;
    public override void consume()
    {
        PlayerStatsScript.instance.vitalityBuff = true;
        PlayerStatsScript.instance.currentMaxHealthMultiplyer += healingMuliplyerIncrease;
        PlayerStatsScript.instance.currentHealingMultiplyer += healingMuliplyerIncrease;
        HealthUIScript.instance.updateHealth(false);
    }

    public override void drop()
    {
        PlayerStatsScript.instance.vitalityBuff = false;
        PlayerStatsScript.instance.currentMaxHealthMultiplyer -= healingMuliplyerIncrease;
        PlayerStatsScript.instance.currentHealingMultiplyer -= healingMuliplyerIncrease;
        HealthUIScript.instance.updateHealth(false);
    }
}
