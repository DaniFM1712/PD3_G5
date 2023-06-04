using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShotBlessingScript : ParentBlessing
{

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        base.blessingType = BlessingType.ShotGun;
    }
    private void OnEnable()
    {
        PlayerStatsScript.instance.doubleShotBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.doubleShotBlessing = false;
    }

}
