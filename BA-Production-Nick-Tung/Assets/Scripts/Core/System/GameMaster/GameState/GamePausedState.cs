using System;
using UnityEngine;

public class GamePausedState : GameState
{
	public override Enum GetEnum()
	{
		return GameStateEnum.GamePaused;
	}

	public override void OnStateEnter()
	{
		master.FreezeGame();
		var inGameUIControl = InGameUIControl.GetInstance();
		if (inGameUIControl)
		{
			inGameUIControl.RequestState(InGameUIState.InGameUIEnum.Menu);
		}
	}

	public override void OnStateExit()
	{
		master.UnFreezeGame();
	}

	public override void UpdateState()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			master.RequestGameState(GameStateEnum.InGame);
		}
	}
}
