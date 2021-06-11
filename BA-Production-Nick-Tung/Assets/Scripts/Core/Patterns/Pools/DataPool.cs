using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPool : ObjectPooling<DataPack>
{
    static DataPool instance = null;
    public static DataPool GetInstance()
    {
        if (instance == null)
        {
            instance = new DataPool();
        }
        return instance;
    }
    protected override void ActivateOjbect(DataPack target)
    {
    }

    protected override DataPack CreateInstance()
    {
        return new DataPack();
    }

    protected override void DeactivateObject(DataPack target)
    {
        target.Reset();
    }
}
