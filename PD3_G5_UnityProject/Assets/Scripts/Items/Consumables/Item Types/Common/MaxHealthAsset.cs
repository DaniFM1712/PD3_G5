using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Common/maxHealth")]
public class MaxHealthAsset : ConsumableAsset {

    [SerializeField] float maxHealthMultiplyer = 0.2f;
    override public void consume()
    {
        PlayerStatsScript.instance.currentMaxHealthMultiplyer += maxHealthMultiplyer;
    }

    public override void drop()
    {
        PlayerStatsScript.instance.currentMaxHealthMultiplyer -= maxHealthMultiplyer;
    }
}

