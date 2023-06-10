using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthOrbScript : MonoBehaviour
{
    [SerializeField] float healthOrbValue = 15f;
    // Start is called before the first frame update
    Vector3 force;
    private void Start()
    {
        float x = Random.Range(-2f,2f);
        float y = Random.Range(2f, 4f);
        float z = Random.Range(-2f, 2f);
        force = new Vector3(x,y,z);
        transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            transform.parent.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        }
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthScript>().ModifyHealth(healthOrbValue);
            Destroy(transform.parent.gameObject);
        }
    }
}
