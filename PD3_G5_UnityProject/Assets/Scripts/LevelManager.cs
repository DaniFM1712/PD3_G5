using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int PATH_LENGHT = 1;
    static Queue <int> levelPath ;
    static List <int> allLevels = new List<int> {1,2,1,2};
    static List<int> levelIndex;
    static int currentLevel = 0;
    chestManagerScript chestManager;
    //[SerializeField] UnityEvent<float, float> callScene;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        chestManager = GameObject.Find("ChestController").GetComponent<chestManagerScript>();
        levelIndex = new List<int>(allLevels);
        Shuffle(levelIndex);
        

        for (int i=0;i< allLevels.Count-PATH_LENGHT;i++){
            levelIndex.RemoveAt(0);   
        }

        levelPath = new Queue<int>(levelIndex);
        levelPath.Dequeue();
        //levelPath.Enqueue(0); Nivel final que a�adimos, bossLvl
    }


    public void LoadLevel(){
        currentLevel++;
        SceneManager.LoadScene(levelPath.Dequeue());
        chestManager.randomizeChests(currentLevel);
    }


    public void Shuffle(List<int> alpha)
    {
        for (int i = 0; i < alpha.Count; i++)
        {
            int temp = alpha[i];
            int randomIndex = Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
        
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }


}
