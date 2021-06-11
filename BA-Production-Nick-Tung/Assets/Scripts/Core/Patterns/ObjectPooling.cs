using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public abstract class ObjectPooling<T>
{

    [SerializeField]
    [ReadOnly]
    protected Queue<T> pool = new Queue<T>();
    public T RequestInstance()
    {
        LogHelper.Log(this + " got requested an instance of " + typeof(T).FullName + " in pool");
        T result = default(T);

        if (pool.Count <= 0)
        {
            LogHelper.Log(this + " no more " + typeof(T).FullName + " in pool, Creating a new one");
            result = CreateInstance();
        }
        else
        {
            LogHelper.Log(this + " found extra in pool, dequeueing it");
            result = pool.Dequeue();
        }
        LogHelper.Log(this + " activates " + result);
        ActivateOjbect(result);
        LogHelper.Log(this + " return requested instance of " + typeof(T).FullName);
        return result;
    }
    public void ReturnInstance(T returnee)
    {
        LogHelper.Log(this + " recieved an instance of " + typeof(T).FullName + " back");
        if (returnee == null)
        {
            LogHelper.LogWarning(this + " recieved a null in return instance");
        }
        LogHelper.Log(this + " deactivates " + returnee);
        DeactivateObject(returnee);
        this.pool.Enqueue(returnee);
        LogHelper.Log(this + " enqueues/add " + returnee + " to pool");
    }
    protected abstract T CreateInstance();
    protected abstract void ActivateOjbect(T target);

    protected abstract void DeactivateObject(T target);
}
