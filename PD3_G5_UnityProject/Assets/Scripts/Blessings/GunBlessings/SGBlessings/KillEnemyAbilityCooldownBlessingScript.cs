using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyAbilityCooldownBlessingScript : ParentBlessing
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
        PlayerStatsScript.instance.killEnemyAbilityCooldownBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.killEnemyAbilityCooldownBlessing = false;
    }
}
