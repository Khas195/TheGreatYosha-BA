using System;
using UnityEngine;

public class InGameDeathMenu : InGameUIState
{
	[SerializeField]
	GameObject menuRoot = null;
	[SerializeField]
	FadeManyTransition transition = null;
	public override Enum GetEnum()
	{
		return InGameUIEnum.DeathMenu;
	}

	public override void OnStateEnter()
	{
		menuRoot.SetActive(true);
		transition.FadeIn();
	}

	public override void OnStateExit()
	{
		menuRoot.SetActive(false);
		transition.FadeOut();
	}

	protected override void Init()
	{
		menuRoot.SetActive(false);
		transition.FadeOut();

	}
}
