using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class AnimationEventTranslator : MonoBehaviour
{

    [SerializeField] private GameObject IA_GO;

    [SerializeField] private EnemyHealthScript IA_Health;
    [SerializeField] private EnemyDesintegrationController IA_Desintegration;
    [SerializeField] private VisualEffect IA_SoulVE;
    
    public StudioEventEmitter cascabelEmitter;

    //General

    public void DestroyEnemy()
    {
        IA_Health.DestroyObject();
    }

    public void SoulActivation()
    {
        IA_SoulVE.Play();
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
    public void ArlequinStep()
    {
        cascabelEmitter.Play();
    }
    

    public void SpecialShoot()
    {
        //Pendiente de cambiar el codigo de RangedEnemyIA
        IA_GO.GetComponent<RangedEnemyAIScript>().Shoot();
    }
    
    

    // Gólem

    public void GolemMeleeAttack()
    {
        IA_GO.GetComponent<GolemEnemyAIScript>().MeleeAttack();
    }

    public void GolemThrowRock()
    {
        IA_GO.GetComponent<GolemEnemyAIScript>().Shoot();
    }
}
