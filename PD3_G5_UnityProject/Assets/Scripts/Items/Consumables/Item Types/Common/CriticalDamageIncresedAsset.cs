using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Common/criticalDamageIncresed")]

public class CriticalDamageIncresedAsset : ConsumableAsset
{
    [SerializeField] float criticalDamageBonus = 0.2f;
    public override void consume()
    {
        PlayerStatsScript.instance.currentCriticalMultiplyer += criticalDamageBonus;
    }

    public override void drop()
    {
        PlayerStatsScript.instance.currentCriticalMultiplyer -= criticalDamageBonus;
    }
}
