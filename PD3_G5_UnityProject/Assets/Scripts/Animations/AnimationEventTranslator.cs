using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationEventTranslator : MonoBehaviour
{

    [SerializeField] private GameObject IA_GO;

    [SerializeField] private EnemyHealthScript IA_Health;
    [SerializeField] private EnemyDesintegrationController IA_Desintegration;
    
    
    //General

    public void DestroyEnemy()
    {
        IA_Health.DestroyObject();
    }

    public void StartDesintegration()
    {
        IA_Desintegration.StartEnemyDesintegration();
    }
    
    //Chillón
    public void ChillonMeleeAttack()
    {
        IA_GO.GetComponent<MeleChaserEnemy>().attack();
    }
    
    
    //Arlequín
    
    public void ArlequinShoot()
    {
        IA_GO.GetComponent<RangedEnemyAIScript>().Shoot();
    }
    

    public void SpecialShoot()
    {
        //Pendiente de cambiar el codigo de RangedEnemyIA
        IA_GO.GetComponent<RangedEnemyAIScript>().Shoot();
    }
    
    

    // Gólem
}
