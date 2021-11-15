using System;

public class ItemViewState : InGameUIState
{
	public override Enum GetEnum()
	{
		return InGameUIState.InGameUIEnum.ItemView;
	}

	public override void OnStateEnter()
	{
		this.control.MoveToItemView();
		var data = DataPool.GetInstance().RequestInstance();
		data.SetValue("NewUiState", InGameUIEnum.ItemView);
		PostOffice.SendData(data, "UIStateChanged");
		DataPool.GetInstance().ReturnInstance(data);
	}

	public override void OnStateExit()
	{
		ItemViewController.GetInstance().Clear();
	}

	protected override void Init()
	{
	}
}
