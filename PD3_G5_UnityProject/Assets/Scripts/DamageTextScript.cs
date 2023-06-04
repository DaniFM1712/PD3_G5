using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextScript : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 randomizeOffeset;
    [SerializeField] private Color damageColour;
    [SerializeField] private Color criticalDamageColour;


    private TextMeshPro damageText;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
        damageText.color = damageColour;
        transform.localPosition += offset;
        transform.localPosition += new Vector3(
            Random.Range(-randomizeOffeset.x,randomizeOffeset.x),
            Random.Range(-randomizeOffeset.y,randomizeOffeset.y),
            Random.Range(-randomizeOffeset.z,randomizeOffeset.z));
        Destroy(gameObject,destroyTime);
    }

    public void Initialise(float damageValue, bool critical)
    {
        if (critical)
        {
            damageText.color = criticalDamageColour;

        }
        else
        {
            damageText.color = damageColour;
        }
        damageText.text = Round(damageValue,2).ToString();
    }
    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
}
