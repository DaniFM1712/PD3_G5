using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetBlessingScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI dashText;
    [SerializeField] TextMeshProUGUI grenadeText;
    [SerializeField] TextMeshProUGUI weaponText;
    private int dashRuneIndex;
    private int grenadeRuneIndex;
    private int weaponRuneIndex;
    private PlayerStatsScript playerStats;
    enum Ability {Dash,Grenade,Weapon};

    private void Start()
    {
        playerStats = PlayerStatsScript.playerStatsInstance;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        canvas.SetActive(true);
        generateBlessings();
    }


    private void generateBlessings()
    {
        dashRuneIndex = Random.Range(0, 5);
        while (playerStats.currentDashAbilities[dashRuneIndex] == true)
        {
            dashRuneIndex = Random.Range(0, 5);
        }
        //extreureString
        dashText.text = "MEJORA DE DASH";
    }


    public void dashRuneSelected()
    {
        playerStats.currentDashAbilities[dashRuneIndex] = true;
        switch (dashRuneIndex)
        {
            case 0:
                break; 
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    private void giveBlessing(Ability abilityType,int abilityNum)
    {
        //Donar millora
        switch (abilityType) {
            case Ability.Dash:
                //Millora 1if abilnum 1 (){}
                playerStats.currentHealth -= 10;
                break;
            case Ability.Grenade:
                break;
            case Ability.Weapon:
                break;     
        }

        Time.timeScale = 1f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(false);
        Destroy(this);
    }

 

}
