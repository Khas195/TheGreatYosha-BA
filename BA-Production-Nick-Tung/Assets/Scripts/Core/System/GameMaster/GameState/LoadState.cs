using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoadState : GameState
{
	public override Enum GetEnum()
	{
		return GameStateEnum.LoadState;
	}

	public override void OnStateEnter()
	{
		SceneLoadingManager.GetInstance().LoadLoadingScene();
	}

	public override void OnStateExit()
	{
		SceneLoadingManager.GetInstance().FinishedLoadingProtocol();
	}

	public override void UpdateState()
	{
	}
}
