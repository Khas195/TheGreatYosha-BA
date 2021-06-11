using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Build Profile", menuName = "Build Settings/Build Profile", order = 1)]
public class BuildProfile : ScriptableObject
{
    [Scene]
    public string masterScene = "";
    [Scene]
    public string loadScene = "";
    [Scene]
    public string logScene = "";

}

