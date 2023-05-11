using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/health")]
public class HealthAsset : ConsumableAsset {
    [SerializeField] private float healthToAdd;
    override public bool consume(GameObject gameObject)//rebrem el player
    {/*
        if (gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            if (healthSystem.canAddHealth())
            {
                healthSystem.addHealth(healthToAdd);
                return true;                              
            }
        } */
        return false;
    }
}

