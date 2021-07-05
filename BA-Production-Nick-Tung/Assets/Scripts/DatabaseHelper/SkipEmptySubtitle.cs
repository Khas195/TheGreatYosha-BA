using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class SkipEmptySubtitle : MonoBehaviour
{
	[SerializeField]
	StandardUISubtitlePanel panel;

	void Update()
	{
		if (panel.currentSubtitle != null)
		{
			var dialogueText = panel.currentSubtitle.dialogueEntry.DialogueText;
			dialogueText = dialogueText.ToLower();
			if (dialogueText.Contains("<<skip>>"))
			{
				LogHelper.Log("DialogueHelper - " + "Found empty text - try to continue.");
				panel.OnContinue();
			}
			else
			{

				LogHelper.Log("DialogueHelper - " + panel.currentSubtitle.dialogueEntry.DialogueText);
			}
		}
	}
}
