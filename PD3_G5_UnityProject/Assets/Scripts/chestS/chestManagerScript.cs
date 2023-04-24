using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class chestManagerScript : MonoBehaviour
{
    GameObject[] chestList;

    public void randomizeChests(int currentLevel)
    {
        chestList = GameObject.FindGameObjectsWithTag("Chest");

        Debug.Log(chestList[0]);
        foreach (GameObject k in chestList)
        {
            bool appearsPercent = Random.Range(0, 100) > 20;
            Debug.Log(appearsPercent);

            if (appearsPercent)
            {
                gameObject.SetActive(true);
                int rewardPercent = Random.Range(0, 100);
                // Check with design team
                /*
                 * if (rewardPercent >= 0 && rewardPercent < 40 - (currentLevel * 5))
                {

                }
                else if (rewardPercent >= 41 - (currentLevel * 5) && rewardPercent < 65 - (currentLevel * 5))
                {

                }
                else if (rewardPercent >= 66 - (currentLevel * 5) && rewardPercent < 85 - (currentLevel * 5))
                {

                }
                else
                {

                }*/
            }
        }
    }
}
