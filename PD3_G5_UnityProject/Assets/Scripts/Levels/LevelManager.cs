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
    static Queue <int> levelPath;
    static List <int> allLevels = new List<int> {3,4};
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
        PlayerStatsScript.playerStatsInstance.SaveBlessings();
        generateRandomPath();
    }


    public void LoadLevel() {
        if (levelPath.Count > 0)
        {
            currentLevel++;
            PlayerStatsScript.playerStatsInstance.SaveBlessings();
            SceneManager.LoadScene(levelPath.Dequeue());
        }
        else
        {
            RestartGame();
        }

        Debug.Log("LOADING LEVEL: "+currentLevel);
    }

    public void generateRandomPath()
    {
        //Generate Random Path
        levelIndex = new List<int>(allLevels);

        ShuffleInt(levelIndex);
        for (int i = 0; i < allLevels.Count - PATH_LENGHT; i++)
        {
            levelIndex.RemoveAt(0);
        }
        /////PARA TESTEAR/////
        levelIndex.Insert(0, 2);
        


        //Add Random Store
        int storePos = Random.Range(1, levelIndex.Count-1);
        levelIndex.Insert(storePos, 2);


        foreach(int k in levelIndex)
        {
            Debug.Log(k);
        }


        levelPath = new Queue<int>(levelIndex);
        levelPath.Enqueue(2);
        foreach (int k in levelPath)
        {
            Debug.Log("LP: "+k);
        }
        Debug.Log(levelPath.Count);
        //levelPath.Enqueue(0); Nivel final que añadimos, bossLvl
    }

    public void ShuffleInt(List<int> alpha)
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
