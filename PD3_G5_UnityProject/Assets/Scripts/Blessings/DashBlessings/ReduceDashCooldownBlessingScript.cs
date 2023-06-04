using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceDashCooldownBlessingScript : ParentBlessing
{
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        blessingType = BlessingType.Dash;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerStatsScript.instance.dashCooldownBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.dashCooldownBlessing = false;

    }

}
