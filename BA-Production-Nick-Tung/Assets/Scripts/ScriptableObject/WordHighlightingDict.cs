using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "WordHighlightSetting", menuName = "Data/WordHighlight", order = 1)]
public class WordHighlightingDict : ScriptableObject
{
	[Serializable]
	public class WordSetting
	{
		public string word = "";
		public Color color = Color.black;
		public bool bold = false;
		public bool italic = false;
	}
	[ReorderableList]
	public List<WordSetting> wordsSetting = new List<WordSetting>();

	public string HighlightIn(string dialogueText)
	{
		foreach (var setting in wordsSetting)
		{
			if (dialogueText.Contains(setting.word))
			{
				var targetWord = Colorize(setting.word, setting.color);
				if (setting.bold)
				{
					targetWord = Bolden(targetWord);
				}
				if (setting.italic)
				{
					targetWord = Italician(targetWord);
				}
				dialogueText = dialogueText.Replace(setting.word, targetWord);
			}
		}
		return dialogueText;
	}
	public String Colorize(String text, Color color)
	{
		return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
	}
	public String Colorize(String text, String color)
	{
		return "<color=" + color + ">" + text + "</color>";
	}
	public String Bolden(String text)
	{
		return "<b>" + text + "</b>";
	}
	public String Italician(String text)
	{
		return "<i>" + text + "</i>";
	}
}