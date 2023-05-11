using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class ConsumableAsset: ScriptableObject
{
    public string itemName, itemDescription;
    abstract public bool consume();

}
