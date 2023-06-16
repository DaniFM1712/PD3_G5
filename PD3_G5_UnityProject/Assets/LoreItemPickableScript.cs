using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
using UnityEngine.Timeline;

public class LoreItemPickableScript : MonoBehaviour
{
    [SerializeField] int loreNum;
    [SerializeField] GameObject loreMesh;
    [SerializeField] GameObject canvas;
    private GameObject canvasLore;
    [SerializeField] int percentRan;
    private bool canTake = false;
    [Header("FMOD")]
    public StudioEventEmitter TakeEmitter;


    // Start is called before the first frame update
    void Start()
    {
        bool dLevel = loreNum < 2;
        bool nLevel = loreNum > 1 && loreNum < 4;
        bool cLevel = loreNum > 3;
        if ((LevelManager.instance.currentGameMode == 0 && dLevel) || (LevelManager.instance.currentGameMode == 1 && nLevel) || 
            (LevelManager.instance.currentGameMode == 2 && cLevel))
        {
            int randomizer = Random.Range(0, 100);
            Debug.Log(randomizer >= percentRan && !PlayerStatsScript.instance.loreEnabled && PlayerStatsScript.instance.loreUnlocked[loreNum] == false);
            if (randomizer >= percentRan && !PlayerStatsScript.instance.loreEnabled && PlayerStatsScript.instance.loreUnlocked[loreNum] == false)
            {
                loreMesh.SetActive(true);
                PlayerStatsScript.instance.loreEnabled = true;
                //canvasLore = GameObject.Find("CanvasPrefab/loreAppears");
                //canvasLore.SetActive(true);
                //StartCoroutine(DestroyText());
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && canTake)
        {
            PlayerStatsScript.instance.loreUnlocked[loreNum] = true;
            TakeEmitter.Play();
            Destroy(loreMesh);
            Destroy(canvas);
            Destroy(gameObject);
        }
    }

    IEnumerator destroyText()
    {
        yield return new WaitForSecondsRealtime(5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTake = true;
            canvas.GetComponent<TextMeshProUGUI>().enabled = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTake = false;
            canvas.GetComponent<TextMeshProUGUI>().enabled = false;

        }
    }
    private void OnDestroy()
    {
        PlayerStatsScript.instance.loreEnabled = false;
    }


}
