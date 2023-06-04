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

    private float maxHealth;

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
        hpText = hpCounter.GetComponent<TextMeshProUGUI>();
        updateHealth(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (damageFeedback)
        {
            foreach (GameObject obj in damageFeedbackContainer)
            {
                float prevFill = obj.GetComponent<Image>().fillAmount;
                float currFill = (float)prevFill/ 1;
                if (currFill > prevFill) prevFill = Mathf.Min(prevFill + fillSmoothness, currFill);
                else if (currFill < prevFill) prevFill = Mathf.Max(prevFill - fillSmoothness, currFill);
                obj.GetComponent<Image>().fillAmount = prevFill;
            }
            damageFeedback = false;

        }
    }

    public void updateHealth(bool isDamage)
    {
        if(isDamage)
            damageFeedback = true;
        float normalizedHP = PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth();
        float hp = PlayerStatsScript.instance.currentHealth;
        
        hpText.text = hp + "";
        healthAmount.fillAmount = normalizedHP;

    }


}
