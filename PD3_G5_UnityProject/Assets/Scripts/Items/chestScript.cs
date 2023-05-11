using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    private PlayerStatsScript playerStats;
    public bool opened = false;
    private bool canTake = false;
    private PlayerHealthScript playerHealth;

    private void Start()
    {
        playerStats = PlayerStatsScript.playerStatsInstance;
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTake && Input.GetKeyDown(KeyCode.E))
        {
            opened = true;
            Canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void generateRandomReward()
    {
        int randomNumber = Random.Range(0, 100);
        if(randomNumber <= 60)
        {
            int itemType = Random.Range(0, 10);
            ConsumableAsset asset;
            switch (itemType)
            {
                case 1:
                    asset = new HealthAsset();
                    break;
                case 2:
                    asset = new AmmoAsset();
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
        else if (randomNumber <= 90)
        {

        }
        else{

        }

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))        
            canTake = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canTake = false;
    }

    private void OnDestroy()
    {
        Canvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ModifyCurrentMaxHealth(float amount)
    {
        Debug.Log("CHEST ITEM - MAX HEALTH");
        playerHealth.ModifyMaxHealth(amount);
        //Destroy(transform.parent.gameObject);
    }

    public void ModifyCurrentHealth(float amount)
    {
        playerHealth.ModifyHealth(amount);
        Destroy(transform.parent.gameObject);
    }

    public void ModifyCurrentSpeedBonus(int amount)
    {
        Debug.Log("CHEST ITEM - SPEED BONUS");
        playerStats.currentSpeedBonus += amount;
        Destroy(transform.parent.gameObject);
    }

    public void ModifyCurrentDamageBonus(int amount)
    {
        Debug.Log("CHEST ITEM - DAMAGE BONUS");
        playerStats.currentDamageBonus += amount;
        Destroy(transform.parent.gameObject);
    }
}
