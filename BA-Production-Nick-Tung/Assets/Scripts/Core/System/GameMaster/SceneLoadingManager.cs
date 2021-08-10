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
	GameInstance currentInstance = null;
	GameInstance instanceToLoad = null;
	[SerializeField]
	[ReadOnly]
	bool loadAndWait = true;
	void Start()
	{
		PostOffice.Subscribes(this, GameMasterEvent.INSTANCE_LOADED_EVENT);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name.Equals(profile.loadScene))
		{
			if (currentInstance != null)
			{
				UnloadInstance(currentInstance, removeDubplicate: false);
			}
			LoadInstance(instanceToLoad, loadDuplicate: false);
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
		SceneManager.UnloadSceneAsync(profile.loadScene);
		var data = DataPool.GetInstance().RequestInstance();
		data.SetValue("Instance", currentInstance);
		PostOffice.SendData(data, GameMasterEvent.INSTANCE_LOADED_EVENT);
		DataPool.GetInstance().ReturnInstance(data);
		this.scenesLoading.Clear();
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

	public void LoadInstance(GameInstance requestedInstance, bool loadDuplicate = true)
	{
		for (int i = 0; i < requestedInstance.sceneList.Count; i++)
		{
			if (loadDuplicate == false && currentInstance != null)
			{
				if (currentInstance.sceneList.Contains(requestedInstance.sceneList[i]) == false)
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

	public void UnloadInstance(GameInstance instance, bool removeDubplicate = true)
	{
		for (int i = 0; i < instance.sceneList.Count; i++)
		{
			if (removeDubplicate == false)
			{
				if (instanceToLoad.sceneList.Contains(instance.sceneList[i]) == false)
				{
					SceneManager.UnloadSceneAsync(instance.sceneList[i]);
				}
			}
			else
			{
				SceneManager.UnloadSceneAsync(instance.sceneList[i]);
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
}
