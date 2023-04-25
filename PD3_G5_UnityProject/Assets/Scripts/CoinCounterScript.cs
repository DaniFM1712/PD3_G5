using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounterScript : MonoBehaviour
{
    [SerializeField] GameObject ncCounter;
    [SerializeField] GameObject scCounter;
    public static CoinCounterScript coinCounterInstance { get; private set; }
    static public int ncAmount = 0;
    static public int scAmount = 0;
    private TextMeshProUGUI ncText;
    private TextMeshProUGUI scText;


    private void Awake()
    {
        if (coinCounterInstance == null)
        {
            coinCounterInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ncText = ncCounter.GetComponent<TextMeshProUGUI>(); 
        scText = scCounter.GetComponent<TextMeshProUGUI>(); 
        updateNCCounter(0);
        updateSCCounter(0);
    }

    public void updateNCCounter(int amount)
    {
        ncAmount += amount;
        ncText.text = "NC: " + ncAmount.ToString();
    }
    public void updateSCCounter(int amount)
    {
        scAmount += amount;
        scText.text = "SC: " + scAmount.ToString();
    }

    public void resetNCCounter()
    {
        ncAmount = 0;
        ncText.text = "NC: " + ncAmount.ToString();
    }


}
