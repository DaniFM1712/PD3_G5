using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    public bool opened= false;
    private bool canTake = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canTake)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                opened = true;
                Canvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;

            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        canTake = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canTake = false;
    }

    private void OnDestroy()
    {
        Canvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
}
