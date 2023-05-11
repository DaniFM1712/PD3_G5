using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/divinePower")]
public class DivinePowerAsset : ConsumableAsset
{
    [SerializeField] float divinePowerMultiplyer = 0.8f;
    override public bool consume()
    {
        PlayerStatsScript.playerStatsInstance.currentDivinePowerMultiplyer = divinePowerMultiplyer;
        return true;
    }
}
