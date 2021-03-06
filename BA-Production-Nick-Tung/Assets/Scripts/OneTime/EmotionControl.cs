using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class EmotionControl : MonoBehaviour
{
	[SerializeField]
	string emotionDescriptionName;
	[SerializeField]
	string khanConnection;
	[SerializeField]
	string albiConnection;
	[SerializeField]
	AudioSource heartBeatSource;
	[SerializeField]
	AudioSource breathingSource;
	[SerializeField]
	AudioClip fastHeart;
	[SerializeField]
	AudioClip mediumHeart;
	[SerializeField]
	AudioClip slowHeart;
	[SerializeField]
	AudioSource gameMusic;
	[SerializeField]
	float curMusicTime = 0;
	[SerializeField]
	float musicTransTime = 1;
	[SerializeField]
	float startVolumn = 0;
	[SerializeField]
	float targetVolumn = 0;

	[SerializeField]
	bool isTransiting = false;
	[SerializeField]
	Action musicCallback = null;
	private void OnEnable()
	{
		Lua.RegisterFunction("EmotionRises", this, SymbolExtensions.GetMethodInfo(() => EmotionRises(string.Empty, 0)));
		Lua.RegisterFunction("EmotionFalls", this, SymbolExtensions.GetMethodInfo(() => EmotionFalls(string.Empty, 0)));
		Lua.RegisterFunction("SetEmotion", this, SymbolExtensions.GetMethodInfo(() => SetEmotion(string.Empty, 0)));
		DialogueManager.AddLuaObserver("Variable['" + khanConnection + "']", LuaWatchFrequency.EveryDialogueEntry, OnConnectionStatusChanged);
		DialogueManager.AddLuaObserver("Variable['" + albiConnection + "']", LuaWatchFrequency.EveryDialogueEntry, OnConnectionStatusChanged);
		Lua.RegisterFunction("SwitchMusic", this, SymbolExtensions.GetMethodInfo(() => SwitchMusic(string.Empty)));
		Lua.RegisterFunction("ChangePlayerName", this, SymbolExtensions.GetMethodInfo(() => ChangePlayerName(string.Empty)));
	}
	public void ChangePlayerName(string newName)
	{
		DialogueLua.SetActorField("Player", "Use Display Name", true);
		DialogueLua.SetActorField("Player", "Display Name", newName);
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
			EmotionFalls(emotionVariableName, 0);
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
			curEmotion = 4;
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

		var connectedToKhan = DialogueLua.GetVariable(khanConnection).asBool;
		var connectedToAlbi = DialogueLua.GetVariable(albiConnection).asBool;
		if (connectedToKhan || connectedToAlbi)
		{
			PlaySoundsToEmotion(curEmotion);
			HandleEmotionPose("Player", curEmotion, true);
		}
		if (emotionVariableName == "Conversations_New.AlbiEmotion")
		{
			HandleEmotionPose("Albi", curEmotion, connectedToAlbi);
		}
		else
		{
			HandleEmotionPose("Khan", curEmotion, connectedToKhan);
		}
	}

	private void HandleEmotionPose(string charName, int curEmotion, bool isConnected)
	{
		if (isConnected)
		{
			if (curEmotion < 2)
			{
				CharacterPositionControl.GetInstance().ChangeCharacterPosition(charName, "ConnectionEmotion0", true);
			}
			else if (curEmotion >= 2 && curEmotion < 3)
			{
				CharacterPositionControl.GetInstance().ChangeCharacterPosition(charName, "ConnectionEmotion2", true);
			}
			else
			{
				CharacterPositionControl.GetInstance().ChangeCharacterPosition(charName, "ConnectionEmotion3", true);
			}
		}
		else
		{
			if (curEmotion < 2)
			{
				CharacterPositionControl.GetInstance().ChangeCharacterPosition(charName, "Emotion0", true);
			}
			else if (curEmotion >= 2 && curEmotion < 3)
			{
				CharacterPositionControl.GetInstance().ChangeCharacterPosition(charName, "Emotion2", true);
			}
			else
			{
				CharacterPositionControl.GetInstance().ChangeCharacterPosition(charName, "Emotion3", true);
			}
		}
	}

	private void StopAllSounds()
	{
		this.breathingSource.Stop();
		this.heartBeatSource.Stop();
	}

	public void EmotionFalls(string emotionVariableName, double amount)
	{
		var curEmotion = DialogueLua.GetVariable(emotionVariableName).asInt;
		curEmotion -= (int)amount;
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
			curEmotion = 0;
		}
		var narratorLine = narratorHeader + narratorBody + "[em1][Stress: " + curEmotion + "][/em1]";
		DialogueLua.SetVariable(emotionVariableName, curEmotion);
		DialogueLua.SetVariable(emotionDescriptionName, narratorLine);
		var connectedToKhan = DialogueLua.GetVariable(khanConnection).asBool;
		var connectedToAlbi = DialogueLua.GetVariable(albiConnection).asBool;
		if (connectedToKhan || connectedToAlbi)
		{
			PlaySoundsToEmotion(curEmotion);
			HandleEmotionPose("Player", curEmotion, true);
		}
		if (emotionVariableName == "Conversations_New.AlbiEmotion")
		{
			HandleEmotionPose("Albi", curEmotion, connectedToAlbi);
		}
		else
		{
			HandleEmotionPose("Khan", curEmotion, connectedToKhan);
		}
	}
	public void PlaySoundsToEmotion(int emotionValue)
	{
		if (emotionValue >= 3)
		{
			this.heartBeatSource.clip = this.fastHeart;
			this.heartBeatSource.Play();
			this.breathingSource.Play();
		}
		else if (emotionValue >= 1)
		{
			this.heartBeatSource.clip = this.mediumHeart;
			this.heartBeatSource.Play();
			this.breathingSource.Stop();
		}
		else
		{
			this.heartBeatSource.clip = this.slowHeart;
			this.heartBeatSource.Play();
			this.breathingSource.Stop();
		}
	}
	public void OnConnectionStatusChanged(LuaWatchItem luaWatchItem, Lua.Result newValue)
	{
		if (newValue.asBool == false)
		{
			heartBeatSource.Stop();
			breathingSource.Stop();
		}
	}
	private void Update()
	{
		if ((GameState.GameStateEnum)GameMaster.GetInstance().GetCurrentState().GetEnum() == GameState.GameStateEnum.MainMenu)
		{
			StopAllSounds();
			gameMusic.Stop();
		}
		if (isTransiting)
		{
			var curVolumn = Mathf.Lerp(startVolumn, targetVolumn, curMusicTime / musicTransTime);
			gameMusic.volume = curVolumn;
			if (curMusicTime >= musicTransTime)
			{
				curVolumn = targetVolumn;
				isTransiting = false;
				if (musicCallback != null)
				{
					musicCallback();
					musicCallback = null;
				}
			}
			curMusicTime += Time.deltaTime;
		}
	}
	public void OnConversationLine(Subtitle sub)
	{
		LogHelper.Log("Current Speaker: " + sub.speakerInfo.Name);
		if (sub.speakerInfo.Name == "Narrator")
		{
			return;
		}
		var curPosName = CharacterPositionControl.GetInstance().GetCurrentPositionName(sub.speakerInfo.Name);
		if (curPosName == "Idle")
		{
			CharacterPositionControl.GetInstance().ChangeCharacterPosition(sub.speakerInfo.Name, "Talking", true);
		}
		if (sub.speakerInfo.Name != "Player")
		{
			SwitchToIdleIfTalking("Player");
		}
		if (sub.speakerInfo.Name != "Khan")
		{
			SwitchToIdleIfTalking("Khan");
		}
		if (sub.speakerInfo.Name != "Albi")
		{
			SwitchToIdleIfTalking("Albi");
		}
	}

	private static void SwitchToIdleIfTalking(string charName)
	{
		var curPosName = CharacterPositionControl.GetInstance().GetCurrentPositionName(charName);
		if (curPosName == "Talking")
		{
			CharacterPositionControl.GetInstance().ChangeCharacterPosition(charName, "Idle", true);
		}
	}

	public void SwitchMusic(string musicName)
	{
		MusicOut(() =>
		{
			var clip = Resources.Load<AudioClip>("Audio/Music/" + musicName);
			gameMusic.clip = clip;
			gameMusic.Play();
			MusicIn();
		});
	}
	public void MusicOut(Action callback = null)
	{
		targetVolumn = 0;
		startVolumn = 1;
		curMusicTime = 0;
		isTransiting = true;
		this.musicCallback = callback;
	}
	public void MusicIn(Action callback = null)
	{
		targetVolumn = 1;
		startVolumn = 0;
		curMusicTime = 0;
		isTransiting = true;
		this.musicCallback = callback;
	}
}
