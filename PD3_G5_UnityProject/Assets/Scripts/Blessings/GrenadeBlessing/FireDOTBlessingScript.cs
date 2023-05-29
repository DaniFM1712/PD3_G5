using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ParentBlessing;

public class FireDOTBlessingScript : ParentBlessing
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        base.blessingType = BlessingType.Grenade;
    }

    private void OnEnable()
    {
        PlayerStatsScript.instance.fireDOTBlessing = true;
    }


    private void OnDisable()
    {
        PlayerStatsScript.instance.fireDOTBlessing = false;
    }

}
