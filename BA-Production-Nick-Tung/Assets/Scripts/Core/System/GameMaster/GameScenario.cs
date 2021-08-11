using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Game Scenario", menuName = "Build Settings/Game Scenario", order = 1)]
public class GameScenario : ScriptableObject
{
	[SerializeField]
	string timeLineVariableName;
	[Scene]
	public List<GameInstance> instanceInOrderOfTimeline = new List<GameInstance>();
	public GameInstance GetInstanceBasedOnCurrentTimeline()
	{
		GameInstance resultInstance = null;
		var luaValue = DialogueLua.GetVariable(timeLineVariableName).asInt;
		if (luaValue >= instanceInOrderOfTimeline.Count)
		{
			resultInstance = instanceInOrderOfTimeline[instanceInOrderOfTimeline.Count - 1];
		}
		else
		{
			resultInstance = instanceInOrderOfTimeline[luaValue];
		}
		LogHelper.Log("Current Instance based On Timeline: " + resultInstance + " with timeline value: " + luaValue, true);
		return resultInstance;
	}
}