using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextScript : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Color damageColour;
    [SerializeField] private Vector3 randomizeOffeset;

    private TextMeshPro damageText;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
        transform.localPosition += offset;
        transform.localPosition += new Vector3(
            Random.Range(-randomizeOffeset.x,randomizeOffeset.x),
            Random.Range(-randomizeOffeset.y,randomizeOffeset.y),
            Random.Range(-randomizeOffeset.z,randomizeOffeset.z));
        Destroy(gameObject,destroyTime);
    }

    public void Initialise(float damageValue)
    {
        damageText.text = damageValue.ToString();
    }

}
