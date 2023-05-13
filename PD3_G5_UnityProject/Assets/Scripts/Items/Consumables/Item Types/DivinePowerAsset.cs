using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/divinePower")]
public class DivinePowerAsset : ConsumableAsset
{
    [SerializeField] float divinePowerMultiplyer = 0.2f;
    override public void consume()
    {
        PlayerStatsScript.playerStatsInstance.currentDivinePowerMultiplyer += divinePowerMultiplyer;
    }

    public override void drop()
    {
        PlayerStatsScript.playerStatsInstance.currentDivinePowerMultiplyer -= divinePowerMultiplyer;
    }
}
