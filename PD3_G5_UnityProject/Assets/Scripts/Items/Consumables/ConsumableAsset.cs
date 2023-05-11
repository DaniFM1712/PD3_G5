using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class ConsumableAsset: ScriptableObject
{
    abstract public bool consume(GameObject consumer);

}
