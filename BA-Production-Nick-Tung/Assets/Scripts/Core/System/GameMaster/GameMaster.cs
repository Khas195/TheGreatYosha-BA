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
	[BoxGroup("Settings")]
	FadeManyTransition fadeTransitions = null;


	[SerializeField]
	[Required]
	[Expandable]
	[BoxGroup("Settings")]
	GameInstance mainMenuInstance = null;

	[SerializeField]
	[Required]
	[Expandable]
	[BoxGroup("Settings")]
	GameInstance creditsInstance = null;
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
		PixelCrushers.DialogueSystem.DialogueManager.ResetDatabase();
		this.RequestInstance(startScenario.GetInstanceBasedOnCurrentTimeline());
		this.SaveGame();
	}

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	protected override void Awake()
	{
		base.Awake();
		PostOffice.Subscribes(this, GameMasterEvent.ON_GAMESTATE_CHANGED);
		PostOffice.Subscribes(this, GameMasterEvent.ON_INSTANCE_LOADED);
	}

	public bool RequestGameState(GameState.GameStateEnum requestState)
	{
		return this.gameStates.RequestState(requestState);
	}

	void Start()
	{
		loadingManager.UnloadAllScenes(exception: requiredScenes.masterScene);
		loadingManager.LoadSceneAdditively(requiredScenes.logScene);
		loadingManager.LoadSceneAdditively(requiredScenes.system);
		RequestInstance(mainMenuInstance);
	}
	void OnDestroy()
	{
		PostOffice.Unsubscribes(this, GameMasterEvent.ON_GAMESTATE_CHANGED);
		PostOffice.Unsubscribes(this, GameMasterEvent.ON_INSTANCE_LOADED);
	}


	public void RequestInstance(GameInstance newInstance, bool loadAndWait = true)
	{
		if (this.gameStates.RequestState(newInstance.desiredGameState))
		{
			fadeTransitions.FadeIn(() =>
			{
				PixelCrushers.DialogueSystem.DialogueManager.StopConversation();
				PixelCrushers.DialogueSystem.ConversationPositionStack.ClearConversationPositionStack();
				PostOffice.SendData(null, GameMasterEvent.ON_LOAD_NEW_STANCE_START);
				loadingManager.InitiateLoadingSequenceFor(newInstance, loadAndWait);
				currentInstance = newInstance;
			});

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
	public FadeManyTransition GetGameMasterFade()
	{
		return fadeTransitions;
	}

	void Update()
	{
		//ForceAspectRatio(16, 9);
		var currentState = gameStates.GetCurrentState<GameState>();
		if (currentState != null)
		{
			currentState.UpdateState();
			if ((GameState.GameStateEnum)currentState.GetEnum() != GameState.GameStateEnum.Console)
			{
				ProcessConsoleTrigger();
			}
		}
		// test save
		if (Input.GetKeyDown(KeyCode.F4))
		{
			SaveGame();
		}
		if (Input.GetKeyDown(KeyCode.F5))
		{
			LoadSave();
		}
		if (Input.GetKeyDown(KeyCode.F6))
		{
			this.RestartFromLastSave();
		}
	}

	private void ForceAspectRatio(int width, int height)
	{
		float targetaspect = width / height;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = Camera.main;

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}

	public void LoadSave()
	{
		var junraSaveData = new JunraSaveData();
		SaveLoadManager.Load<JunraSaveData>(junraSaveData, "JunraGameSave");
		PixelCrushers.DialogueSystem.PersistentDataManager.ApplySaveData(junraSaveData.databaseData);
		this.currentScenario = junraSaveData.currentScenario;
		UpdateScenario();
	}

	public void SaveGame()
	{
		var junraSaveData = new JunraSaveData();
		junraSaveData.currentScenario = this.currentScenario;
		var saveData = PixelCrushers.DialogueSystem.PersistentDataManager.GetSaveData();
		LogHelper.Log(saveData);
		junraSaveData.databaseData = saveData;
		SaveLoadManager.Save<JunraSaveData>(junraSaveData, "JunraGameSave");
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
	public void UpdateScenario()
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
			SaveGame();

		}
		else
		{
			fadeTransitions.FadeIn(() =>
			{
				PostOffice.SendData(null, GameMasterEvent.ON_LOAD_NEW_STANCE_START);
				loadingManager.ReloadInstance(targetInstance, true);
				currentInstance = targetInstance;
			});
			SaveGame();
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
	public bool IsSaveExist()
	{
		var junraSave = new JunraSaveData();
		junraSave = SaveLoadManager.Load<JunraSaveData>("JunraGameSave");
		if (junraSave != null && junraSave.IsValid())
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public void RestartFromLastSave()
	{
		LoadSave();
		if (this.currentScenario == null)
		{
			LogHelper.LogError("Trying to load a null scenario");
			return;
		}
		var targetInstance = this.currentScenario.GetInstanceBasedOnCurrentTimeline();
		PostOffice.SendData(null, GameMasterEvent.ON_LOAD_NEW_STANCE_START);
		loadingManager.ReloadInstance(targetInstance, true);
		currentInstance = targetInstance;
		SaveGame();
	}
	public void LoadCredits()
	{
		this.RequestInstance(creditsInstance);
	}

	public void LoadMainMenu()
	{
		this.RequestInstance(mainMenuInstance);
	}
}

