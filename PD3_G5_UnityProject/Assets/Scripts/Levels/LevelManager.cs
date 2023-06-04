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
    public Queue <int> levelPath;
    public List <int> manualLevelPath;
    private List <int> allLevels = new List<int> {4,5,6};
    private List<int> levelIndex;
    private int previousScene = -1;
    public bool randomPath = true;
    public static LevelManager instance { get; private set; }
    //[SerializeField] UnityEvent<float, float> callScene;

    private void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerStatsScript.instance.SaveBlessings();
        previousScene = getCurrentSceneIndex();
        if(randomPath)
            generateRandomPath();
        else
            levelPath = new Queue<int>(manualLevelPath);
    }


    public void LoadLevel() {
        if (levelPath.Count > 0)
        {
            PlayerStatsScript.instance.SaveBlessings();
            previousScene = getCurrentSceneIndex();
            SceneManager.LoadScene(levelPath.Dequeue());
        }
        else
        {
            RestartGame(1);
        }
    }

    public void generateRandomPath()
    {
        levelIndex = new List<int>(allLevels);

        Shuffle(levelIndex);
        for (int i = 0; i < allLevels.Count - PATH_LENGHT; i++)
        {
            levelIndex.RemoveAt(0);
        }
        /////PARA TESTEAR/////
        //levelIndex.Insert(0, 2);
        


        //Add Random Store
        int storePos = Random.Range(1, levelIndex.Count-1);
        levelIndex.Insert(storePos, 2);


        foreach(int k in levelIndex)
        {
            Debug.Log(k);
        }



        levelPath = new Queue<int>(levelIndex);
        foreach (int k in levelPath)
        {
            Debug.Log("LP: "+k);
        }
        Debug.Log(levelPath.Count);
        //levelPath.Enqueue(0); Nivel final que añadimos, bossLvl
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

    public void GoToDeathMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);
    }

    public void RestartLevel() {
        previousScene = getCurrentSceneIndex();
        PlayerStatsScript.instance.currentHealth = PlayerStatsScript.instance.GetCurrentMaxHealth();
        HealthUIScript.instance.updateHealth(false);
        SceneManager.LoadScene(getCurrentSceneIndex());
    }
    public void RestartGame(int scene)
    {
        generateRandomPath();
        CoinCounterScript.coinCounterInstance.resetNCCounter();
        InventoryManagerScript.InventoryInstance.ResetInventory();
        PlayerStatsScript.instance.ResetStats();
        SceneManager.LoadScene(scene);
        previousScene = getCurrentSceneIndex();
    }


    public int getCurrentSceneIndex() {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public int getPreviousSceneIndex() {
        return previousScene;
    }
}
