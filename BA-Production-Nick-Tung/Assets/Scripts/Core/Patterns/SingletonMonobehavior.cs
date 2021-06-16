using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonobehavior<T> : MonoBehaviour where T : SingletonMonobehavior<T>
{
	protected static T instance;
	public static T FindInstance()
	{
		T result = null;
		try
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();
			}
			result = (T)instance;
		}
		catch (System.InvalidCastException)
		{
			return null;
		}
		return result;
	}
	public static T GetInstance(bool forceCreate = false)
	{
		T result = FindInstance();
		if (result == null && forceCreate == true)
		{
			// create new instance in scene.
			var newInstance = new GameObject(typeof(T).FullName);
			newInstance.AddComponent<T>();
			result = FindInstance();
		}
		return result;
	}
	protected virtual void Awake()
	{
		if (instance && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = (T)this;
		}

	}
}
