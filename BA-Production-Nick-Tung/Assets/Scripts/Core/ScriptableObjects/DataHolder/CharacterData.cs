using System;
using System.Xml.Serialization;
using NaughtyAttributes;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character", order = 1)]
[Serializable]
public class CharacterData : ScriptableObject
{
	[SerializeField]
	public MovementData moveData = null;

	[Button("Save")]
	public void SaveData()
	{
		SaveLoadManager.Save<CharacterData>(this, this.name);
	}
	[Button("Load")]
	public void LoadData()
	{
		SaveLoadManager.Load<CharacterData>(this, this.name);
	}
}
