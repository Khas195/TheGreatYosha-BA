using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LocalizeText : MonoBehaviour
{
    [SerializeField]
    [Required]
    Text text;
    [SerializeField]
    [Dropdown("GetIds")]
    [OnValueChanged("OnIdChange")]
    int id;
    [SerializeField]
    [Dropdown("GetTextName")]
    [OnValueChanged("OnTextNameChange")]
    string textName;


    public DropdownList<int> GetIds()
    {
        var result = new DropdownList<int>();
        var inGameText = InGameText.GetInstance();
        if (inGameText)
        {
            var textList = inGameText.GetTextList();
            foreach (var text in textList.texts)
            {
                result.Add(text.id.ToString(), text.id);
            }
        }
        else
        {
            LogHelper.LogWarning("Missing In Game Text instance for Text Localization");
        }
        return result;
    }

    public DropdownList<string> GetTextName()
    {
        var result = new DropdownList<string>();
        var inGameText = InGameText.GetInstance();
        if (inGameText)
        {
            var textList = inGameText.GetTextList();
            foreach (var text in textList.texts)
            {
                result.Add(text.name, text.name);
            }
        }
        else
        {
            LogHelper.LogWarning("Missing In Game Text instance for Text Localization");
        }
        return result;
    }

    public void OnIdChange(int oldId, int newId)
    {
        var inGameText = InGameText.GetInstance();
        if (inGameText)
        {
            var textData = inGameText.GetTextData(newId);
            if (textData != null)
            {
                textName = textData.name;
                // EditorUtility.SetDirty(this.gameObject);
            }
        }
    }
    public void OnTextNameChange(string oldName, string newName)
    {
        var inGameText = InGameText.GetInstance();
        if (inGameText)
        {
            var textData = inGameText.GetTextData(newName);
            if (textData != null)
            {
                id = textData.id;
                // EditorUtility.SetDirty(this.gameObject);
            }
        }
    }

    [Button("Find Text Element")]
    public void FindTextElement()
    {
        var result = this.GetComponent<Text>();
        LogHelper.Log("Find Text element for " + this);
        if (result == null)
        {
            LogHelper.LogWarning("Found no Text Element in " + this);
        }
        else
        {
            text = result;
        }
    }
    [Button("Load Text")]
    public void LoadText()
    {
        LogHelper.Log("Loading new Text for " + this);
        this.text.text = InGameText.GetInstance().GetTextData(id).content;
        //EditorUtility.SetDirty(this.gameObject);
    }
}
