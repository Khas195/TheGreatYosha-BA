using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public GameObject continueButton = null;
	private void Start()
	{
		if (GameMaster.GetInstance().IsSaveExist())
		{
			continueButton.SetActive(true);
		}
		else
		{
			continueButton.SetActive(false);
		}
	}
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
	public void Continue()
	{
		LogHelper.Log("Main Menu - Continue.", true);
		GameMaster.GetInstance().LoadSave();

	}

}
