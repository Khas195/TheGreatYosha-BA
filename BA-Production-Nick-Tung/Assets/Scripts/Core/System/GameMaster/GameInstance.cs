using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Game Instance", menuName = "Build Settings/Game Instance", order = 1)]
public class GameInstance : ScriptableObject
{
	[Scene]
	public List<string> sceneList = new List<string>();
	public GameState.GameStateEnum desiredGameState;
#if UNITY_EDITOR
	[Button]
	public void AddScenesToHierarchy()
	{
		for (int i = 0; i < sceneList.Count; i++)
		{
			for (int j = 0; j < UnityEditor.EditorBuildSettings.scenes.Length; j++)
			{
				if (UnityEditor.EditorBuildSettings.scenes[j].path.Contains(sceneList[i]))
				{
					UnityEditor.SceneManagement.EditorSceneManager.OpenScene(UnityEditor.EditorBuildSettings.scenes[j].path, UnityEditor.SceneManagement.OpenSceneMode.Additive);
				}
			}
		}
	}
#else
#endif
}