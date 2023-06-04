using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class AnimatorEventConsumerScript : MonoBehaviour
{
    public static AnimatorEventConsumerScript instance { get; private set; }
    [Header("Animators")]
    [SerializeField] Animator rapidFireAnimator;
    [SerializeField] Animator shotgunAnimator;
    [SerializeField] Animator rocksAnimator;
    FMOD.Studio.Bus master;
    public bool shooting = false;
    public bool reloading = false;


    /*[Header("Particles")]
    [SerializeField] ParticleSystem walkStepsPart;
    [SerializeField] ParticleSystem jumpPart;
    [SerializeField] ParticleSystem lifePart;*/




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);



    }

    private void Start()
    {
        master = FMODUnity.RuntimeManager.GetBus("bus:/Master Bus");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("pujo");
            IncreaseVolume();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("baixo");
            DecreaseVolume();
        }
    }


    //----------ANIMATIONS----------//
    public void startWalkAnimation()
    {
        if (PlayerStatsScript.instance.currentWeaponIndex == 1)
        {
            if (!shooting && !reloading)
                rapidFireAnimator.SetInteger("State", 1);
        }
        if (PlayerStatsScript.instance.currentWeaponIndex == 2)
        {
            if (!shooting && !reloading) {
                shotgunAnimator.SetInteger("State", 1);
                rocksAnimator.SetInteger("State", 1);
            }
                
        }
    }

    public void startIdleAnimation()
    {
        if(PlayerStatsScript.instance.currentWeaponIndex == 1) {
            if (!shooting && !reloading)
                rapidFireAnimator.SetInteger("State", 0);
        }
        if (PlayerStatsScript.instance.currentWeaponIndex == 2)
        {
            if (!shooting && !reloading)
            {
                shotgunAnimator.SetInteger("State", 0);
                rocksAnimator.SetInteger("State", 0);
            }
        }
            
    }

    public void startShootAnimation()
    {
        if (PlayerStatsScript.instance.currentWeaponIndex == 1)
        {
            if(!reloading)
                rapidFireAnimator.SetInteger("State", 2);
        }
        if (PlayerStatsScript.instance.currentWeaponIndex == 2)
        {
            if (!reloading)
            {
                shotgunAnimator.SetInteger("State", 2);
                rocksAnimator.SetInteger("State", 2);
            }
        }
    }

    public void startReloadAnimation()
    {
        if (PlayerStatsScript.instance.currentWeaponIndex == 1)
        {
            rapidFireAnimator.SetInteger("State", 3);
        }
        if (PlayerStatsScript.instance.currentWeaponIndex == 2)
        {
            shotgunAnimator.SetInteger("State", 3);
            rocksAnimator.SetInteger("State", 3);
        }
    }



    public void IncreaseVolume()
    {
        master.getVolume(out float volume);
        master.setVolume(volume + 10f);
    }
    
    public void DecreaseVolume()
    {
        master.getVolume(out float volume);
        master.setVolume(volume - 10f);
    }

}
