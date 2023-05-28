using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPartScript : MonoBehaviour
{
    [SerializeField] float damageMultiplyer = 1f;
    private float currentHealth = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage(float damage, GameObject bullet)
    {

        if (transform.parent.gameObject.TryGetComponent<EnemyHealthScript>(out EnemyHealthScript health))
        {
            currentHealth = health.GetCurrentHealth();
            currentHealth -= (damage * damageMultiplyer);
            bool dead = health.TakeDamage(damage * damageMultiplyer);
            return dead;

            /*
            if(bullet.TryGetComponent<SGSpecialBulletScript>(out SGSpecialBulletScript sgBullet))
            {
                if(currentHealth<= 0 && sgBullet.weaponScript != null)
                {
                    sgBullet.weaponScript.ResetCooldown();
                }
            }*/
        }
        return false;
    }
}
