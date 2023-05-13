using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesSpawnScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesToSpawn;
    public List<GameObject> enemiesToKill;
    [SerializeField] private float secondsForNextWave;
    private int listIndex = 0;
    private bool canGoNextWave = true;
    private bool startWaves = false;
    private bool enemies = true;


    private void Update()
    {
        if (startWaves)
        {
            if (listIndex<enemiesToSpawn.Count)
            {

                if (canGoNextWave)
                {
                    foreach (Transform enemy in enemiesToSpawn[listIndex].transform)
                    {
                        enemy.gameObject.SetActive(true);
                        enemiesToKill.Add(enemy.gameObject);
                    }
                    StartCoroutine(TimerToNextWave());
                    listIndex++;
                    canGoNextWave = false;
                }
                else
                {
                    if (isEmptyList(enemiesToKill))
                    {
                        canGoNextWave = true;
                        StopCoroutine(TimerToNextWave());
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private bool isEmptyList(List<GameObject> list)
    {
        enemies = true;
        foreach(GameObject enemy in list)
        {
            if (enemy != null) {
                enemies = false;
            }
        }
        return enemies;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startWaves = true;
        }
    }



    IEnumerator TimerToNextWave()
    {
        yield return new WaitForSeconds(secondsForNextWave);
        canGoNextWave=true;
    }
}
