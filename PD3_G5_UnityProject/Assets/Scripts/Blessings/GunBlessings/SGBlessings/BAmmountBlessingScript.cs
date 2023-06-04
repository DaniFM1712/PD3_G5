using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAmmountBlessingScript : ParentBlessing
{
    [SerializeField] int bulletsPerTapBlessing = 10;
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        blessingType = BlessingType.ShotGun;
    }



    private void OnEnable()
    {
        PlayerStatsScript.instance.bulletsShotAmountBlessing = true;
        GetComponent<SGSpecialScript>().SetSBulletsPerTap(bulletsPerTapBlessing);
    }

    private void OnDisable()
    {
        PlayerStatsScript.instance.bulletsShotAmountBlessing = false;
        GetComponent<SGSpecialScript>().SetSBulletsPerTap(0);
    }
}
