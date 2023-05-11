using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/maxHealth")]
public class MaxHealthAsset : ConsumableAsset {

    [SerializeField] float maxHealthMultiplyer = 1.2f;
    override public bool consume()
    {
        PlayerStatsScript.playerStatsInstance.currentMaxHealth *= maxHealthMultiplyer;
        return true;
    }
}

