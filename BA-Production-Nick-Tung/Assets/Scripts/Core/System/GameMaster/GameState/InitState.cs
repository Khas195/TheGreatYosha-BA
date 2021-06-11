using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : State
{
    public override Enum GetEnum()
    {
        return GameState.GameStateEnum.Init;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

}
