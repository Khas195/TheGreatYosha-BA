using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public static class SaveLoadManager
{
	public static void SaveAllData()
	{
		// foreach (var obj in Resources.FindObjectsOfTypeAll(typeof(ScriptableObject)) as ScriptableObject[])
		// {
		//     if (EditorUtility.IsPersistent(obj))
		//     {
		//         string pathToAsset = UnityEditor.AssetDatabase.GetAssetPath(obj);
		//         if (pathToAsset.StartsWith("Assets/Resources/Datas"))
		//         {
		//             SaveLoadManager.Save<object>(obj, obj.name);
		//         }
		//     }
		// }
	}
	public static void LoadAllData()
	{
		// foreach (var obj in Resources.FindObjectsOfTypeAll(typeof(ScriptableObject)) as ScriptableObject[])
		// {
		//     if (EditorUtility.IsPersistent(obj))
		//     {
		//         string pathToAsset = UnityEditor.AssetDatabase.GetAssetPath(obj);
		//         if (pathToAsset.StartsWith("Assets/Resources/Datas"))
		//         {
		//             SaveLoadManager.Load<object>(obj, obj.name);
		//         }
		//     }
		// }
	}

	public static void Save<T>(T savedObject, string fileName)
	{
		string jsonSaved = JsonUtility.ToJson(savedObject);
		File.WriteAllText(Application.streamingAssetsPath + "/SavedData/" + fileName + ".json", jsonSaved);
	}
	public static void Load<T>(T objectToLoad, string fileName)
	{
		string jsonLoad = File.ReadAllText(Application.streamingAssetsPath + "/SavedData/" + fileName + ".json");
		JsonUtility.FromJsonOverwrite(jsonLoad, objectToLoad);
	}
	public static T Load<T>(string fileName)
	{
		try
		{
			string jsonLoad = File.ReadAllText(Application.streamingAssetsPath + "/SavedData/" + fileName + ".json");
			T result = JsonUtility.FromJson<T>(jsonLoad);
			return result;
		}
		catch (FileNotFoundException e)
		{
			return default(T);
		}
	}
}
