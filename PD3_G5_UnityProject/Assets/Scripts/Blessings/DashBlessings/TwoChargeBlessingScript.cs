using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoChargeBlessingScript : ParentBlessing
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
        PlayerStatsScript.instance.dashDoubleChargesBlessing = true;
        PlayerStatsScript.instance.currentMaxDashCharges = 2;
        fPController = GetComponent<FPController>();
        fPController.currentDashCharges = PlayerStatsScript.instance.currentMaxDashCharges;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.currentMaxDashCharges = PlayerStatsScript.instance.baseMaxDashCharges;
        PlayerStatsScript.instance.dashDoubleChargesBlessing = false;
    }

}
