using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    public static HealthUIScript instance { get; private set; }

    [SerializeField] Image healthAmount;
    [SerializeField] GameObject hpCounter;
    private TextMeshProUGUI hpText;

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
        updateHealth(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth()
    {
        float normalizedHP = PlayerStatsScript.instance.currentHealth / PlayerStatsScript.instance.GetCurrentMaxHealth();
        float hp = PlayerStatsScript.instance.currentHealth;
        
        hpText.text = hp + "";
        healthAmount.fillAmount = normalizedHP;

    }
}
