using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public void Exit()
	{
		LogHelper.Log("Main Menu - Exit Game.", true);
		GameMaster.GetInstance().ExitGame();
	}
	public void StartGame()
	{
		LogHelper.Log("Main Menu - Start Game.", true);
		GameMaster.GetInstance().StartGame();
	}

}
