using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoreItemPickableScript : MonoBehaviour
{
    [SerializeField] int loreNum;
    [SerializeField] GameObject loreMesh;
    [SerializeField] GameObject canvas;
    [SerializeField] int percentRan;
    private bool canTake = false;
    // Start is called before the first frame update
    void Start()
    {
        int randomizer = Random.Range(0, 100);
        if(randomizer >= percentRan && !PlayerStatsScript.instance.loreEnabled)
        {
            loreMesh.SetActive(true);
            PlayerStatsScript.instance.loreEnabled = true;
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
            Destroy(loreMesh);
            Destroy(canvas);
            Destroy(gameObject);
        }
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
