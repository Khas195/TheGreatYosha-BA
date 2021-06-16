using System;
using UnityEngine;
public class InGameOverlay : InGameUIState
{
	[SerializeField]
	GameObject overlayRoot = null;
	public override Enum GetEnum()
	{
		return InGameUIState.InGameUIEnum.InGameOverlay;
	}
	protected override void Init()
	{
		overlayRoot.SetActive(false);
	}

	public override void OnStateEnter()
	{
		overlayRoot.SetActive(true);
	}

	public override void OnStateExit()
	{
		overlayRoot.SetActive(false);
	}
}
