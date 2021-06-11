using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class TextData
{
    [ReadOnly]
    public int id;
    public string name;
    [ResizableTextArea]
    public string content;
}

[CreateAssetMenu(fileName = "InGameText", menuName = "Data/Localization", order = 1)]
public class InGameTextData : ScriptableObject
{
    public string lang = "En";
    [ReorderableList]
    [ValidateInput("NoSimilarName", "All names must be different")]
    public List<TextData> texts = new List<TextData>();

    [Button("Sort IDs")]
    public void SortID()
    {
        SortID(texts);
    }
    public void SortID(List<TextData> mList)
    {
        for (int i = 0; i < mList.Count; i++)
        {
            mList[i].id = i;
        }
    }
    public bool NoSimilarName(List<TextData> mList)
    {

        SortID(mList);
        for (int i = 0; i < mList.Count; i++)
        {
            for (int j = i + 1; j < mList.Count; j++)
            {
                if (mList[i].name.Equals(mList[j].name))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
