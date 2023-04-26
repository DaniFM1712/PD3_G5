using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class chestManagerScript : MonoBehaviour
{
    [SerializeField] List<GameObject> chestList;

    private void Start()
    {
        randomizeChests();
    }


    public void randomizeChests()
    {
        //int currentLevel = LevelManager.levelManagerInstance.getCurrentIndex();
        Debug.Log("SDAS");
        foreach (GameObject k in chestList)
        {
            bool appearsPercent = Random.Range(0, 100) > 50;
            Debug.Log(appearsPercent);

            if (appearsPercent)
            {
                k.SetActive(true);
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
            }  else
                k.SetActive(false);
        }
    }
}
