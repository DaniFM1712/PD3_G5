using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDealsDamageBlessingScript : ParentBlessing
{
    public float increasedTrapDamage = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerStatsScript.instance.trapDealsDamageBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.trapDealsDamageBlessing = false;
    }


}
