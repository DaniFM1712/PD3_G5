using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/fireRate")]
public class FireRateAsset : ConsumableAsset
{
    [SerializeField] float fireRateMultiplyer = 0.2f;
    override public void consume()
    {
        if(PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer - fireRateMultiplyer < 0.5)
        {
            PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer = 0.5f;
        }
        else
        {
            PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer -= fireRateMultiplyer;
        }
    }

    public override void drop()
    {
        if (PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer + fireRateMultiplyer > 2f)
        {
            PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer = 2f;
        }
        else
        {
            PlayerStatsScript.playerStatsInstance.currentFireRateMultiplyer += fireRateMultiplyer;
        }
    }
}
