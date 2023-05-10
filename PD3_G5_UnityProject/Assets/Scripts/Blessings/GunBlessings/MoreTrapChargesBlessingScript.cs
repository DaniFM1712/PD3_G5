using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreTrapChargesBlessingScript : ParentBlessing
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        PlayerStatsScript.playerStatsInstance.currentMaxTrapCharges = 2;
        rFSpecialScript = GetComponent<RFSpecialScript>();
        rFSpecialScript.currentTrapCharges = PlayerStatsScript.playerStatsInstance.currentMaxTrapCharges;
    }

    private void OnDisable()
    {
        PlayerStatsScript.playerStatsInstance.currentMaxTrapCharges = PlayerStatsScript.playerStatsInstance.baseMaxTrapCharges;

    }
}
