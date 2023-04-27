using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    [SerializeField] GameObject enemiesToSpawn;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach(Transform enemy in enemiesToSpawn.transform)
            {  
                enemy.gameObject.SetActive(true);
            }
        }

        Destroy(gameObject);
    }
}
