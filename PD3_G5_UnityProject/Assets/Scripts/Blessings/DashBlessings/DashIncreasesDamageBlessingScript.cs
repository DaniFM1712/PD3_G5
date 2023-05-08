using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIncreasesDamageBlessingScript : ParentBlessing
{
    public float totalDamageTimer = 3f;
    private bool startTimer = false;
    private float damageTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            damageTimer -= Time.deltaTime;

            if (damageTimer < 0)
            {
                PlayerStatsScript.playerStatsInstance.dashDamageBlessing = false;
                damageTimer = totalDamageTimer;
                startTimer = false;
            }
        }

        
    }
    public void StartDamageTimer()
    {
        PlayerStatsScript.playerStatsInstance.dashDamageBlessing = true;
        startTimer = true;
    }
}
