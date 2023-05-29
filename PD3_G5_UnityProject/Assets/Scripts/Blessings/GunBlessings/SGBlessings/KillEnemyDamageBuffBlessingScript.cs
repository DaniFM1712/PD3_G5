using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyDamageBuffBlessingScript : ParentBlessing
{
    [SerializeField] float damageMultiplyerIncrease = 0.3f;
    public float totalDamageTimer = 3f;
    private bool startTimer = false;
    private float damageTimer;

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        blessingType = BlessingType.ShotGun;
    }

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
        startTimer = true;
        damageTimer = totalDamageTimer;
        PlayerStatsScript.instance.currentDamageMultiplyer += damageMultiplyerIncrease;
    }

    private void OnEnable()
    {
        PlayerStatsScript.instance.killEnemyDamageBuffBlessing = true;
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.killEnemyDamageBuffBlessing = false;

    }
}
