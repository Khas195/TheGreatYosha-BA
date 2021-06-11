using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevelManager : MonoBehaviour
{
    [SerializeField]
    [Required]
    GameObject levelHolder = null;
    [SerializeField]
    [Required]
    GameObjectPool levelButtonPool = null;
    [SerializeField]
    List<GameObject> activeButtons = new List<GameObject>();
    public void RefreshLevelView()
    {
        // LogHelper.Log("Refresh Level view");
        // ClearButtons();
        // LogHelper.Log("Finding Existing Level");
        // bool foundAnyLevel = false;
        // int numOfScene = EditorBuildSettings.scenes.Length;
        // for (int i = 0; i < numOfScene; i++)
        // {
        //     var scene = EditorBuildSettings.scenes[i];
        //     var sceneName = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
        //     if (sceneName.Contains("Level"))
        //     {
        //         foundAnyLevel = true;
        //         LogHelper.Log("Found " + sceneName.Bolden());
        //         sceneName = AddLevelButton(sceneName);
        //     }
        // }
        // if (foundAnyLevel == false)
        // {
        //     LogHelper.Log("Found no existing level".Bolden());
        // }
    }

    private string AddLevelButton(string sceneName)
    {
        var newButton = levelButtonPool.RequestInstance();
        sceneName = sceneName.Substring(0, sceneName.Length - sceneName.IndexOf('.'));
        newButton.GetComponentInChildren<Text>().text = sceneName;
        activeButtons.Add(newButton);
        newButton.transform.parent = levelHolder.transform;
        var buttonComponent = newButton.GetComponent<Button>();

        buttonComponent.onClick.AddListener(delegate
        {
            OnLoadLevelRequest(sceneName);
        });
        return sceneName;
    }

    private void ClearButtons()
    {
        foreach (var button in activeButtons)
        {
            levelButtonPool.ReturnInstance(button);
            button.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        activeButtons.Clear();
    }

    public void OnLoadLevelRequest(string levelName)
    {
    }
}
