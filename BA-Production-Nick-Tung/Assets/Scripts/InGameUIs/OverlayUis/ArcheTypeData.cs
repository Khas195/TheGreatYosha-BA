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

	[SerializeField]
	[BoxGroup("DataBase Settings")]
	DialogueDatabase database = null;
	private DropdownList<string> GetArchtypeMaxLuaVariables()
	{
		var result = new DropdownList<string>();
		result.Add("Empty", "");
		if (database != null)
		{
			foreach (var variable in database.variables)
			{
				if (variable.Name.ToLower().Contains(GameKeyWord.ARCHETYPE_MAX_LOOKUP))
				{
					result.Add(variable.Name, variable.Name);
				}
			}
		}
		return result;
	}
	private DropdownList<string> GetArchtypeLuaVariables()
	{
		var result = new DropdownList<string>();
		result.Add("Empty", "");
		if (database != null)
		{
			foreach (var variable in database.variables)
			{
				if (variable.Name.ToLower().Contains(GameKeyWord.ARCHETYPE_LOOKUP))
				{
					result.Add(variable.Name, variable.Name);
				}
			}
		}
		return result;
	}
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
