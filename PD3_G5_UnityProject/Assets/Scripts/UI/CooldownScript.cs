using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CooldownScript : MonoBehaviour
{
    float dashCooldown;
    float abilityCooldown;
    float grenadeCooldown;
    [SerializeField] Image dashCDImage;
    [SerializeField] Image abilityCDImage;
    [SerializeField] Image grenadeCDImage;

    GameObject dashCDGO;
    GameObject abilityCDGO;
    GameObject grenadeCDGO;
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

        dashCDGO = dashCDImage.transform.parent.gameObject;
        abilityCDGO = abilityCDImage.transform.parent.gameObject;
        grenadeCDGO = grenadeCDImage.transform.parent.gameObject;

        if (!PlayerStatsScript.instance.dashUnlocked)
        {
            dashCDImage.GetComponent<Image>().enabled = true;
        }
        if (!PlayerStatsScript.instance.grenadeUnlocked)
        {
            grenadeCDImage.GetComponent<Image>().enabled = true;
        }

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
                LeanTween.moveLocal(dashCDGO, new Vector3(-10, 15, 0), 0.1f).setOnComplete(() =>
                {
                    LeanTween.moveLocal(dashCDGO, new Vector3(-10, 0, 0), 0.2f);
                });
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

                LeanTween.moveLocal(grenadeCDGO, new Vector3(261, 15, 0), 0.1f).setOnComplete(() =>
                {
                    LeanTween.moveLocal(grenadeCDGO, new Vector3(261, 0, 0), 0.2f);
                });
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
                LeanTween.moveLocal(abilityCDGO, new Vector3(124.9999f, 15, 0), 0.1f).setOnComplete(() =>
                {
                    LeanTween.moveLocal(abilityCDGO, new Vector3(124.9999f, 0, 0), 0.2f);
                });
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
    public void ReduceDashCooldown(float time)
    {
        if (dashInCD)
        {
            dashCDTime -= time;
        }
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

    public void ResetAbilityCooldown()
    {

        if (abilityInCD)
        {
            abilityCDTime = 0f;
        }
    }

}
