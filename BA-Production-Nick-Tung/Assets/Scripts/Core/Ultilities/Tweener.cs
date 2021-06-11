using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Use unity Lerps instead
 */
public class Tweener 
{
    public enum TweenType
    {
        LinearTween, 
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad
    }
    public static float Tween(TweenType type, float curTime, float beginingValue, float changeInValue, float duration)
    {
        float result;
        switch (type)
        {
            case TweenType.LinearTween:
                result = LinearTween(curTime, beginingValue, changeInValue, duration);
                break;
            case TweenType.EaseInQuad:
                 result = EaseInQuad(curTime, beginingValue, changeInValue, duration);
                break;
            case TweenType.EaseOutQuad:
                 result = EaseOutQuad(curTime, beginingValue, changeInValue, duration);
                break;
            case TweenType.EaseInOutQuad:
                result =  EaseInOutQuad(curTime, beginingValue, changeInValue, duration);
                break;
            default:
                 result = LinearTween(curTime, beginingValue, changeInValue, duration);
                break;
        }
        return result;
    }
    public static float LinearTween(float curTime, float beginingValue, float changeInValue, float duration)
    {
        return changeInValue * curTime / duration + beginingValue;
    }

    public static Color TweenColor(TweenType type, float curTime, Color originColor, Color changeInValue, float duration)
    {
        Color result = originColor;
        result.b = Tween(type, curTime, originColor.b, changeInValue.b, duration);
        result.g = Tween(type, curTime, originColor.g, changeInValue.g, duration);
        result.r = Tween(type, curTime, originColor.r, changeInValue.r, duration);
        Debug.Log("Color: " + result);
        return result;
    }

    public static float EaseInQuad(float curTime, float beginingValue, float changeInValue, float duration)
    {
        return changeInValue * (curTime /= duration) * curTime + beginingValue;
    } 
   public static float EaseOutQuad(float curTime, float beginingValue, float changeInValue, float duration)
    {

        return -changeInValue * (curTime /= duration) * (curTime - 2) + beginingValue;
    } 
   public static float EaseInOutQuad(float curTime, float beginingValue, float changeInValue, float duration)
    {
        if ((curTime /= duration / 2) < 1) return changeInValue / 2 * curTime * curTime + beginingValue;
        return -changeInValue / 2 * ((--curTime) * (curTime - 2) - 1) + beginingValue;
    }

}