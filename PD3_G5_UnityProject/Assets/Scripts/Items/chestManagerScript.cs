using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestManagerScript : MonoBehaviour
{
    [SerializeField] List<GameObject> chestList;
    [SerializeField] int chestNumber;
    private int chestSpawned = 0;


    private void Start()
    {
        randomizeChests();
    }


    public void randomizeChests()
    {
        //int currentLevel = LevelManager.levelManagerInstance.getCurrentIndex();
        while (chestSpawned < chestNumber)
        {
            int rIndex = Random.Range(0, chestList.Count);
            if (!chestList[rIndex].activeSelf)
            {
                bool appearsPercent = Random.Range(0, 100) > 50;

                if (appearsPercent)
                {
                    //60 30 10
                    chestList[rIndex].SetActive(true);
                    chestSpawned++;
                    //k.GetComponent<chestScript>().generateRandomReward();
                }
                else
                {
                    chestList[rIndex].SetActive(false);
                }
            }
        }

    }
}
