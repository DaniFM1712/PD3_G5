using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShotBlessingScript : ParentBlessing
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool DoubleShot()
    {
        return UnityEngine.Random.Range(1, 100) > 50;
    }

}
