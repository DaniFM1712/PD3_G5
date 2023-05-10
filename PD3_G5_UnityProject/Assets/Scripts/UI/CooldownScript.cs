using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CooldownScript : MonoBehaviour
{
    [SerializeField] float dashCooldown;
    [SerializeField] float abilityCooldown;
    [SerializeField] float grenadeCooldown;
    [SerializeField] Image dashCDImage;
    [SerializeField] Image abilityCDImage;
    [SerializeField] Image grenadeCDImage;
    float dashCDTime;
    float abilityCDTime;
    float grenadeCDTime;
    bool dashInCD;
    bool abilityInCD;
    bool grenadeInCD;


    // Start is called before the first frame update
    void Start()
    {
        dashInCD = false;
        abilityInCD = false;
        grenadeInCD = false;
        dashCDImage.enabled = false;
        abilityCDImage.enabled = false;
        grenadeCDImage.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        
        if (dashInCD)
        {
            dashCDTime -= Time.deltaTime;
            dashCDImage.fillAmount = dashCDTime/ dashCooldown;
            if (dashCDTime <= 0f)
            {
                dashInCD = false;
                dashCDTime = dashCooldown;
                dashCDImage.fillAmount = 1;
                dashCDImage.enabled = false;
            }
        }

        if (abilityInCD)
        {
            abilityCDTime -= Time.deltaTime;
            abilityCDImage.fillAmount = abilityCDTime/ abilityCooldown;
            if (abilityCDTime <= 0f)
            {
                abilityInCD = false;
                abilityCDTime = abilityCooldown;
                abilityCDImage.fillAmount = 1;
                abilityCDImage.enabled = false;
            }
        }
        
        if (grenadeInCD)
        {
            grenadeCDTime -= Time.deltaTime;
            grenadeCDImage.fillAmount = grenadeCDTime/ grenadeCooldown;
            if (grenadeCDTime <= 0f)
            {
                grenadeInCD = false;
                grenadeCDTime = abilityCooldown;
                grenadeCDImage.fillAmount = 1;
                grenadeCDImage.enabled = false;
            }
        }

    }

    public void StartDashCooldown(float cooldown)
    {
        dashCooldown = cooldown;
        dashCDTime = dashCooldown;
        dashInCD = true;
        dashCDImage.enabled = true;
    }

    public void StartAbilityCooldown(float cooldown)
    {
        abilityCooldown = cooldown;
        abilityCDTime = abilityCooldown;
        abilityInCD = true;
        abilityCDImage.enabled = true;
    } 
    
    public void StartGrenadeCooldown(float cooldown)
    {
        grenadeCooldown = cooldown;
        grenadeCDTime = grenadeCooldown;
        grenadeInCD = true;
        grenadeCDImage.enabled = true;
    }
}
