using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetsDamageBuffBlessingScript : ParentBlessing
{
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        base.blessingType = BlessingType.Grenade;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerStatsScript.instance.multipleTargetsGrenadeBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.multipleTargetsGrenadeBlessing = false;
    }
}
