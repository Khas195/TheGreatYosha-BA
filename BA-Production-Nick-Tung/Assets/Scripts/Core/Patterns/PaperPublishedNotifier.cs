using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class PaperPublishedNotifier : MonoBehaviour
{
    [SerializeField]
    List<Text> notifyTexts;
    [SerializeField]
    AnimationCurve notifyCurve;
    [SerializeField]
    float notifyDuration = 3;
    [ShowNonSerializedField]
    float curTime = 100;


    public void Notify()
    {
        curTime = 0;
    }
    private void Update()
    {
        if (curTime <= notifyDuration)
        {
            var alphaValue = notifyCurve.Evaluate(curTime);
            for (int i = 0; i < notifyTexts.Count; i++)
            {
                var color = notifyTexts[i].color;
                color.a = alphaValue;
                notifyTexts[i].color = color;
            }
            curTime += Time.deltaTime;

            if (curTime > notifyDuration)
            {
                ResetAllNotifiersToTransparent();
            }
        }
    }

    private void ResetAllNotifiersToTransparent()
    {
        for (int i = 0; i < notifyTexts.Count; i++)
        {
            var color = notifyTexts[i].color;
            color.a = 0.0f;
            notifyTexts[i].color = color;
        }
    }
}
