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
    }

    public override void OnStateExit()
    {
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }
}
