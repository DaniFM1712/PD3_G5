using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Extret del tutorial:
// Building a Localisation Tool in Unity - Part 1, de Game Dev Guide
// https://www.youtube.com/watch?v=c-dzg4M20wY

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliserUI : MonoBehaviour
{
    protected TextMeshProUGUI textField;
    public string key;

    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalisationSystem.GetLocalisedValue(key);
        textField.text = value;
    }

    public virtual void Refresh()
    {
        // Refreshes the current text on the bubble.
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalisationSystem.GetLocalisedValue(key);
        textField.text = value;
    }
}
