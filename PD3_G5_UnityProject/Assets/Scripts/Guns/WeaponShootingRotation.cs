using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShootingRotation : MonoBehaviour
{

    [SerializeField] private int m_NCannons = 4;
    [SerializeField] private float m_RotationSpeed = 0.1f;
    private float m_RotationXShoot;
    private bool m_CanRotate;
    
    // Start is called before the first frame update
    void Start()
    {
        m_RotationXShoot = 360f / m_NCannons;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))//change
        {
            StartRotation();
            StartCoroutine(RotateWeapon(Vector3.forward, m_RotationXShoot, m_RotationSpeed));
            
        }
    }

    public void StartRotation()
    {
        m_CanRotate = true;
    }
    private IEnumerator RotateWeapon(Vector3 axis, float angle, float duration)
    {

        Quaternion from = transform.localRotation;
        Quaternion to = transform.localRotation;
        to *= Quaternion.Euler(0, 0, angle);
        
        float elapsed = 0.0f;
        while (elapsed < duration && m_CanRotate)
        {
            transform.localRotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.localRotation = to;
        m_CanRotate = false;
    }
    
}
