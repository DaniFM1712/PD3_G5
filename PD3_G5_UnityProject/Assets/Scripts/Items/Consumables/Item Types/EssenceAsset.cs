using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/essence")]

public class EssenceAsset : ConsumableAsset
{
    [SerializeField] float essenceMultiplyer = 0.8f;
    override public bool consume()
    {
        PlayerStatsScript.playerStatsInstance.currentEssenceMultiplyer = essenceMultiplyer;
        return true;
    }
}
