using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Data/Movement", order = 1)]
/**
 * The container for all movement related datas in the game
 * Can be created in the Unity Editor.!-- 
 * Right Click in Folder View -> Data -> movement
 */

[XmlInclude(typeof(MovementData))]
[Serializable]
public class MovementData : ScriptableObject
{
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public float jumpForce = 5;
    public float rotateSpeed = 20f;
    [Button("Save")]
    public void SaveData()
    {
        SaveLoadManager.Save<MovementData>(this, this.name);
    }
    [Button("Load")]
    public void LoadData()
    {
        SaveLoadManager.Load<MovementData>(this, this.name);
    }
}
