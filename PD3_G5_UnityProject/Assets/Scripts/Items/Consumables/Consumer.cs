using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {//guardem el resultat en la variable en consumable
        if (other.gameObject.TryGetComponent<Consumable>(out Consumable consumable))
        {
            consumable.Take();
        }
    }
}
