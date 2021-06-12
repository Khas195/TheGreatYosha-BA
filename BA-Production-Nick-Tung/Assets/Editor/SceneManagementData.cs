using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneManagementData", menuName = "SceneManagement/Data", order = 1)]
public class SceneManagementData : ScriptableObject
{
	[HorizontalLine(color: EColor.Red)]
	[BoxGroup("Scene Control")]
	[SerializeField]
	[Scene]
	string scene = "";
	[HorizontalLine(color: EColor.Red)]
	[SerializeField]
	[BoxGroup("Game Instance Control")]
	[Dropdown("GatherAvailableInstances")]
	[OnValueChanged("OnInstanceChosen")]
	GameInstance instance = null;

	[SerializeField]
	[BoxGroup("Game Instance Control")]
	[Expandable]
	GameInstance chosenInstance = null;


	private void OnInstanceChosen()
	{
		chosenInstance = instance;
	}
	private DropdownList<GameInstance> GatherAvailableInstances()
	{
		var gameInstances = Resources.LoadAll<GameInstance>("ScriptableData/GameInstances");
		var dropDownList = new DropdownList<GameInstance>();
		foreach (var inst in gameInstances)
		{
			dropDownList.Add(inst.name, inst);
		}
		return dropDownList;
	}
	[Button("Scene: Load")]
	public void LoadSelectedScene()
	{
		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			if (EditorBuildSettings.scenes[i].path.Contains(scene))
			{
				EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path, OpenSceneMode.Additive);
			}
		}
	}
	[Button("Scene: Unload")]
	public void UnloadSelectedScene()
	{
		for (int i = 0; i < EditorSceneManager.sceneCount; i++)
		{
			if (EditorSceneManager.GetSceneAt(i).name == scene)
			{
				EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneAt(i));
				return;
			}
		}
	}
	[Button("Game Instance: Add")]
	public void AddInstanceToScene()
	{
		for (int i = 0; i < chosenInstance.sceneList.Count; i++)
		{
			for (int j = 0; j < EditorBuildSettings.scenes.Length; j++)
			{
				if (EditorBuildSettings.scenes[j].path.Contains(chosenInstance.sceneList[i]))
				{
					EditorSceneManager.OpenScene(EditorBuildSettings.scenes[j].path, OpenSceneMode.Additive);
				}
			}
		}
	}
	[Button("Game Instance: Remove")]
	public void RemoveInstanceFromScene()
	{
		for (int i = 0; i < chosenInstance.sceneList.Count; i++)
		{
			for (int j = 0; j < EditorSceneManager.sceneCount; j++)
			{
				if (EditorSceneManager.GetSceneAt(j).name == chosenInstance.sceneList[i])
				{
					EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneAt(j));
					return;
				}
			}

		}
	}
	[Button("PLAY")]
	void PlayFromMasterScene()
	{
		GameMasterEditor.PlayFromMasterScene();

	}
	[Button("ADD MASTER SCENE")]

	void AddMasterScene()
	{
		GameMasterEditor.AddMasterScene();
	}
	[Button("CLEAR ALL")]
	void ClearAll()
	{
		GameMasterEditor.ClearAll();
	}
}
