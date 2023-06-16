using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
