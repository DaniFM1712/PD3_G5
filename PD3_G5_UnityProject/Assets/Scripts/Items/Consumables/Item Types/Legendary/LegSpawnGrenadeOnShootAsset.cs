using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Legendary/spawnGrenadeOnShoot")]

public class LegSpawnGrenadeOnShootAsset : ConsumableAsset
{
    [SerializeField] float fireRateMulitplyerIncrease = 0.2f;
    public override void consume()
    {
        PlayerStatsScript.instance.spawnGrenadeOnShoot = true;
        PlayerStatsScript.instance.currentFireRateMultiplyer += fireRateMulitplyerIncrease;
    }

    public override void drop()
    {
        PlayerStatsScript.instance.spawnGrenadeOnShoot = false;
        PlayerStatsScript.instance.currentFireRateMultiplyer -= fireRateMulitplyerIncrease;
    }
}
