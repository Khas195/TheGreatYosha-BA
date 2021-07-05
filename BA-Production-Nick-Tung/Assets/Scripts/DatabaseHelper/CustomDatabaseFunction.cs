using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class CustomDatabaseFunction : MonoBehaviour
{
	[SerializeField]
	DialogueDatabase database;
	[SerializeField]
	StandardUISubtitlePanel panel;
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (panel.currentSubtitle != null)
		{

			Debug.Log("TEST TEST" + panel.currentSubtitle.formattedText.text);
		}
	}
}
