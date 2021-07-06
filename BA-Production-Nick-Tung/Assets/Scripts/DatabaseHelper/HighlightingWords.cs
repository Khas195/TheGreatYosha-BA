using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class HighlightingWords : MonoBehaviour
{
	[SerializeField]
	StandardUISubtitlePanel panel;
	[SerializeField]
	DialogueDatabase database;

	private void Start()
	{
		foreach (var conversation in database.conversations)
		{
			foreach (var entry in conversation.dialogueEntries)
			{
				entry.DialogueText = entry.DialogueText.Bolden();
			}
		}
	}
	void Update()
	{
	}

}
