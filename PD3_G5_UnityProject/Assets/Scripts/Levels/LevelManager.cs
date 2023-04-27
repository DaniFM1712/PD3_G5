using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int PATH_LENGHT;
    static Queue <int> levelPath ;
    static List <int> allLevels = new List<int> {1,2};
    static List<int> levelIndex;
    static int currentLevel = 0;
    public static LevelManager levelManagerInstance { get; private set; }
    //[SerializeField] UnityEvent<float, float> callScene;

    private void Awake()
    {
        if(levelManagerInstance == null){
            levelManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        generateRandomPath();
    }


    public void LoadLevel() {
        if (levelPath.Count > 0)
        {
            currentLevel++;
            SceneManager.LoadScene(levelPath.Dequeue());
        }
        else{
            RestartGame();
        }
    }

    public void generateRandomPath()
    {
        //Generate Random Path
        levelIndex = new List<int>(allLevels);
        Shuffle(levelIndex);
        for (int i = 0; i < allLevels.Count - PATH_LENGHT; i++)
        {
            levelIndex.RemoveAt(0);
        }
        


        //Add Random Store
        int storePos = Random.Range(1, levelIndex.Count-1);
        levelIndex.Insert(storePos, 3);




        levelPath = new Queue<int>(levelIndex);
        levelPath.Enqueue(3);

        //levelPath.Enqueue(0); Nivel final que a�adimos, bossLvl
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
        generateRandomPath();
        CoinCounterScript.coinCounterInstance.resetNCCounter();
        PlayerStatsScript.playerStatsInstance.ResetStats();
        SceneManager.LoadScene(0);
    }

    public int getCurrentIndex() {
        return currentLevel;
    }

    

}