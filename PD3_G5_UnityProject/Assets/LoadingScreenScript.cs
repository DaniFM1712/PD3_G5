using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenScript : MonoBehaviour
{
    private GameObject blessings;
    // Start is called before the first frame update
    void Start()
    {
        if (LevelManager.instance.getCurrentSceneIndex() < 4)
        {
            Destroy(gameObject);
        }
        else
        {
            blessings = GameObject.Find("BlessingTrigger");
            blessings.SetActive(false);
            StartCoroutine(WaitLevelCharge());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitLevelCharge()
    {
        yield return new WaitForSecondsRealtime(5f);
        blessings.SetActive(true); 
        Destroy(gameObject);
    }
}
