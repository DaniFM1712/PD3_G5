using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUi : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    private void Start()
    {
        playerCamera = GameObject.Find("Player/PitchController/Main Camera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward, playerCamera.transform.rotation * Vector3.up);
    }

}
