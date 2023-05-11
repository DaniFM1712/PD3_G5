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
        foreach (GameObject k in chestList)
        {
            bool appearsPercent = Random.Range(0, 100) > 50;

            if (appearsPercent)
            {
                //60 30 10
                k.SetActive(true);
                k.GetComponent<chestScript>().generateRandomReward();
            }
            else{
                k.SetActive(false);
            }
        }
    }
}
