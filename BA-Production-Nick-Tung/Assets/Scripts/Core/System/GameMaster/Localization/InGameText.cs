using System;
using System.Collections;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;


public class InGameText : SingletonMonobehavior<InGameText>
{
    [SerializeField]
    [Required]
    InGameTextData data = null;
    [SerializeField]
    [Dropdown("GetLanguage")]
    string language = "EN";
    [Button("Load Language")]
    public void LoadTextData()
    {
        // foreach (var obj in Resources.FindObjectsOfTypeAll(typeof(ScriptableObject)) as ScriptableObject[])
        // {
        //     if (EditorUtility.IsPersistent(obj))
        //     {
        //         string pathToAsset = UnityEditor.AssetDatabase.GetAssetPath(obj);
        //         if (pathToAsset.StartsWith("Assets/Resources/Datas/Localization"))
        //         {
        //             if (obj.name.Equals(language))
        //             {
        //                 data = obj as InGameTextData;
        //             }
        //         }
        //     }
        // }
    }
    [Button("Load Text for all Localization Text")]
    public void LoadAllLocalizationText()
    {
        LogHelper.Log("Reloading Text For all Localization");
        foreach (var obj in Resources.FindObjectsOfTypeAll(typeof(LocalizeText)) as LocalizeText[])
        {
            LogHelper.Log("Reloading Text For " + obj);
            obj.LoadText();
        }
    }
    public DropdownList<string> GetLanguage()
    {
        var result = new DropdownList<string>();
        // foreach (var obj in Resources.FindObjectsOfTypeAll(typeof(ScriptableObject)) as ScriptableObject[])
        // {
        //     if (EditorUtility.IsPersistent(obj))
        //     {
        //         string pathToAsset = UnityEditor.AssetDatabase.GetAssetPath(obj);
        //         if (pathToAsset.StartsWith("Assets/Resources/Datas/Localization"))
        //         {
        //             result.Add(obj.name, obj.name);
        //         }
        //     }
        // }
        return result;
    }

    public InGameTextData GetTextList()
    {
        return data;
    }
    public TextData GetTextData(string name)
    {
        var result = data.texts.Find(x => x.name.Equals(name));
        if (result == null)
        {
            LogHelper.LogWarning("Can't find Text Data with name: " + name);
            return null;
        }
        return result;
    }
    public TextData GetTextData(int id)
    {
        var result = data.texts.Find(x => x.id.Equals(id));
        if (result == null)
        {
            LogHelper.LogWarning("Can't find Text Data with id: " + id);
            return null;
        }
        return result;

    }
}
