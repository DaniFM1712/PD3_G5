using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAOEBlessingScript : ParentBlessing
{
    public float areaMultiplyer = 2f;
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
        PlayerStatsScript.instance.doubleAOEGrenadeBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.doubleAOEGrenadeBlessing = false;
    }

}
