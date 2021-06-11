using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPack
{
    Dictionary<string, object> datas = new Dictionary<string, object>();

    public bool SetValue(string valueName, object value)
    {
        datas[valueName] = value;
        return true;
    }
    public T GetValue<T>(string valueName)
    {
        if (datas.ContainsKey(valueName) == false)
        {
            LogHelper.LogWarning("Data Value " + valueName + " does not exist in " + this);
            return default(T);
        }
        try
        {
            var convertedValue = (T)datas[valueName];
            return convertedValue;
        }
        catch (InvalidCastException e)
        {
            LogHelper.LogError("Invalid casting exception for getting " + valueName + ", " + e);
            return default(T);
        }
    }
    public bool ContainsValue(string valueName)
    {
        return datas.ContainsKey(valueName);
    }
    public void Reset()
    {
        datas.Clear();
    }
}
