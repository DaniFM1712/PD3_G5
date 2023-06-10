using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoGrenadeChargesBlessingScript : ParentBlessing
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
        PlayerStatsScript.instance.twoGrenadesBlessing = true;
        PlayerStatsScript.instance.currentMaxGrenadeCharges++;
        PlayerStatsScript.instance.currentGrenadeCharges = PlayerStatsScript.instance.currentMaxGrenadeCharges;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.twoGrenadesBlessing = false;
        PlayerStatsScript.instance.currentMaxGrenadeCharges = PlayerStatsScript.instance.baseMaxGrenadeCharges;
    }

}
