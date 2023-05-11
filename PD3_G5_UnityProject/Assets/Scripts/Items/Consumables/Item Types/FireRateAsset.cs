using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/fireRate")]
public class FireRateAsset : ConsumableAsset
{
    [SerializeField] float fireRateMultiplyer = 0.8f;
    override public bool consume()
    {
        if(PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer * fireRateMultiplyer < 0.1)
        {
            PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer = 0.1f;
        }
        else
        {
            PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer = fireRateMultiplyer;
        }

        return true;
    }
}
