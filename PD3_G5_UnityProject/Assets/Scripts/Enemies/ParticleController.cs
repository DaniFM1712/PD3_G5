using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particlesTrap;
    [SerializeField] ParticleSystem[] particlesSlow;
    public void playParticlesTrap()
    {
        for (int i = 0; i < particlesTrap.Length; i++)
        {
            if (particlesTrap[i] != null)
                particlesTrap[i].Play();
        }
    }

    public void playParticlesSlow()
    {
        for (int i = 0; i < particlesSlow.Length; i++)
        {
            if (particlesSlow[i] != null)
                particlesSlow[i].Play();
        }
    }

    public void stopParticlesSlow()
    {
        for (int i = 0; i < particlesSlow.Length; i++)
        {
            if (particlesSlow[i] != null)
                particlesSlow[i].Stop();
        }
    }
}
