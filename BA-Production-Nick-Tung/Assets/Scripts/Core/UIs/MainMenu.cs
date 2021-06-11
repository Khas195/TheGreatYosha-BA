using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Exit()
    {
        LogHelper.Log("Exit Game.", true);
        GameMaster.GetInstance().ExitGame();
    }
    public void StartGame()
    {
        LogHelper.Log("Start Game.", true);
        GameMaster.GetInstance().StartGame();
    }

}
