using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/maxHealth")]
public class MaxHealthAsset : ConsumableAsset {

    [SerializeField] float maxHealthMultiplyer = 0.2f;
    override public void consume()
    {
        PlayerStatsScript.playerStatsInstance.currentMaxHealthMultiplyer += maxHealthMultiplyer;
    }

    public override void drop()
    {
        PlayerStatsScript.playerStatsInstance.currentMaxHealthMultiplyer -= maxHealthMultiplyer;
    }
}

