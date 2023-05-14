using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarScript : MonoBehaviour
{
    private Camera camera;
    public Transform target;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Player/PitchController/Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera.transform,Vector3.up);
        transform.position = target.position + offset;
    }




}
