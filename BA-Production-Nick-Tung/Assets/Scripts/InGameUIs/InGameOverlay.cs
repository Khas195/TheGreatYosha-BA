using System;
using UnityEngine;
public class InGameOverlay : InGameUIState
{
	public override Enum GetEnum()
	{
		return InGameUIState.InGameUIEnum.InGameOverlay;
	}
	protected override void Init()
	{
	}

	public override void OnStateEnter()
	{
		this.control.MoveToOverlay();
		var data = DataPool.GetInstance().RequestInstance();
		data.SetValue("NewUiState", InGameUIEnum.InGameOverlay);
		PostOffice.SendData(data, "UIStateChanged");
		DataPool.GetInstance().ReturnInstance(data);
	}

	public override void OnStateExit()
	{
	}
}
