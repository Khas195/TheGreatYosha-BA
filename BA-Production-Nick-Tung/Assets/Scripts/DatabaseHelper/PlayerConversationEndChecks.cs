using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConversationEndChecks : MonoBehaviour
{
	[SerializeField]
	float updateScenarioAfter = 2.0f;
	[SerializeField]
	PlayerController2D controller = null;
	[SerializeField]
	string deathVarName = "";

	public void OnConversationEnd(Transform actor)
	{
		if (PixelCrushers.DialogueSystem.DialogueLua.GetVariable(deathVarName).asBool == true)
		{
			controller.LockControl();
			InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.DeathMenu);
		}
		else if (GameMaster.GetInstance().IsScenarioUpdateToDate() == false)
		{
			controller.LockControl();
			InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.TransitionState);
			Invoke("TriggerScenarioUpdate", updateScenarioAfter);
		}

	}
	public void TriggerScenarioUpdate()
	{
		GameMaster.GetInstance().UpdateScenario();
	}
}
