using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;


public class ScenarioUpdator : MonoBehaviour
{
	[SerializeField]
	float updateScenarioAfter = 2.0f;
	[SerializeField]
	PlayerController2D controller = null;
	public void OnConversationEnd(Transform actor)
	{
		if (GameMaster.GetInstance().IsScenarioUpdateToDate() == false)
		{
			controller.LockControl();
			Invoke("TriggerScenarioUpdate", updateScenarioAfter);
		}
	}
	public void TriggerScenarioUpdate()
	{
		GameMaster.GetInstance().UpdateSceario();
	}
}
