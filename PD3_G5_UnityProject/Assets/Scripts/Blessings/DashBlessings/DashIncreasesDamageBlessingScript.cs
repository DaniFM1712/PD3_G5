using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIncreasesDamageBlessingScript : ParentBlessing
{
    [SerializeField] float damageMultiplyerIncrease = 0.3f;
    public float totalDamageTimer = 3f;
    private bool startTimer = false;
    private float damageTimer;


    override public void Start()
    {
        base.Start();
        blessingType = BlessingType.Dash;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            damageTimer -= Time.deltaTime;

            if (damageTimer < 0)
            {
                PlayerStatsScript.instance.currentDamageMultiplyer -= damageMultiplyerIncrease;
                damageTimer = totalDamageTimer;
                startTimer = false;
            }
        }

        
    }
    public void StartDamageTimer()
    {
        PlayerStatsScript.instance.currentDamageMultiplyer += damageMultiplyerIncrease;
        startTimer = true;
        damageTimer = totalDamageTimer;
    }

    private void OnEnable()
    {
        PlayerStatsScript.instance.dashDamageBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.dashDamageBlessing = false;

    }

}
