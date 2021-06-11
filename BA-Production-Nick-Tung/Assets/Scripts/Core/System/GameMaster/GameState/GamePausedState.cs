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
    }

    public override void OnStateExit()
    {
        master.UnFreezeGame();
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }
}
