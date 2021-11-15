using System;

public class ConversationState : InGameUIState
{
	public override Enum GetEnum()
	{
		return InGameUIState.InGameUIEnum.InConversation;
	}

	public override void OnStateEnter()
	{
		this.control.MoveToConversation();
		var data = DataPool.GetInstance().RequestInstance();
		data.SetValue("NewUiState", InGameUIEnum.InConversation);
		PostOffice.SendData(data, "UIStateChanged");
		DataPool.GetInstance().ReturnInstance(data);
	}

	public override void OnStateExit()
	{
	}

	protected override void Init()
	{
	}
}
