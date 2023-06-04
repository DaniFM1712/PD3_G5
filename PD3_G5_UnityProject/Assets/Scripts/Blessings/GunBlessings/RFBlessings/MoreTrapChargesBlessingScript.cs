using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreTrapChargesBlessingScript : ParentBlessing
{
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        base.blessingType = BlessingType.RapidFire;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        PlayerStatsScript.instance.twoTrapChargesBlessing = true;
        PlayerStatsScript.instance.currentMaxTrapCharges = 2;
        GetComponent<RFSpecialScript>().currentTrapCharges = PlayerStatsScript.instance.currentMaxTrapCharges;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.twoTrapChargesBlessing = false;
        PlayerStatsScript.instance.currentMaxTrapCharges = PlayerStatsScript.instance.baseMaxTrapCharges;
        GetComponent<RFSpecialScript>().currentTrapCharges = PlayerStatsScript.instance.baseMaxTrapCharges;
    }
}
