using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAmmountBlessingScript : ParentBlessing
{
    [SerializeField] int bulletsPerTapBlessing = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GetComponent<SGSpecialScript>().SetSBulletsPerTap(bulletsPerTapBlessing);
    }

    private void OnDisable()
    {
        GetComponent<SGSpecialScript>().SetSBulletsPerTap(0);
    }
}
