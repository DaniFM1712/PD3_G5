using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/essence")]

public class EssenceAsset : ConsumableAsset
{
    [SerializeField] float essenceMultiplyer = 0.2f;
    override public void consume()
    {
        PlayerStatsScript.instance.currentEssenceMultiplyer += essenceMultiplyer;
    }

    public override void drop()
    {
        PlayerStatsScript.instance.currentEssenceMultiplyer -= essenceMultiplyer;
    }
}
