using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleState : GameState
{
    GameState lastGameState = null;
    public override Enum GetEnum()
    {
        return GameStateEnum.Console;
    }

    public override void OnStateEnter()
    {
        PostOffice.SendData(null, GameMasterEvent.InGameLogConsoleEvent.CONSOLE_SWITCH_ON_OFF);
        lastGameState = GameMaster.GetInstance().GetCurrentState();
    }

    public override void OnStateExit()
    {
        PostOffice.SendData(null, GameMasterEvent.InGameLogConsoleEvent.CONSOLE_SWITCH_ON_OFF);
        lastGameState = null;
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            this.master.RequestGameState((GameStateEnum)lastGameState.GetEnum());
        }
    }
}
