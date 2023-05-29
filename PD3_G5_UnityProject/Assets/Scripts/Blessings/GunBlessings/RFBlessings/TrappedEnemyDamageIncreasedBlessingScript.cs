using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrappedEnemyDamageIncreasedBlessingScript : ParentBlessing
{
    override public void Start()
    {
        base.Start();
        blessingType = BlessingType.RapidFire;
    }
    private void OnEnable()
    {
        PlayerStatsScript.instance.trappedEnemyDamageIncreasedBlessing = true;
    }
    private void OnDisable()
    {
        PlayerStatsScript.instance.trappedEnemyDamageIncreasedBlessing = false;
    }
}
