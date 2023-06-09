using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    public static HealthUIScript instance { get; private set; }

    [SerializeField] Image healthAmount;
    [SerializeField] float fillSmoothness = 0.01f;
    [SerializeField] GameObject hpCounter;
    [SerializeField] List<GameObject> damageFeedbackContainer;
    private TextMeshProUGUI hpText;
    private bool damageFeedback = false;

    private float screenDamageCooldown = 2f;
    private float screenDamageTimer;
    private bool screenDamageOnScreen = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        screenDamageTimer = screenDamageCooldown;
        hpText = hpCounter.GetComponent<TextMeshProUGUI>();
        updateHealth(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (screenDamageOnScreen)
        {
            screenDamageTimer -= Time.deltaTime;

            if(screenDamageTimer <= 0)
            {
                screenDamageOnScreen = false;
                damageFeedback = true;
                screenDamageTimer = screenDamageCooldown;
            }
        }

        if (damageFeedback)
        {
            float prevFill = 1;
            float currFill = 0;
            if (prevFill > 0)
            {
                foreach (GameObject obj in damageFeedbackContainer)
                {
                    prevFill = obj.GetComponent<Image>().fillAmount;
                    if (currFill > prevFill) prevFill = Mathf.Min(prevFill + fillSmoothness, currFill);
                    else if (currFill < prevFill) prevFill = Mathf.Max(prevFill - fillSmoothness, currFill);
                    obj.GetComponent<Image>().fillAmount = prevFill;
                }
            }
            else
            {
                damageFeedback = false;
            }

        }
    }

    public void updateHealth(bool isDamage)
    {

        if (isDamage)
        {
            ResetScreenDamage();
            screenDamageTimer = screenDamageCooldown;
            screenDamageOnScreen = true;
        }
        /*
        if (isDamage && damageFeedbackContainer[0].GetComponent<Image>().fillAmount == 0)
        {
            StartCoroutine(ShowDamage());
        }*/

        float normalizedHP = PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth();
        float hp = PlayerStatsScript.instance.currentHealth;
        
        hpText.text = hp + "";
        healthAmount.fillAmount = normalizedHP;
    }

    private void ResetScreenDamage()
    {
        damageFeedback = false;
        foreach (GameObject obj in damageFeedbackContainer)
        {
            obj.GetComponent<Image>().fillAmount = 1;
        }
    }

    /*
    IEnumerator ShowDamage()
    {
        foreach (GameObject obj in damageFeedbackContainer)
        {
            obj.GetComponent<Image>().fillAmount = 1;
        }
        yield return new WaitForSeconds(2f);
        damageFeedback = true;
    }
    */
}
