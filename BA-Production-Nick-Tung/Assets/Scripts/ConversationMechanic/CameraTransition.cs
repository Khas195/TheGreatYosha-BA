using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CameraTransition : MonoBehaviour, IObserver
{
	[BoxGroup("UI Transitions")]
	[SerializeField]
	Transform cameraTrans;
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

	[Button]
	public void MoveToOverlay()
	{
		if (isInTransit) return;
		transitOrigin = cameraTrans.position;
		transitDestination = overlayTrans.position;
		curTransitTime = 0;
		isInTransit = true;
	}
	[Button]
	public void MoveToConversation()
	{
		if (isInTransit) return;
		transitOrigin = cameraTrans.position;
		transitDestination = conversationTrans.position;
		curTransitTime = 0;
		isInTransit = true;
	}
	[Button]
	public void MoveToItemView()
	{
		if (isInTransit) return;
		transitOrigin = cameraTrans.position;
		transitDestination = itemViewTrans.position;
		curTransitTime = 0;
		isInTransit = true;
	}
	private void Update()
	{
		if (isInTransit)
		{
			Vector3 curPos = Vector3.LerpUnclamped(transitOrigin, transitDestination, transitionCurve.Evaluate(curTransitTime));
			this.cameraTrans.position = curPos;
			curTransitTime += Time.deltaTime;
			if (curTransitTime >= transitionCurve[transitionCurve.length - 1].time)
			{
				isInTransit = false;
				this.cameraTrans.position = transitDestination;
			}
		}
	}
	private void Start()
	{
		PostOffice.Subscribes(this, "UIStateChanged");
	}
	private void OnDestroy()
	{
		PostOffice.Unsubscribes(this, "UIStateChanged");
	}
	public void ReceiveData(DataPack pack, string eventName)
	{
		if (eventName.Equals("UIStateChanged"))
		{
			var uiStateEnum = pack.GetValue<InGameUIState.InGameUIEnum>("NewUiState");
			if (uiStateEnum == InGameUIState.InGameUIEnum.ItemView)
			{
				MoveToItemView();
			}
			else if (uiStateEnum == InGameUIState.InGameUIEnum.InConversation)
			{
				MoveToConversation();
			}
			else
			{
				MoveToOverlay();
			}
		}
	}
}
