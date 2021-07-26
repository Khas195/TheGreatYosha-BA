using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class HighlightingWords : MonoBehaviour
{
	[SerializeField]
	[Required]
	WordHighlightingDict dictionary = null;
	public void OnConversationLine(Subtitle subtitle)
	{
		var text = subtitle.formattedText.text;
		text = subtitle.speakerInfo.Name + ": " + text;
		text = dictionary.HighlightIn(text);
		subtitle.formattedText.text = text;

	}

}
