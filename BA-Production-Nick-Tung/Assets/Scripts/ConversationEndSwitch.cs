using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationEndSwitch : MonoBehaviour
{
	[SerializeField]
	GameObject continueButton = null;
	public void OnConversationStart(Transform actor)
	{
		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.InConversation);
	}
	public void OnConversationEnd(Transform actor)
	{
		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.InGameOverlay);
		continueButton.SetActive(true);
	}

	public void AdvanceNextScene()
	{
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
