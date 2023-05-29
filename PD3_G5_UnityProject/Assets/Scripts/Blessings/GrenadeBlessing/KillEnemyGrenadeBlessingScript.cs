using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyGrenadeBlessingScript : ParentBlessing
{
    // Start is called before the first frame update
    void Start()
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
        PlayerStatsScript.playerStatsInstance.killEnemyGrenadeBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.playerStatsInstance.killEnemyGrenadeBlessing = false;
    }
}
