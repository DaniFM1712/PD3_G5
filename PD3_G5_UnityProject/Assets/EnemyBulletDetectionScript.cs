using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDetectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            bool dir = false;
            if (Vector3.Dot(other.transform.position, Vector3.forward) == 0)
            {
                Vector3 cross = Vector3.Cross(Vector3.forward, other.transform.position);
                if (cross.y > 0)
                { // right
                    dir = true;
                }
                else
                { // left
                    dir = false;
                }
            }

            transform.parent.gameObject.GetComponent<MeleChaserEnemy>().BulletDetected(dir);
        }
    }
}
