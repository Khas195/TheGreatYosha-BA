using System;
using UnityEngine;

public class InGamePausedMenu : InGameUIState
{
	[SerializeField]
	GameObject menuRoot = null;
	public override Enum GetEnum()
	{
		return InGameUIEnum.Menu;
	}

	public override void OnStateEnter()
	{
		menuRoot.SetActive(true);
	}

	public override void OnStateExit()
	{
		menuRoot.SetActive(false);
	}

	protected override void Init()
	{
		menuRoot.SetActive(false);
	}
}
