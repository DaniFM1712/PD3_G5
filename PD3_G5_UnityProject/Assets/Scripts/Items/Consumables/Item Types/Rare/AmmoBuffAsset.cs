using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Rares/ammoBuff")]

public class AmmoBuffAsset : ConsumableAsset
{
    [SerializeField] float ammoIncrease = 0.2f;
    public override void consume()
    {
        PlayerStatsScript.instance.ammoBuff = true;
        PlayerStatsScript.instance.currentAmmoMultiplyer += ammoIncrease;
    }

    public override void drop()
    {
        PlayerStatsScript.instance.ammoBuff = false;
        PlayerStatsScript.instance.currentAmmoMultiplyer -= ammoIncrease;
    }
}