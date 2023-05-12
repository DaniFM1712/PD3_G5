using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class ConsumableAsset: ScriptableObject
{
    public string itemName, itemDescription;
    public enum Rarity { Common, Rare, Legendary };
    public Rarity rarity;
    abstract public bool consume();

}
