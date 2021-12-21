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
		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.InConversation);
	}
	public void OnConversationEnd(Transform actor)
	{
		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.InGameOverlay);
		bool playerDied = DialogueLua.GetVariable("WorldVariables.Story_PlayerDeath").asBool;
		if (playerDied)
		{
			GameMaster.GetInstance().UpdateScenario();
		}
	}

}
