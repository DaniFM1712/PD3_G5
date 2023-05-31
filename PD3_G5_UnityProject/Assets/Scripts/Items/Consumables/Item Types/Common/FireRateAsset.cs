using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/fireRate")]
public class FireRateAsset : ConsumableAsset
{
    [SerializeField] float fireRateMultiplyer = 0.2f;
    override public void consume()
    {
        if(PlayerStatsScript.instance.currentFireRateMultiplyer - fireRateMultiplyer < 0.5)
        {
            PlayerStatsScript.instance.currentFireRateMultiplyer = 0.5f;
        }
        else
        {
            PlayerStatsScript.instance.currentFireRateMultiplyer -= fireRateMultiplyer;
        }
    }

    public override void drop()
    {
        if (PlayerStatsScript.instance.currentFireRateMultiplyer + fireRateMultiplyer > 2f)
        {
            PlayerStatsScript.instance.currentFireRateMultiplyer = 2f;
        }
        else
        {
            PlayerStatsScript.instance.currentFireRateMultiplyer += fireRateMultiplyer;
        }
    }
}
