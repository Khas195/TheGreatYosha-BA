using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ArcheTypeData", menuName = "Data/ArcheTypeData", order = 0)]
public class ArcheTypeData : ScriptableObject
{
	[SerializeField]
	[Dropdown("GetArchtypeLuaVariables")]
	string archetypeLuaVariable = null;
	[SerializeField]
	[Dropdown("GetArchtypeMaxLuaVariables")]
	string archetypeMaxLuaVariable = null;
	public int GetValue()
	{
		return DialogueLua.GetVariable(archetypeLuaVariable).asInt;
	}
	public void SetValue(int newValue)
	{
		DialogueLua.SetVariable(archetypeLuaVariable, newValue);
	}
	public int GetMaxValue()
	{
		return DialogueLua.GetVariable(archetypeMaxLuaVariable).asInt;
	}
}
