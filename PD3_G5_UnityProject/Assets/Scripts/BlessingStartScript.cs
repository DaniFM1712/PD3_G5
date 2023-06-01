using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class BlessingStartScript : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    private int blessingIndex;
    private ParentBlessing[] blessings;
    [SerializeField] TextMeshProUGUI blessingName1;
    [SerializeField] TextMeshProUGUI blessingName2;
    [SerializeField] TextMeshProUGUI blessingName3;
    [SerializeField] TextMeshProUGUI blessingDescription1;
    [SerializeField] TextMeshProUGUI blessingDescription2;
    [SerializeField] TextMeshProUGUI blessingDescription3;
    private ParentBlessing blessing1;
    private ParentBlessing blessing2;
    private ParentBlessing blessing3;



    // Start is called before the first frame update
    void Start()
    {
        if(LevelManager.instance.getCurrentSceneIndex() != LevelManager.instance.getPreviousSceneIndex())
        {
            Debug.Log(LevelManager.instance.getCurrentSceneIndex()+"actual");
            Debug.Log(LevelManager.instance.getPreviousSceneIndex() + "anterior");
            blessings = FindObjectsByType<ParentBlessing>(FindObjectsSortMode.InstanceID);
            Time.timeScale = 0f;
            generateBlessings();
            Canvas.SetActive(true); 
            Cursor.lockState = CursorLockMode.None;
        }
    }
    



    public void generateBlessings()
    {
        //Hem d'evitar repetits
        List<int> usedIndexes = new List<int>();

        Debug.Log("BLESSINGS LENGTH: "+blessings.Length);
        if (PlayerStatsScript.instance.activatedBlessings < PlayerStatsScript.instance.maxBlessings)
        {
            blessingIndex = Random.Range(0, blessings.Length );
            while (blessings[blessingIndex].enabled || blessings[blessingIndex].blessingType != ParentBlessing.BlessingType.Dash
                || usedIndexes.Contains(blessingIndex))
            {
                blessingIndex = Random.Range(0, blessings.Length);
                //Hem d'evitar aixo si totes les blessings ja estan activades
            }
            usedIndexes.Add(blessingIndex);
            blessing1 = blessings[blessingIndex];

            blessingIndex = Random.Range(0, blessings.Length );
            while (blessings[blessingIndex].enabled || blessings[blessingIndex].blessingType != ParentBlessing.BlessingType.Grenade
                || usedIndexes.Contains(blessingIndex))
            {
                blessingIndex = Random.Range(0, blessings.Length);
                //Hem d'evitar aixo si totes les blessings ja estan activades
            }
            usedIndexes.Add(blessingIndex);
            blessing2 = blessings[blessingIndex];

            blessingIndex = Random.Range(0, blessings.Length );
            if(PlayerStatsScript.instance.currentWeaponIndex == 1)
            {
                while (blessings[blessingIndex].enabled || blessings[blessingIndex].blessingType != ParentBlessing.BlessingType.RapidFire
                || usedIndexes.Contains(blessingIndex))
                {
                    blessingIndex = Random.Range(0, blessings.Length);
                    //Hem d'evitar aixo si totes les blessings ja estan activades
                }
            }
            else if(PlayerStatsScript.instance.currentWeaponIndex == 2)
            {

                while (blessings[blessingIndex].enabled || blessings[blessingIndex].blessingType != ParentBlessing.BlessingType.ShotGun
                || usedIndexes.Contains(blessingIndex))
                {
                    blessingIndex = Random.Range(0, blessings.Length);
                    //Hem d'evitar aixo si totes les blessings ja estan activades
                }
            }
            
            usedIndexes.Add(blessingIndex);
            blessing3 = blessings[blessingIndex];
            //blessings[blessingIndex].enabled = true;
            //PlayerStatsScript.playerStatsInstance.activatedBlessings++;
        }

        blessingName1.text = blessing1.blessingName;
        blessingDescription1.text = blessing1.blessingDescription;
       
        blessingName2.text = blessing2.blessingName;
        blessingDescription2.text = blessing2.blessingDescription;
        
        blessingName3.text = blessing3.blessingName;
        blessingDescription3.text = blessing3.blessingDescription;

    }

    public void activateBlessing1()
    {
        blessing1.enabled = true;
        PlayerStatsScript.instance.activatedBlessings++;
        InventoryManagerScript.InventoryInstance.UpdateBlessingsUI();
        Destroy(gameObject);
    }
    public void activateBlessing2()
    {
        blessing2.enabled = true;
        PlayerStatsScript.instance.activatedBlessings++;
        InventoryManagerScript.InventoryInstance.UpdateBlessingsUI();
        Destroy(gameObject);
    }
    public void activateBlessing3()
    {
        blessing3.enabled = true;
        PlayerStatsScript.instance.activatedBlessings++;
        InventoryManagerScript.InventoryInstance.UpdateBlessingsUI();
        Destroy(gameObject);
    }



    private void OnDestroy()
    {
        Time.timeScale = 1f;
        Canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
