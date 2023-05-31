using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Rares/threeShotBuffAsset")]


public class ThreeShotBuffAsset : ConsumableAsset
{
    public override void consume()
    {
        PlayerStatsScript.instance.threeShotBuff = true;
       
    }

    public override void drop()
    {
        PlayerStatsScript.instance.threeShotBuff = false;
    }
}
