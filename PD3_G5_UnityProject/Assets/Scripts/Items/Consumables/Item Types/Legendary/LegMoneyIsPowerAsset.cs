using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Legendary/moneyIsPower")]

public class LegMoneyIsPowerAsset : ConsumableAsset
{
    private float accumulatedDamage = 0f;
    public override void consume()
    {
        PlayerStatsScript.instance.moneyIsPower = true;
        CoinCounterScript.coinCounterInstance.updateNCCounter(0);
    }

    public override void drop()
    {
        PlayerStatsScript.instance.moneyIsPower = false;
        CoinCounterScript.coinCounterInstance.resetMoneyIsPower();    
    }
}
