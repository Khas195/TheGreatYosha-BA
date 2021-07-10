using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;

[Serializable]
public class MyLuaVariable
{
	[SerializeField]
	[Dropdown("GetLuaVariables")]
	public string variableName = "";
	public string shortName = "";

	[SerializeField]
	[ReadOnly]
	MyLuaFunctions luaFunctions = null;

	private DropdownList<string> GetLuaVariables()
	{
		var result = new DropdownList<string>();
		result.Add("Empty", "");
		if (luaFunctions == null)
		{
			return result;
		}
		foreach (var variable in luaFunctions.database.variables)
		{
			result.Add(variable.Name, variable.Name);
		}
		return result;
	}

	public void SetDatabase(MyLuaFunctions luaFunctions)
	{
		this.luaFunctions = luaFunctions;
	}
}
public class MyLuaFunctions : MonoBehaviour
{
	[SerializeField]
	List<MyLuaVariable> variables;

	[SerializeField]
	[BoxGroup("DataBase Settings")]
	public DialogueDatabase database = null;
	[Button]
	public void AddVariableFromDatabase()
	{
		foreach (var variable in database.variables)
		{
			if (variables.Find(x => x.variableName == variable.Name) != null)
			{
				continue;
			}
			var newVar = new MyLuaVariable();
			newVar.variableName = variable.Name;
			newVar.SetDatabase(this);
			variables.Add(newVar);
		}
		List<MyLuaVariable> markForRemoval = new List<MyLuaVariable>();
		foreach (var variable in variables)
		{
			if (database.variables.Find(x => x.Name == variable.variableName) == null)
			{
				markForRemoval.Add(variable);
			}
		}
		foreach (var item in markForRemoval)
		{
			variables.Remove(item);
		}
	}
	void OnEnable()
	{
		Lua.RegisterFunction("AddVar", this, SymbolExtensions.GetMethodInfo(() => AddVar("", (double)0)));
		Lua.RegisterFunction("SetVar", this, SymbolExtensions.GetMethodInfo(() => SetVar("", (double)0)));
		Lua.RegisterFunction("GetStr", this, SymbolExtensions.GetMethodInfo(() => GetStr("")));
		Lua.RegisterFunction("GetBool", this, SymbolExtensions.GetMethodInfo(() => GetBool("")));
		Lua.RegisterFunction("GetInt", this, SymbolExtensions.GetMethodInfo(() => GetInt("")));
	}
	public void AddVar(string shortName, double amount)
	{
		shortName = shortName.ToLower();
		foreach (var item in variables)
		{
			if (item.shortName.ToLower().Equals(shortName))
			{
				DialogueLua.SetVariable(item.variableName, DialogueLua.GetVariable(item.variableName).asInt + (int)amount);
			}
		}
	}
	public void SetVar(string shortName, object amount)
	{
		shortName = shortName.ToLower();
		foreach (var item in variables)
		{
			if (item.shortName.ToLower().Equals(shortName))
			{
				DialogueLua.SetVariable(item.variableName, amount);
			}
		}
	}
	public string GetStr(string shortName)
	{
		shortName = shortName.ToLower();
		foreach (var item in variables)
		{
			if (item.shortName.ToLower().Equals(shortName))
			{
				return DialogueLua.GetVariable(item.variableName).asString;
			}
		}
		LogHelper.LogError("Found no variable: " + shortName);
		return "";
	}
	public bool GetBool(string shortName)
	{
		shortName = shortName.ToLower();
		foreach (var item in variables)
		{
			if (item.shortName.ToLower().Equals(shortName))
			{
				return DialogueLua.GetVariable(item.variableName).asBool;
			}
		}
		LogHelper.LogError("Found no variable: " + shortName);
		return false;
	}
	public int GetInt(string shortName)
	{
		shortName = shortName.ToLower();
		foreach (var item in variables)
		{
			if (item.shortName.ToLower().Equals(shortName))
			{
				return DialogueLua.GetVariable(item.variableName).asInt;
			}
		}
		LogHelper.LogError("Found no variable: " + shortName);
		return 0;
	}
}
