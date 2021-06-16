using System;
using UnityEngine;

public class InGame : GameState
{
	public override Enum GetEnum()
	{
		return GameStateEnum.InGame;
	}

	public override void OnStateEnter()
	{
		var inGameUIControl = InGameUIControl.GetInstance(forceCreate: false);
		if (inGameUIControl)
		{
			inGameUIControl.RequestState(InGameUIState.InGameUIEnum.InGameOverlay);
		}
	}

	public override void OnStateExit()
	{
	}

	public override void UpdateState()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			master.RequestGameState(GameStateEnum.GamePaused);
		}
	}
}
