using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyDesintegrationController : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> skinnedMesh;
    [SerializeField] private float desintegrateRate = 0.0125f;
    [SerializeField] private float refreshRate = 0.025f;
    private List<Material> skinnedMaterial = new List<Material>();

    [SerializeField] private VisualEffect VFXGraph;

    // Start is called before the first frame update
    void Start()
    {
        if (skinnedMesh != null)
        {
            SetMaterials();
        }
    }

    void SetMaterials()
    {
        for (int j = 0; j < skinnedMesh.Count; j++)
        {
            skinnedMaterial.Add(skinnedMesh[j].material);
        }
    }
/*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("Starting Coroutine");
            StartCoroutine(DesintegrateEnemy());
        }
    }
    */

    public void StartEnemyDesintegration()
    {
        print("Method desintegrate is called");
        StartCoroutine(DesintegrateEnemy());
    } 

    private IEnumerator DesintegrateEnemy()
    {
        print("Method desintegrate is called");
        //Particles start
        if (VFXGraph != null)
        {
            VFXGraph.Play();
        }
        
        //Desintegration process
        if (skinnedMaterial.Count > 0)
        {
            float counter = 0;
            while (skinnedMaterial[0].GetFloat("_Desintegration_Amount") < 1)
            {
                counter += desintegrateRate;
                for (int i = 0; i < skinnedMaterial.Count; i++)
                {
                    skinnedMaterial[i].SetFloat("_Desintegration_Amount", counter);
                }

                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}