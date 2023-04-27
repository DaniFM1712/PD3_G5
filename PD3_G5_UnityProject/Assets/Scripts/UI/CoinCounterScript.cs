using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounterScript : MonoBehaviour
{
    [SerializeField] GameObject ncCounter;
    [SerializeField] GameObject scCounter;
    public static CoinCounterScript coinCounterInstance { get; private set; }
    private TextMeshProUGUI ncText;
    private TextMeshProUGUI scText;
    private PlayerStatsScript playerStats;


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
        playerStats = PlayerStatsScript.playerStatsInstance;
        ncText = ncCounter.GetComponent<TextMeshProUGUI>(); 
        scText = scCounter.GetComponent<TextMeshProUGUI>(); 
        updateNCCounter(0);
        updateSCCounter(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            updateNCCounter(5);
            updateSCCounter(1);
        }
    }

    public void updateNCCounter(int amount)
    {
        playerStats.currentNormalCoin += amount;
        ncText.text = "NC: " + playerStats.currentNormalCoin.ToString();
    }
    public void updateSCCounter(int amount)
    {
        playerStats.currentSpecialCoin += amount;
        scText.text = "SC: " + playerStats.currentSpecialCoin.ToString();
    }

    public void resetNCCounter()
    {
        playerStats.currentNormalCoin = 0;
        ncText.text = "NC: " + playerStats.currentNormalCoin.ToString();
    }


}