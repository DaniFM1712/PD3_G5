using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShootingRotation : MonoBehaviour
{

    [SerializeField] private int m_NCannons = 4;
    private float m_RotationSpeed = 0.1f;
    private float m_RotationXShoot;
    private bool m_CanRotate;
    
    void Start()
    {
        m_RotationXShoot = 360f / m_NCannons;
    }
    
    void Update()
    {
        if (m_CanRotate)
        {
            StartCoroutine(RotateWeapon());
        }
    }

    public void StartRotation(float l_RotationTime)
    {
        m_RotationSpeed = l_RotationTime;
        m_CanRotate = true;
    }
    private IEnumerator RotateWeapon()
    {

        Quaternion l_From = transform.localRotation;
        Quaternion l_To = transform.localRotation;
        l_To *= Quaternion.Euler(0, 0, m_RotationXShoot);
        
        float elapsed = 0.0f;
        while (elapsed < m_RotationSpeed && m_CanRotate)
        {
            transform.localRotation = Quaternion.Slerp(l_From, l_To, elapsed / m_RotationSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.localRotation = l_To;
        m_CanRotate = false;
    }

}
