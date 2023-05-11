using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/fireRate")]
public class FireRateAsset : ConsumableAsset
{
    [SerializeField] float fireRateMultiplyer = 0.8f;
    override public bool consume()
    {
        PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer = fireRateMultiplyer;
        return true;
    }
}
