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
		var inGameUIControl = InGameUIControl.GetInstance();
		if (inGameUIControl)
		{
			inGameUIControl.RequestState(InGameUIState.InGameUIEnum.Menu);
			inGameUIControl.MoveToOverlay();
			var data = DataPool.GetInstance().RequestInstance();
			data.SetValue("NewUiState", InGameUIState.InGameUIEnum.Menu);
			PostOffice.SendData(data, "UIStateChanged");
			DataPool.GetInstance().ReturnInstance(data);
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
