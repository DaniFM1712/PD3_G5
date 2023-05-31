using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Rares/reloadDamageBuff")]

public class ReloadDamageBuffAsset : ConsumableAsset
{
    public override void consume()
    {
        PlayerStatsScript.instance.reloadDamageBuff = true;
    }

    public override void drop()
    {
        PlayerStatsScript.instance.reloadDamageBuff = false;
    }
}
