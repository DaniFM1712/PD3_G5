using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoGrenadeChargesBlessingScript : ParentBlessing
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
        PlayerStatsScript.playerStatsInstance.currentMaxGrenadeCharges = 2;
        grenadeController = GetComponent<GrenadeScript>();
        grenadeController.currentGrenadeCharges = PlayerStatsScript.playerStatsInstance.currentMaxGrenadeCharges;
    }

    private void OnDisable()
    {
        PlayerStatsScript.playerStatsInstance.currentMaxGrenadeCharges = PlayerStatsScript.playerStatsInstance.baseMaxGrenadeCharges;

    }

}
