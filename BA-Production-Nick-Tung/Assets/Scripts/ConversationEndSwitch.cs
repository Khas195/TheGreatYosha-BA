using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationEndSwitch : MonoBehaviour
{

	public void OnConversationEnd(Transform actor)
	{
		bool playerDied = DialogueLua.GetVariable("WorldVariables.PlayerDeath").asBool;
		if (playerDied)
		{
			DialogueLua.SetVariable("WorldVariables.OneYoshasTooMany_Timeline", 0);
			DialogueLua.SetVariable("WorldVariables.PlayerDeath", false);
			GameMaster.GetInstance().RestartFromLastSave();
		}
		else
		{
			GameMaster.GetInstance().UpdateScenario();
		}
	}
}
