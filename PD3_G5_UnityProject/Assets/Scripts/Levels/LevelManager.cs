using FMODUnity;
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
    public int currentGameMode = 0;
    public Queue <int> levelDiurnalPath;
    public Queue <int> levelNocturnalPath;
    public Queue <int> levelCaoticPath;
    public List <int> manualLevelPath;
    private List <int> diurnalLevels = new List<int> {4,5,6,7,8};
    private List <int> nocturnalLevels = new List<int> {9,9};
    private List <int> caoticLevels = new List<int> {9,9};
    private List<int> levelDiurnalIndex;
    private List<int> levelNocturnalIndex;
    private List<int> levelCaoticIndex;
    private int previousScene = -1;
    public bool randomPath = true;

    [Header("FMOD")]
    public StudioEventEmitter moveEmitter;
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
            levelDiurnalPath = new Queue<int>(manualLevelPath);
    }


    public void LoadLevel() {
        moveEmitter.Stop();
        switch (currentGameMode)
        {
            case 0:
                if (levelDiurnalPath.Count > 0)
                {
                    PlayerStatsScript.instance.SaveBlessings();
                    previousScene = getCurrentSceneIndex();
                    SceneManager.LoadScene(levelDiurnalPath.Dequeue());
                }
                else
                {
                    RestartGame(1);
                }
                break;
            case 1:
                if (levelNocturnalPath.Count > 0)
                {
                    PlayerStatsScript.instance.SaveBlessings();
                    previousScene = getCurrentSceneIndex();
                    SceneManager.LoadScene(levelNocturnalPath.Dequeue());
                }
                else
                {
                    RestartGame(1);
                }
                break;
            case 2:
                if (levelCaoticPath.Count > 0)
                {
                    PlayerStatsScript.instance.SaveBlessings();
                    previousScene = getCurrentSceneIndex();
                    SceneManager.LoadScene(levelCaoticPath.Dequeue());
                }
                else
                {
                    RestartGame(1);
                }
                break;
        }
        if(levelCaoticPath.Count == 0 || levelDiurnalPath.Count == 0 || levelNocturnalPath.Count == 0)
        {
            RestartGame(1);
        }

        CoinCounterScript.coinCounterInstance.updateSCCounter(0);
    }

    public void generateRandomPath()
    {
        levelDiurnalIndex = new List<int>(diurnalLevels);
        levelNocturnalIndex = new List<int>(nocturnalLevels);
        levelCaoticIndex = new List<int>(caoticLevels);

        Shuffle(levelDiurnalIndex);
        Shuffle(levelNocturnalIndex);
        Shuffle(levelCaoticIndex);
        
        for (int i = 0; i < caoticLevels.Count - PATH_LENGHT; i++)
        {
            levelCaoticIndex.RemoveAt(0);
        }
        /////PARA TESTEAR/////
        //levelIndex.Insert(0, 2);
        


        //Add Random Store
        int storePos = Random.Range(1, levelDiurnalIndex.Count-1);
        //levelDiurnalIndex.Insert(storePos, 2);
        //levelNocturnalIndex.Insert(storePos, 2);
        //levelCaoticIndex.Insert(storePos, 2);


        /*
        foreach(int k in levelIndex)
        {
            Debug.Log(k);
        }
        */



        levelDiurnalPath = new Queue<int>(levelDiurnalIndex);
        levelNocturnalPath = new Queue<int>(levelNocturnalIndex);
        levelCaoticPath = new Queue<int>(levelCaoticIndex);
        
        /*
        foreach (int k in levelPath)
        {
            Debug.Log("LP: "+k);
        }
        */
        Debug.Log(levelDiurnalPath.Count);
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
        CoinCounterScript.coinCounterInstance.updateSCCounter(0);
    }


    public int getCurrentSceneIndex() {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public int getPreviousSceneIndex() {
        return previousScene;
    }
}
