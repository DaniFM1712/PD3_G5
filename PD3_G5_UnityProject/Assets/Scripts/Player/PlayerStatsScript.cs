using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    public static PlayerStatsScript playerStatsInstance { get; private set; }

    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    public float maxShield { get; set; }
    public float currentShield { get; set; }

    public int gunStrength { get; set; }
    public int speed { get; set; }


    private void Awake()
    {
        if (playerStatsInstance == null)
        {
            playerStatsInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
