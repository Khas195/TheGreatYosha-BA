using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class InGameUIControl : SingletonMonobehavior<InGameUIControl>
{
	[SerializeField]
	[Required]
	StateManager uiStates = null;
	[SerializeField]
	[Required]
	InGameUIState startState = null;



	[SerializeField]
	GameInstance mainMenuInstance = null;
	private void Start()
	{
		uiStates.RequestState(startState.GetEnum());
	}
	public void RequestState(InGameUIState.InGameUIEnum newState)
	{
		uiStates.RequestState(newState);
	}
	public InGameUIState.InGameUIEnum GetCurrentState()
	{
		return (InGameUIState.InGameUIEnum)uiStates.GetCurrentState().GetEnum();
	}
	public void GoToMainMenu()
	{
		var gameMaster = GameMaster.GetInstance(forceCreate: false);
		if (gameMaster)
		{
			gameMaster.RequestInstance(mainMenuInstance);
		}
	}
	public void Quit()
	{
		var gameMaster = GameMaster.GetInstance(forceCreate: false);
		if (gameMaster)
		{
			gameMaster.ExitGame();
		}
	}
}
