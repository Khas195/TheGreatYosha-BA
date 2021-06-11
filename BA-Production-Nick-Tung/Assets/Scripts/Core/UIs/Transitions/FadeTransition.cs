using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    [SerializeField]
    float fadeTime = 3.0f;
    [SerializeField]
    Text textUI;

    [SerializeField]
    [ReadOnly]
    float currentTime = 0.0f;
    [SerializeField]
    [ReadOnly]
    float targetAlpha = 0.0f;
    [SerializeField]
    [ReadOnly]
    float startAlpha = 1.0f;

    [SerializeField]
    [ReadOnly]
    bool isTransitioning = false;

    void Update()
    {
        if (isTransitioning)
        {
            var color = textUI.color;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, currentTime / fadeTime);
            textUI.color = color;
            if (currentTime >= fadeTime)
            {
                isTransitioning = false;
            }
            currentTime += Time.deltaTime;
        }
    }
    [Button]
    public void FadeIn()
    {
        startAlpha = 0.0f;
        targetAlpha = 1.0f;
        currentTime = 0;
        isTransitioning = true;
    }
    [Button]
    public void FadeOut()
    {
        startAlpha = 1.0f;
        targetAlpha = 0.0f;
        currentTime = 0;
        isTransitioning = true;
    }
}
