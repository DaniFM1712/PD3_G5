using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.UnloadSceneAsync(LevelManager.instance.getCurrentSceneIndex());
        StartCoroutine(LoadSceneAsync(LevelManager.instance.LoadLevel()));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("LOADING SCENE");
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            yield return null;
        }
        Debug.Log("DONE LOADING SCENE");
    }

}
