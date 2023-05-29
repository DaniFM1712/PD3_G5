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
        PlayerStatsScript.instance.currentMaxGrenadeCharges = 2;
        grenadeController = GetComponent<GrenadeScript>();
        grenadeController.currentGrenadeCharges = PlayerStatsScript.instance.currentMaxGrenadeCharges;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.currentMaxGrenadeCharges = PlayerStatsScript.instance.baseMaxGrenadeCharges;

    }

}
