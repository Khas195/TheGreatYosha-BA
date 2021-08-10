using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMaster : SingletonMonobehavior<GameMaster>, IObserver
{
	[SerializeField]
	[Required]
	[Expandable]
	[BoxGroup("Settings")]
	BuildProfile requiredScenes = null;



	[SerializeField]
	[Required]
	[Expandable]
	[BoxGroup("Settings")]
	GameScenario startScenario = null;



	[SerializeField]
	[Required]
	[Expandable]
	[BoxGroup("Settings")]
	GameInstance mainMenuInstance = null;
	[SerializeField]
	[ReadOnly]
	[Expandable]
	[BoxGroup("Settings")]
	GameInstance currentInstance = null;
	[SerializeField]
	[BoxGroup("Settings")]
	GameScenario currentScenario = null;




	[SerializeField]
	[Required]
	[BoxGroup("Required Components")]
	SceneLoadingManager loadingManager = null;

	[SerializeField]
	[Required]
	[BoxGroup("Required Components")]
	StateManager gameStates = null;


	public void StartGame()
	{
		currentScenario = startScenario;
		this.RequestInstance(startScenario.GetInstanceBasedOnCurrentTimeline());
	}

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	protected override void Awake()
	{
		base.Awake();
		PostOffice.Subscribes(this, GameMasterEvent.ON_GAMESTATE_CHANGED);
	}

	public bool RequestGameState(GameState.GameStateEnum requestState)
	{
		return this.gameStates.RequestState(requestState);
	}

	void Start()
	{
		loadingManager.UnloadAllScenes(exception: requiredScenes.masterScene);
		loadingManager.LoadSceneAdditively(requiredScenes.logScene);
		RequestInstance(mainMenuInstance);
	}
	void OnDestroy()
	{
		PostOffice.Unsubscribes(this, GameMasterEvent.ON_GAMESTATE_CHANGED);
	}


	public void RequestInstance(GameInstance newInstance, bool loadAndWait = true)
	{
		if (this.gameStates.RequestState(newInstance.desiredGameState))
		{
			PostOffice.SendData(null, GameMasterEvent.ON_LOAD_NEW_STANCE);
			loadingManager.InitiateLoadingSequenceFor(newInstance, loadAndWait);
			currentInstance = newInstance;
		}
		else
		{
			LogHelper.LogError("Cannot Transition to the desired game state:" + newInstance.desiredGameState + "of this game instance: " + newInstance);
		}
	}


	private void NotifyOnGameStateChange(GameState.GameStateEnum newGameState)
	{
		var data = DataPool.GetInstance().RequestInstance();
		data.SetValue(GameMasterEvent.GameStateChangeEvent.New_Game_State, newGameState);
		PostOffice.SendData(data, GameMasterEvent.ON_GAMESTATE_CHANGED);
		DataPool.GetInstance().ReturnInstance(data);
	}

	public GameInstance GetCurrentGameInstance()
	{
		return currentInstance;
	}

	void Update()
	{
		var currentState = gameStates.GetCurrentState<GameState>();
		if (currentState != null)
		{
			currentState.UpdateState();
			if ((GameState.GameStateEnum)currentState.GetEnum() != GameState.GameStateEnum.Console)
			{
				ProcessConsoleTrigger();
			}
		}
	}
	public void ProcessConsoleTrigger()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			this.gameStates.RequestState(GameState.GameStateEnum.Console);
		}
	}



	public void SetMouseVisibility(bool visibility)
	{
		Cursor.visible = visibility;
		Cursor.lockState = visibility ? CursorLockMode.None : CursorLockMode.Confined;
	}

	public void ExitGame()
	{
		// save any game data here
#if UNITY_EDITOR
		// Application.Quit() does not work in the editor so
		// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
	public void SetGameTimeScale(float newTimeScale)
	{
		Time.timeScale = newTimeScale;
	}

	public void ReceiveData(DataPack pack, string eventName)
	{
	}
	public void FreezeGame()
	{
		Time.timeScale = 0.0f;
	}
	public void UnFreezeGame()
	{
		Time.timeScale = 1.0f;
	}
	public GameState GetCurrentState()
	{
		return gameStates.GetCurrentState<GameState>();
	}
	public void UpdateSceario()
	{
		if (this.currentScenario == null)
		{
			LogHelper.LogError("Trying to Update a null scenario");
			return;
		}
		var targetInstance = this.currentScenario.GetInstanceBasedOnCurrentTimeline();
		if (targetInstance != currentInstance)
		{
			this.RequestInstance(targetInstance);
		}

	}
	public GameScenario GetCurrentScenario()
	{
		return currentScenario;
	}
	public bool IsScenarioUpdateToDate()
	{
		var targetInstance = this.currentScenario.GetInstanceBasedOnCurrentTimeline();
		if (targetInstance != currentInstance)
		{
			return false;
		}
		return true;
	}
}

