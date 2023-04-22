using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextLevelScript : MonoBehaviour
{
    [SerializeField] UnityEvent callScene;


    private void OnTriggerEnter(Collider other)
    {
        callScene.Invoke();
    }
}
