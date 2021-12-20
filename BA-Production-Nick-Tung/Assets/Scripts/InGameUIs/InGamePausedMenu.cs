using System;
using System.Collections.Generic;
using UnityEngine;
public class InGamePausedMenu : InGameUIState
{
	[SerializeField]
	GameObject menuRoot = null;
	[SerializeField]
	GameObject conversationText = null;
	[SerializeField]
	GameObject responseButtons = null;
	[SerializeField]
	FadeManyTransition backgroundFade = null;
	[SerializeField]
	PauseMenuTransition menuTransition;
	public override Enum GetEnum()
	{
		return InGameUIEnum.Menu;
	}

	public override void OnStateEnter()
	{
		menuRoot.SetActive(true);
		if (InGameUIControl.GetInstance())
		{
			InGameUIControl.GetInstance().MoveToOverlay();
		}
		conversationText.SetActive(false);
		responseButtons.SetActive(false);
		backgroundFade.FadeIn();
		menuTransition.MoveIn();

	}

	public override void OnStateExit()
	{
		menuTransition.MoveOut();
		backgroundFade.FadeOut(() =>
		{
			conversationText.SetActive(true);
			responseButtons.SetActive(true);
			menuRoot.SetActive(false);
		});
	}

	protected override void Init()
	{
		menuRoot.SetActive(false);
	}
}
