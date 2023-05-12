using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentBlessing : MonoBehaviour
{
    protected FPController fPController;
    protected GrenadeScript grenadeController;
    public string blessingName;
    public string blessingDescription;
    public enum BlessingType {Dash,Grenade,RapidFire,ShotGun};
    public BlessingType blessingType; 

    // Start is called before the first frame update
    virtual public void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
