using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class InGameUIControl : SingletonMonobehavior<InGameUIControl>
{
	[BoxGroup("Setup")]
	[SerializeField]
	[Required]
	StateManager uiStates = null;
	[BoxGroup("Setup")]
	[SerializeField]
	[Required]
	InGameUIState startState = null;
	[BoxGroup("Setup")]
	[SerializeField]
	GameInstance mainMenuInstance = null;


	[BoxGroup("UI Transitions")]
	[SerializeField]
	Transform dialoguePanel;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	Transform overlayTrans;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	Transform conversationTrans;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	Transform itemViewTrans;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	AnimationCurve transitionCurve;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	[ReadOnly]
	float curTransitTime = 0;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	[ReadOnly]
	Vector3 transitOrigin;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	[ReadOnly]
	Vector3 transitDestination;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	[ReadOnly]
	bool isInTransit = false;

	private void Start()
	{
		uiStates.RequestState(startState.GetEnum());
	}
	public void RequestState(InGameUIState.InGameUIEnum newState)
	{
		uiStates.RequestState(newState);
	}
	public InGameUIState.InGameUIEnum GetCurrentState()
	{
		return (InGameUIState.InGameUIEnum)uiStates.GetCurrentState().GetEnum();
	}
	public void GoToMainMenu()
	{
		var gameMaster = GameMaster.GetInstance(forceCreate: false);
		if (gameMaster)
		{
			gameMaster.RequestInstance(mainMenuInstance);
		}
	}
	public void Quit()
	{
		var gameMaster = GameMaster.GetInstance(forceCreate: false);
		if (gameMaster)
		{
			gameMaster.ExitGame();
		}
	}
	public void Restart()
	{
		GameMaster.GetInstance().RestartFromLastSave();
	}
	public void MoveToOverlay()
	{
		transitOrigin = dialoguePanel.position;
		transitDestination = overlayTrans.position;
		curTransitTime = 0;
		isInTransit = true;
	}
	public void MoveToConversation()
	{
		transitOrigin = dialoguePanel.position;
		transitDestination = conversationTrans.position;
		curTransitTime = 0;
		isInTransit = true;
	}
	public void MoveToItemView()
	{
		transitOrigin = dialoguePanel.position;
		transitDestination = itemViewTrans.position;
		curTransitTime = 0;
		isInTransit = true;
	}
	private void Update()
	{
		if (isInTransit)
		{
			Vector3 curPos = Vector3.LerpUnclamped(transitOrigin, transitDestination, transitionCurve.Evaluate(curTransitTime));
			this.dialoguePanel.position = curPos;
			curTransitTime += Time.deltaTime;
			if (curTransitTime >= transitionCurve[transitionCurve.length - 1].time)
			{
				isInTransit = false;
				this.dialoguePanel.position = transitDestination;
			}
		}
	}
	[BoxGroup("Test")]
	[SerializeField]
	InGameUIState.InGameUIEnum targetState;

	[Button]
	private void SwitchToConversationState()
	{
		this.RequestState(targetState);
	}
	public void Continue()
	{
		GameMaster.GetInstance().RequestGameState(GameState.GameStateEnum.InGame);
	}

}
