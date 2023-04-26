using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    private PlayerStatsScript playerStats;
    public bool opened= false;
    private bool canTake = false;

    private void Start()
    {
        playerStats = PlayerStatsScript.playerStatsInstance;
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
        Debug.Log("CHEST ITEM");
        playerStats.currentMaxHealth += amount;
        //Destroy(transform.parent.gameObject);
    }

    public void ModifyCurrentHealth(float amount)
    {
        playerStats.currentHealth += amount;
        Destroy(transform.parent.gameObject);
    }

    public void ModifyCurrentSpeedBonus(int amount)
    {
        Debug.Log("CHEST ITEM");
        playerStats.currentSpeedBonus += amount;
        Destroy(transform.parent.gameObject);
    }

    public void ModifyCurrentDamageBonus(int amount)
    {
        Debug.Log("CHEST ITEM");
        playerStats.currentDamageBonus += amount;
        Destroy(transform.parent.gameObject);
    }
}
