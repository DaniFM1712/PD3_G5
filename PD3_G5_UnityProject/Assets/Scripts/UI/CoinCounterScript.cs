using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounterScript : MonoBehaviour
{
    [SerializeField] GameObject ncCounter;
    [SerializeField] GameObject scCounter;
    [SerializeField] GameObject ncGO;
    [SerializeField] GameObject scGO;
    public static CoinCounterScript coinCounterInstance { get; private set; }
    private TextMeshProUGUI ncText;
    private TextMeshProUGUI scText;
    private PlayerStatsScript playerStats;
    private int fifties = 0;
    private int accumulatedFifties = 0;

    [Header("FMOD")]
    public StudioEventEmitter coinEmitter;
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
        playerStats = PlayerStatsScript.instance;
        ncText = ncCounter.GetComponent<TextMeshProUGUI>(); 
        scText = scCounter.GetComponent<TextMeshProUGUI>(); 
        updateNCCounter(0);
        updateSCCounter(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            updateNCCounter(100);
            updateSCCounter(100);
        }
    }


    public void updateNCCounter(int amount)
    {
        if(amount > 0)
            StartCoroutine(CounterShake(amount, true));

        else
        {
            playerStats.currentNormalCoin += amount;
            ncText.text = " " + playerStats.currentNormalCoin.ToString();
        }

        if (PlayerStatsScript.instance.moneyIsPower)
        {
            if (accumulatedFifties < (int)(PlayerStatsScript.instance.currentNormalCoin / 50f))
            {
                fifties = (int)(PlayerStatsScript.instance.currentNormalCoin / 50f) - accumulatedFifties;
                accumulatedFifties += fifties;
            }
            float accumulatedDamage = 0.1f * fifties;
            PlayerStatsScript.instance.currentDamageMultiplyer += accumulatedDamage;
        }

    }
    public void updateSCCounter(int amount)
    {
        if(amount>0)
            StartCoroutine(CounterShake(amount, false));
        else
        {
            playerStats.currentSpecialCoin += amount;
            scText.text = " " + playerStats.currentSpecialCoin.ToString();
        }
    }

    IEnumerator CounterShake(int amount, bool normalCoin)
    {
        if (normalCoin)
        {
            for (int i = 0; i < amount; i++)
            {
                LeanTween.moveLocal(ncGO, new Vector3(10, 0, 0), 0.1f).setOnComplete(() =>
                {
                    coinEmitter.Play();
                    playerStats.currentNormalCoin++;
                    ncText.text = " " + playerStats.currentNormalCoin.ToString();
                    LeanTween.moveLocal(ncGO, new Vector3(0, 0, 0), 0.2f);
                });
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                LeanTween.moveLocal(scGO, new Vector3(10, 0, 0), 0.1f).setOnComplete(() =>
                {
                    playerStats.currentSpecialCoin++; ;
                    scText.text = " " + playerStats.currentSpecialCoin.ToString();
                    LeanTween.moveLocal(scGO, new Vector3(0, 0, 0), 0.2f);
                });
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void resetNCCounter()
    {

        playerStats.currentNormalCoin = 0;
        ncText.text = " " + playerStats.currentNormalCoin.ToString();
    }

    public void resetMoneyIsPower()
    {
        PlayerStatsScript.instance.currentDamageMultiplyer -= 0.1f * accumulatedFifties;
    }


}
