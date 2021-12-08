using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class EmotionControl : MonoBehaviour
{
	[SerializeField]
	string emotionDescriptionName;
	private void OnEnable()
	{
		Lua.RegisterFunction("EmotionRises", this, SymbolExtensions.GetMethodInfo(() => EmotionRises(string.Empty, 0)));
		Lua.RegisterFunction("EmotionFalls", this, SymbolExtensions.GetMethodInfo(() => EmotionFalls(string.Empty, 0)));
		Lua.RegisterFunction("SetEmotion", this, SymbolExtensions.GetMethodInfo(() => SetEmotion(string.Empty, 0)));
	}

	public void SetEmotion(string emotionVariableName, double value)
	{
		DialogueLua.SetVariable(emotionVariableName, 0);
		if (value != 0)
		{
			EmotionRises(emotionVariableName, (int)value);
		}
		else
		{
			EmotionFalls(emotionDescriptionName, 0);
		}

	}
	public void EmotionRises(string emotionVariableName, double amount)
	{
		var curEmotion = DialogueLua.GetVariable(emotionVariableName).asInt;
		curEmotion += (int)amount;
		var narratorHeader = "[em1]Your heart raced[/em1]";
		var narratorBody = "";
		if (curEmotion >= 4)
		{
			narratorBody = ", pressure built up in your chest.";
		}
		else if (curEmotion == 3)
		{
			narratorBody = ", your hands twitched uncontrollably.";
		}
		else if (curEmotion == 2)
		{
			narratorBody = ", the air surrounding you became thicker and thicker.";
		}
		else if (curEmotion == 1)
		{
			narratorBody = ", sweats dripped from your back.";
		}
		else
		{
			narratorBody = ", you felt resolved.";
		}
		var narratorLine = narratorHeader + narratorBody + "[em1][Stress: " + curEmotion + "][/em1]";
		DialogueLua.SetVariable(emotionVariableName, curEmotion);
		DialogueLua.SetVariable(emotionDescriptionName, narratorLine);
	}
	public void EmotionFalls(string emotionVariableName, double amount)
	{
		var curEmotion = DialogueLua.GetVariable(emotionVariableName).asInt;
		curEmotion -= (int)amount;
		if (curEmotion < 0) curEmotion = 0;
		var narratorHeader = "[em1]Your heart beats slowed down[/em1]";
		var narratorBody = "";
		if (curEmotion >= 4)
		{
			narratorBody = ", pressure built up in your chest.";
		}
		else if (curEmotion == 3)
		{
			narratorBody = ", pressure started to dissipate in your chest.";
		}
		else if (curEmotion == 2)
		{
			narratorBody = ", you managed to seize control of your hands.";
		}
		else if (curEmotion == 1)
		{
			narratorBody = ", breathing became easier and lighter.";
		}
		else
		{
			narratorBody = ", you felt resolved.";
		}
		var narratorLine = narratorHeader + narratorBody + "[em1][Stress: " + curEmotion + "][/em1]";
		DialogueLua.SetVariable(emotionVariableName, curEmotion);
		DialogueLua.SetVariable(emotionDescriptionName, narratorLine);
	}
}
