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
		var luaValue = DialogueLua.GetVariable(timeLineVariableName).asInt;
		luaValue -= 1;
		if (luaValue >= instanceInOrderOfTimeline.Count)
		{
			return instanceInOrderOfTimeline[instanceInOrderOfTimeline.Count - 1];
		}
		else
		{
			return instanceInOrderOfTimeline[luaValue];
		}
	}
}