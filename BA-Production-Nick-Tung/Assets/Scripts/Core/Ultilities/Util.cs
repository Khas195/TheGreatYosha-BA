using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Vector2 GetMousPosOnCanvas(Canvas targetCanvas)
    {
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetCanvas.transform as RectTransform, Input.mousePosition, targetCanvas.worldCamera, out uiPos);
        return targetCanvas.transform.TransformPoint(uiPos);
    }
    // Outdated, use Mathf.Lerp instead 
    public static float CalculateAsymptoticAverage(float currentValue, float target, float percentage)
    {
        var result = 0.0f;
        result = (1 - percentage) * currentValue + percentage * target;
        return result;
    }
    public static Vector3 CalculateMoveDirection(float horizontalInput, float forwardInput, Vector3 forwardDirection, Vector3 sideDirection)
    {
        Vector3 movedir;
        var forwardDir = forwardDirection * forwardInput;
        var sideDir = sideDirection * horizontalInput;
        movedir = forwardDir + sideDir;
        return movedir;
    }
    public static bool HasParameter(this Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name.Equals(paramName))
            {
                return true;
            }
        }

        return false;
    }
    public static void SetValueInAnimator(this Animator animator, string paramName, float value)
    {
        if (HasParameter(animator, paramName))
        {
            animator.SetFloat(paramName, value);
        }
    }
    public static void SetValueInAnimator(this Animator animator, string paramName, int value)
    {
        if (HasParameter(animator, paramName))
        {
            animator.SetInteger(paramName, value);
        }
    }
    public static void SetValueInAnimator(this Animator animator, string paramName, bool value)
    {
        if (HasParameter(animator, paramName))
        {
            animator.SetBool(paramName, value);
        }
    }
    public static void SetValueInAnimator(this Animator animator, string paramName)
    {
        if (HasParameter(animator, paramName))
        {
            animator.SetTrigger(paramName);
        }
    }
    public static float GetAnimationCurveTotalTime(this AnimationCurve curve)
    {
        return curve.keys[curve.keys.Length - 1].time;
    }
    public static String TextMod(this String text, Color color, bool bolden = false, bool italic = false)
    {
        var result = text;
        if (bolden)
        {
            result = Bolden(result);
        }
        if (italic)
        {
            result = Italician(result);
        }
        result = Colorize(result, color);
        return result;
    }
    public static String TextMod(this String text, String color, bool bolden = false, bool italic = false)
    {
        var result = text;
        if (bolden)
        {
            result = Bolden(result);
        }
        if (italic)
        {
            result = Italician(result);
        }
        if (color.Equals("") == false)
        {
            result = Colorize(result, color);
        }
        return result;
    }
    public static String Colorize(this String text, Color color)
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
    }
    public static String Colorize(this String text, String color)
    {
        return "<color=" + color + ">" + text + "</color>";
    }
    public static String Bolden(this String text)
    {
        return "<b>" + text + "</b>";
    }
    public static String Italician(this String text)
    {
        return "<i>" + text + "</i>";
    }
    public static List<T> Shuffle<T>(this List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = UnityEngine.Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }

        return _list;
    }
    public static void ForLoop<T>(this List<T> _list, Action<T> loopAction)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            loopAction(_list[i]);
        }
    }
    // not much usage to these functions, just use If else :v
    public static void DoWhenTrue(this bool condition, Action action)
    {
        if (condition)
        {
            action();
        }
    }
    public static void DoWhenFalse(this bool condition, Action action)
    {
        if (condition == false)
        {
            action();
        }
    }
}
