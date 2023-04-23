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
    //[SerializeField] UnityEvent<float, float> callScene;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        levelIndex = new List<int>(allLevels);
        Shuffle(levelIndex);
        

        for (int i=0;i< allLevels.Count-PATH_LENGHT;i++){
            levelIndex.RemoveAt(0);   
        }

        levelPath = new Queue<int>(levelIndex);
        levelPath.Dequeue();
        //levelPath.Enqueue(0); Nivel final que añadimos, bossLvl
    }


    public void LoadLevel(){
        SceneManager.LoadScene(levelPath.Dequeue());
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
        foreach (int k in alpha) { Debug.Log(k.ToString()); }
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
