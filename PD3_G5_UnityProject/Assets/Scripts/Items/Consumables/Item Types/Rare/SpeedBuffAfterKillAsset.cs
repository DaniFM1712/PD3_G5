using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ParentBlessing;

[CreateAssetMenu(menuName = "Items/Rares/speedBuffAfterKilling")]

public class SpeedBuffAfterKillAsset : ConsumableAsset
{
    public override void consume()
    {
        PlayerStatsScript.instance.speedBuffAfterKilling = true;
    }

    public override void drop()
    {
        PlayerStatsScript.instance.speedBuffAfterKilling = false;
    }
}

