using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
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
            Debug.Log("PODEMOS COGER");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(transform.parent.gameObject);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Canvas.SetActive(true);

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

    public void destroyCanvas()
    {
        Destroy(Canvas);
    }
}
