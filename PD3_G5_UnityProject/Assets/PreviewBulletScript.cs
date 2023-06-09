using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBulletScript : MonoBehaviour
{
    [SerializeField] GameObject specialEffectPrefab;
    public EnemyGolemBulletScript bullet;

    private GameObject specialEffect;
    Vector3 originPosition = new Vector3(0f, 0f, 0f);


    private void Start()
    {
        specialEffect = Instantiate(specialEffectPrefab, originPosition, Quaternion.identity);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Terrain"))
        {
            Vector3 vector3 = transform.position;
            specialEffect.SetActive(true);
            specialEffect.transform.position = vector3;
            specialEffect.transform.rotation = Quaternion.identity;
            ReturnToOrigin();
        }
    }

    private void ReturnToOrigin()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        transform.position = originPosition;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    public void HidePreview()
    {
        specialEffect.SetActive(false);
        specialEffect.transform.position = originPosition;
        specialEffect.transform.rotation = Quaternion.identity;
    }
}
