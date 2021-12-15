using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingManager : SingletonMonobehavior<SceneLoadingManager>, IObserver
{
	[SerializeField]
	[Expandable]
	[Required]
	BuildProfile profile = null;
	List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
	List<AsyncOperation> scenesUnloading = new List<AsyncOperation>();
	GameInstance currentInstance = null;
	GameInstance instanceToLoad = null;
	[SerializeField]
	[ReadOnly]
	bool loadAndWait = true;
	bool reload = false;
	bool waitForUnload;

	void Start()
	{
		PostOffice.Subscribes(this, GameMasterEvent.ON_INSTANCE_LOADED);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name.Equals(profile.loadScene))
		{
			GameMaster.GetInstance().GetGameMasterFade().FadeOut();
			if (currentInstance != null)
			{
				UnloadInstance(currentInstance, removeDubplicate: reload);
				reload = false;
			}
			waitForUnload = true;
		}

	}
	private void Update()
	{
		if (waitForUnload)
		{
			if (this.GetUnloadingProcess() >= 1.0f)
			{
				LoadInstance(instanceToLoad, loadDuplicate: false);
				waitForUnload = false;
			}
		}
	}
	public void FinishedLoading()
	{
		if (loadAndWait)
		{
			for (int i = 0; i < scenesLoading.Count; i++)
			{
				LogHelper.Log("Activate scene: " + scenesLoading[i]);
				scenesLoading[i].allowSceneActivation = true;
			}
		}
		GameMaster.GetInstance().RequestGameState(this.instanceToLoad.desiredGameState);
	}
	public void FinishedLoadingProtocol()
	{
		LogHelper.Log("Finished Loading Protocol");
		GameMaster.GetInstance().GetGameMasterFade().FadeIn(() =>
		{
			SceneManager.UnloadSceneAsync(profile.loadScene);
			var data = DataPool.GetInstance().RequestInstance();
			data.SetValue("Instance", currentInstance);
			PostOffice.SendData(data, GameMasterEvent.ON_INSTANCE_LOADED);
			DataPool.GetInstance().ReturnInstance(data);
			this.scenesLoading.Clear();
			GameMaster.GetInstance().GetGameMasterFade().FadeOut();
		});
	}

	public void InitiateLoadingSequenceFor(GameInstance newInstance, bool loadAndWait)
	{
		instanceToLoad = newInstance;
		GameMaster.GetInstance().RequestGameState(GameState.GameStateEnum.LoadState);
		this.loadAndWait = loadAndWait;
	}

	public void LoadLoadingScene()
	{
		this.LoadSceneAdditively(profile.loadScene);
	}

	public float GetLoadingProgress()
	{
		var totalProgress = 0.0f;
		for (int i = 0; i < this.scenesLoading.Count; i++)
		{
			if (scenesLoading[i].allowSceneActivation == false)
			{
				totalProgress += scenesLoading[i].progress + 0.1f;
			}
			else
			{
				totalProgress += scenesLoading[i].progress;
			}
		}
		return totalProgress / this.scenesLoading.Count;
	}
	public float GetUnloadingProcess()
	{
		var totalProgress = 0.0f;
		if (this.scenesUnloading.Count <= 0)
		{
			return 1.0f;
		}
		for (int i = 0; i < this.scenesUnloading.Count; i++)
		{
			if (scenesUnloading[i].allowSceneActivation == false)
			{
				totalProgress += scenesUnloading[i].progress + 0.1f;
			}
			else
			{
				totalProgress += scenesUnloading[i].progress;
			}
		}
		return totalProgress / this.scenesUnloading.Count;
	}
	public void LoadInstance(GameInstance requestedInstance, bool loadDuplicate = true)
	{
		for (int i = 0; i < requestedInstance.sceneList.Count; i++)
		{
			if (loadDuplicate == false)
			{
				if (IsSceneLoaded(requestedInstance.sceneList[i]) == false)
				{
					LoadSceneAdditively(requestedInstance.sceneList[i], allowAutoActivation: loadAndWait == false);
				}
			}
			else
			{
				LoadSceneAdditively(requestedInstance.sceneList[i], allowAutoActivation: loadAndWait == false);
			}
		}
		currentInstance = requestedInstance;

	}

	private bool IsSceneLoaded(string sceneName)
	{
		return SceneManager.GetSceneByName(sceneName).IsValid();
	}

	public void UnloadInstance(GameInstance instance, bool removeDubplicate = true)
	{
		for (int i = 0; i < instance.sceneList.Count; i++)
		{
			if (removeDubplicate == false)
			{
				if (instanceToLoad.sceneList.Contains(instance.sceneList[i]) == false)
				{
					this.scenesUnloading.Add(SceneManager.UnloadSceneAsync(instance.sceneList[i]));
				}
			}
			else
			{
				this.scenesUnloading.Add(SceneManager.UnloadSceneAsync(instance.sceneList[i]));
			}
		}
	}

	public void LoadSceneAdditively(string sceneName, bool allowAutoActivation = true)
	{
		LogHelper.Log("Scene Loading Manager: Loading Additively " + sceneName.Bolden() + "", true);
		var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		LogHelper.Log(" Deactivate scene on loaded: " + sceneName.Bolden() + "", true);
		operation.allowSceneActivation = allowAutoActivation;
		this.scenesLoading.Add(operation);
	}
	public void UnloadAllScenes(string exception = "")
	{
		int numOfScene = SceneManager.sceneCount;
		LogHelper.Log("Loading Manager".Bolden().Colorize(Color.green) + " counts " + numOfScene + " at start", true);
		for (int i = 0; i < numOfScene; i++)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name != exception)
			{
				LogHelper.Log(exception.Bolden().Colorize(Color.green) + " unloading " + scene.name, true);
				SceneManager.UnloadSceneAsync(scene.name);
			}
		}
	}

	public void ReceiveData(DataPack pack, string eventName)
	{
	}

	public void ReloadInstance(GameInstance targetInstance, bool loadAndWait)
	{
		InitiateLoadingSequenceFor(targetInstance, loadAndWait);
		this.reload = true;
	}
}
