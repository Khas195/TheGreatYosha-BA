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
        File.WriteAllText(Application.dataPath + "/SavedData/" + fileName + ".json", jsonSaved);
    }
    public static void Load<T>(T objectToLoad, string fileName)
    {
        string jsonLoad = File.ReadAllText(Application.dataPath + "/SavedData/" + fileName + ".json");
        JsonUtility.FromJsonOverwrite(jsonLoad, objectToLoad);
    }
}
