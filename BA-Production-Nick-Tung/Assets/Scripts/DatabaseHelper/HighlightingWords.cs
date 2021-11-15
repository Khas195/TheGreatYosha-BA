using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
public class HighlightingWords : MonoBehaviour
{
	[SerializeField]
	[Required]
	[Expandable]
	WordHighlightingDict dictionary = null;
	public void OnConversationLine(Subtitle subtitle)
	{
		var text = subtitle.formattedText.text;
		text = dictionary.HighlightIn(text);
		text += "\n";
		subtitle.formattedText.text = text;
	}

}
