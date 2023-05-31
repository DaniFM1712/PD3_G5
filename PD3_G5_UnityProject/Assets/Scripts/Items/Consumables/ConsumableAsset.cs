using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class ConsumableAsset: ScriptableObject
{
    public string itemName, itemDescription;
    public enum Rarity { Common, Rare, Legendary };
    public Rarity rarity;
    public bool repeatable = false;
    public bool spawned = false;

    abstract public void consume();
    abstract public void drop();

}
