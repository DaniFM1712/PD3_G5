using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceDashCooldownBlessingScript : ParentBlessing
{
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
        PlayerStatsScript.playerStatsInstance.dashCooldownBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.playerStatsInstance.dashCooldownBlessing = false;

    }

}
