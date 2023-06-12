using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotator : MonoBehaviour
{
    private bool Activated = false;
    [SerializeField] private float RotationSpeed = 2f;
    [SerializeField] private Vector3 RotationMovement;
    void OnEnable()
    {
        Activated = true;
    }

    void Update()
    {
        if (Activated)
        {
            transform.Rotate(RotationMovement * Time.deltaTime*RotationSpeed);
        }
    }

    void OnDisable()
    {
        Activated = false;
    }
}
