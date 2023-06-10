using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using FMODUnity;
using UnityEngine.UI;

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
    [SerializeField] GameObject blessingBut1;
    [SerializeField] GameObject blessingBut2;
    private ParentBlessing blessing1;
    private ParentBlessing blessing2;
    private ParentBlessing blessing3;
    [Header("FMOD")]
    public StudioEventEmitter SelectEmitter;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(LevelManager.instance.getCurrentSceneIndex() + "actual");
        Debug.Log(LevelManager.instance.getPreviousSceneIndex() + "anterior");
        if (LevelManager.instance.getCurrentSceneIndex() != LevelManager.instance.getPreviousSceneIndex())
        {
            blessings = FindObjectsByType<ParentBlessing>(FindObjectsSortMode.InstanceID);
            Time.timeScale = 0f;
            generateBlessings();
            Canvas.SetActive(true); 
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Canvas.SetActive(false);
        }
    }
    



    public void generateBlessings()
    {
        //Hem d'evitar repetits
        List<int> usedIndexes = new List<int>();

        Debug.Log("BLESSINGS LENGTH: "+blessings.Length);
        if (PlayerStatsScript.instance.activatedBlessings < PlayerStatsScript.instance.maxBlessings)
        {
            if (PlayerStatsScript.instance.dashUnlocked) { 
                blessingIndex = Random.Range(0, blessings.Length );
                while (blessings[blessingIndex].enabled || blessings[blessingIndex].blessingType != ParentBlessing.BlessingType.Dash
                    || usedIndexes.Contains(blessingIndex))
                {
                    blessingIndex = Random.Range(0, blessings.Length);
                    //Hem d'evitar aixo si totes les blessings ja estan activades
                }
                usedIndexes.Add(blessingIndex);
                blessing1 = blessings[blessingIndex];
            }
            else
            {
                //parafernalias
            }

            if (PlayerStatsScript.instance.grenadeUnlocked)
            {
                blessingIndex = Random.Range(0, blessings.Length);
                while (blessings[blessingIndex].enabled || blessings[blessingIndex].blessingType != ParentBlessing.BlessingType.Grenade
                    || usedIndexes.Contains(blessingIndex))
                {
                    blessingIndex = Random.Range(0, blessings.Length);
                    //Hem d'evitar aixo si totes les blessings ja estan activades
                }
                usedIndexes.Add(blessingIndex);
                blessing2 = blessings[blessingIndex];
            }

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
        if(blessing1 != null)
        {
            blessingName1.text = blessing1.blessingName;
            blessingDescription1.text = blessing1.blessingDescription;
            blessingBut1.GetComponent<Button>().interactable = true;
            //blessingBut1.transform.localScale = new Vector3(blessingBut1.transform.localScale.x/2 , blessingBut1.transform.localScale.y / 2, blessingBut1.transform.localScale.z / 2);
        }
        if(blessing2 != null)
        {
            blessingName2.text = blessing2.blessingName;
            blessingDescription2.text = blessing2.blessingDescription;
            blessingBut2.GetComponent<Button>().interactable = true;
        }

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
        SelectEmitter.Play();
        Time.timeScale = 1f;
        Canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
