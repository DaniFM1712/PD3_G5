using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationEventTranslator : MonoBehaviour
{

    [SerializeField] private GameObject IA_GO;
    
    
    //Chillón
    public void ChillonMeleeAttack()
    {
        IA_GO.GetComponent<MeleChaserEnemy>().attack();
    }
    public void ChillonDeathEnd()
    {
        
    }
    
    
    //Arlequín
    
    public void ArlequinShoot()
    {
        IA_GO.GetComponent<RangedEnemyAIScript>().Shoot();
    }

    public void ArlequinDeathEnd()
    {
        
    }

    public void SpecialShoot()
    {
        //Pendiente de cambiar el codigo de RangedEnemyIA
        IA_GO.GetComponent<RangedEnemyAIScript>().Shoot();
    }
    
    

    // Gólem
}
