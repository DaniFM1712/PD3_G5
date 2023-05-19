using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventConsumerScript : MonoBehaviour
{
    // Start is called before the first frame update
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
        DontDestroyOnLoad(gameObject);


        soundsAudioSource.PlayOneShot(RestartMusic);
        musicAudioSource.loop = true;
        musicAudioSource.clip = BackgroundMusic;
        Soundtrack();
    }

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
