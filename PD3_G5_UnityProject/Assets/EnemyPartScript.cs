using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPartScript : MonoBehaviour
{
    [SerializeField] float damageMultiplyer = 1f;
    [SerializeField] UnityEvent<float> receiveDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {

        Debug.Log("TAKE DAMAGE BODY PART");
        receiveDamage.Invoke(damage * damageMultiplyer);
    }
}
