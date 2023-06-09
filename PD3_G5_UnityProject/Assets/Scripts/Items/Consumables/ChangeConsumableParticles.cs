using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ChangeConsumableParticles : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] VisualEffect particles;

    Color colorloot;
    float particlesRate;
    Vector3 flareSize;
    float flareRate;
    float circleMeshRate;
    float circleMeshLife;
    float circleMeshSize;
    float circleMeshVelocity;

    public void setItemCommon()
    {
        colorloot = new Color(0, 2.7f, 4, 0);
        particlesRate = 10;
        flareSize = new Vector3(1f, 0.2f, 1f);
        flareRate = 0;
        circleMeshRate = 0;
        circleMeshLife = 0;
        circleMeshSize = 0;
        circleMeshVelocity = 0;
        setStats();
    }


    public void setItemRare()
    {
        colorloot = new Color(4, 0, 2.7f, 0);
        particlesRate = 40;
        flareSize = new Vector3(1.1f, 0.2f, 1f);
        flareRate = 1;
        circleMeshRate = 1;
        circleMeshLife = 1;
        circleMeshSize = 8;
        circleMeshVelocity = 0.6f;
        setStats();
    }


    public void setItemLegendary()
    {
        colorloot = new Color(4,2.7f,0,0);
        particlesRate = 120;
        flareSize = new Vector3(1.5f, 0.2f, 1f);
        flareRate = 1;
        circleMeshRate = 3;
        circleMeshLife = 5;
        circleMeshSize = 10;
        circleMeshVelocity = 0.6f;
        setStats();
    }


    void setStats()
    {
        particles.SetVector4("ColorLoot", colorloot);
        particles.SetFloat("ParticlesRate", particlesRate);
        particles.SetVector3("FlareSize", flareSize);
        particles.SetFloat("FlareRate", flareRate);
        particles.SetFloat("CircleMeshRate", circleMeshRate);
        particles.SetFloat("CircleMeshLife", circleMeshLife);
        particles.SetFloat("CircleMeshSize", circleMeshSize);
        particles.SetFloat("CircleMeshVelocity", circleMeshVelocity);
        particles.Play();
    }
}