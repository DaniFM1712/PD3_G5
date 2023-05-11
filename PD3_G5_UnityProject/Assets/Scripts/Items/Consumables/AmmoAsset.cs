using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ammo")]
public class AmmoAsset : ConsumableAsset
{

    [SerializeField] private int ammoToAdd;

    override public bool consume(GameObject gameObject)
    {
        /*
        if (gameObject.TryGetComponent(
                out RaycastShooting shooting)) 
        {
            if (shooting.canAddAmmo()) 
            {
                shooting.addAmmo(ammoToAdd); 
                return true;
            }
        }
        */
        return false;
    }
}
