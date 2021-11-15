using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationEndSwitch : MonoBehaviour
{
	public void OnConversationStart(Transform actor)
	{

		Debug.Log("CONVERSATION Start");
		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.InConversation);
	}
	public void OnConversationEnd(Transform actor)
	{
		Debug.Log("CONVERSATION ENDED");
		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.InGameOverlay);
		return;
		bool playerDied = DialogueLua.GetVariable("WorldVariables.PlayerDeath").asBool;
		bool reset = DialogueLua.GetVariable("WorldVariables.Reset").asBool;
		if (reset == true)
		{
			DialogueLua.SetVariable("WorldVariables.OneYoshasTooMany_Timeline", 0);
			GameMaster.GetInstance().UpdateScenario();
			DialogueLua.SetVariable("WorldVariables.Reset", false);
			return;
		}
		if (playerDied)
		{
			DialogueLua.SetVariable("WorldVariables.OneYoshasTooMany_Timeline", 0);
			DialogueLua.SetVariable("WorldVariables.PlayerDeath", false);
			GameMaster.GetInstance().UpdateScenario();
		}
		else
		{
			GameMaster.GetInstance().UpdateScenario();
		}
	}
}
