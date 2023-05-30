using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventConsumerScript : MonoBehaviour
{
    public static AnimatorEventConsumerScript instance { get; private set; }
    [Header("Animators")]
    [SerializeField] Animator rapidFireAnimator;
    [SerializeField] Animator shotgunAnimator;
    [SerializeField] Animator rocksAnimator;
    public bool shooting = false;
    public bool reloading = false;



    [SerializeField] AudioSource soundsAudioSource;
    [SerializeField] AudioSource musicAudioSource;

    [Header("Sound")]
    [SerializeField] AudioClip StepGrass;
    [SerializeField] AudioClip BackgroundMusic;
    [SerializeField] AudioClip RestartMusic;
    [SerializeField] AudioClip Jumps1;
    [SerializeField] AudioClip ShotGunSound;
    [SerializeField] AudioClip RapidFireSound;
    [SerializeField] AudioClip HitSound;
    [SerializeField] AudioClip Death;

    [Header("Particles")]
    [SerializeField] ParticleSystem walkStepsPart;
    [SerializeField] ParticleSystem jumpPart;
    [SerializeField] ParticleSystem lifePart;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);


        soundsAudioSource.PlayOneShot(RestartMusic);
        musicAudioSource.loop = true;
        musicAudioSource.clip = BackgroundMusic;
        Soundtrack();
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



    //----------AUDIOS----------//

    public void Step()
    {
        walkStepsPart.Play();
        soundsAudioSource.PlayOneShot(StepGrass);
    }
    
    public void Soundtrack()
    {
        musicAudioSource.Play();
    }

    public void StartMusic()
    {
        soundsAudioSource.PlayOneShot(RestartMusic);
    }

    void Jump1()
    {
        jumpPart.Play();
        soundsAudioSource.PlayOneShot(Jumps1);
    }

    public void ShotGunEvent()
    {
        soundsAudioSource.PlayOneShot(ShotGunSound);
    }
    public void RapidFireEvent()
    {
        soundsAudioSource.PlayOneShot(RapidFireSound);
    }

    public void HitSoundEvent()
    {
        soundsAudioSource.PlayOneShot(HitSound);
    }

    public void DeathSoundEvent()
    {
        soundsAudioSource.PlayOneShot(Death);
    }

    public void StopSounds()
    {
        musicAudioSource.Stop();
    }

    public void StartSounds()
    {
        soundsAudioSource.PlayOneShot(RestartMusic);
        musicAudioSource.Play();
    }


}
