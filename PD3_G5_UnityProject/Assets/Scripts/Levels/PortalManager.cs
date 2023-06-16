using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] GameObject portal1;
    [SerializeField] GameObject portal2;
    [SerializeField] GameObject portal3;
    // Start is called before the first frame update
    void Start()
    {
        portal1.SetActive(false);
        portal2.SetActive(false);
        portal3.SetActive(false);

        switch (LevelManager.instance.currentGameMode)
        {
            case 0:
                portal1.SetActive(true);
                break;
            case 1:
                portal2.SetActive(true);
                break;
            case 2:
                portal3.SetActive(true);
                break;
        }

    }
}
