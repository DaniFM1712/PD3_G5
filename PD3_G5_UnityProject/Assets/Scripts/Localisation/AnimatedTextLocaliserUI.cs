using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimatedTextLocaliserUI : TextLocaliserUI
{
    public float letterDuration = 0.03f;
    public float fadeUpOffset = 20f;
    public float fadeUpDuration = 0.5f;
    bool _isAnimating;
    public void RefreshAnimatedLetters()
    {
        if (_isAnimating) { return; }
        _isAnimating = true;
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalisationSystem.GetLocalisedValue(key);
        AnimateLetters(value);
    }

    public void RefreshAnimatedLetters(string key)
    {
        this.key = key;
        RefreshAnimatedLetters();
    }

    public void RefreshFadeUp()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalisationSystem.GetLocalisedValue(key);
        textField.text = value;
        FadeUpPositionAnimation();
        FadeUpAlphaAnimation();
    }

    public void RefreshFadeUp(string key)
    {
        this.key = key;
        RefreshFadeUp();
    }

    private void FadeUpAlphaAnimation()
    {
        LeanTween.alphaText(textField.rectTransform, 0f, 0f);
        LeanTween.alphaText(textField.rectTransform, 1f, fadeUpDuration);
    }

    private void FadeUpPositionAnimation()
    {
        float targetPosition = textField.rectTransform.anchoredPosition.y;
        textField.rectTransform.anchoredPosition = new Vector2(textField.rectTransform.anchoredPosition.x, targetPosition - fadeUpOffset);
        LeanTween.moveY(textField.rectTransform, targetPosition, fadeUpDuration).setEaseOutBack();
    }

    private void AnimateLetters(string value)
    {
        StartCoroutine(AnimateLettersCoroutine(value));
    }

    private IEnumerator AnimateLettersCoroutine(string value)
    {
        string text = "";
        foreach (char c in value)
        {
            text += c;
            textField.text = text;
            yield return new WaitForSeconds(letterDuration);
        }
        _isAnimating = false;
    }
}
