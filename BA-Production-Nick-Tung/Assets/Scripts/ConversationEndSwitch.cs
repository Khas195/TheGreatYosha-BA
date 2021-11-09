using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationEndSwitch : MonoBehaviour
{
	[SerializeField]
	[Scene]
	string sceneToSwitch;

	public void OnConversationEnd(Transform actor)
	{
		SceneManager.LoadScene(sceneToSwitch, LoadSceneMode.Single);
	}
}
