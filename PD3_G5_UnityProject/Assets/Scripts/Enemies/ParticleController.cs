using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particles;
    public void playParticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i] != null)
                particles[i].Play();
        }
    }
}
