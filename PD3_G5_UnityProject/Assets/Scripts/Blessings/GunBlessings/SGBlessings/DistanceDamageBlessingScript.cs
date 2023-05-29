using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDamageBlessingScript : ParentBlessing
{
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        base.blessingType = BlessingType.ShotGun;
    }

    private void OnEnable()
    {
        PlayerStatsScript.instance.distanceDamageBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.distanceDamageBlessing = false;

    }
}
