using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSlowsBlessing : ParentBlessing
{
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        blessingType = BlessingType.RapidFire;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerStatsScript.instance.trapSlowsBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.trapSlowsBlessing = false;
    }
}
