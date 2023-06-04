using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Legendary/criticalBuff")]


public class LegCriticalAsset : ConsumableAsset
{
    [SerializeField] float criticalMultiplyerIncrease = 0.25f;
    // Start is called before the first frame update
    public override void consume()
    {
        PlayerStatsScript.instance.criticalBuff = true;
        PlayerStatsScript.instance.currentCriticalMultiplyer += criticalMultiplyerIncrease;
    }

    public override void drop()
    {
        PlayerStatsScript.instance.criticalBuff = false;
        PlayerStatsScript.instance.currentCriticalMultiplyer -= criticalMultiplyerIncrease;
    }
}
