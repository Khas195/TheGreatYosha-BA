using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PauseMenuTransition : MonoBehaviour
{
	[BoxGroup("UI Transitions")]
	[SerializeField]
	Transform overlayTrans;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	Transform outsideTrans;
	[BoxGroup("UI Transitions")]
	[SerializeField]
	Transform pauseMenuTrans;
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
	public void MoveIn()
	{
		transitOrigin = outsideTrans.position;
		transitDestination = overlayTrans.position;
		curTransitTime = 0;
		isInTransit = true;
	}
	[Button]
	public void MoveOut()
	{
		transitOrigin = overlayTrans.position;
		transitDestination = outsideTrans.position;
		curTransitTime = 0;
		isInTransit = true;
	}
	private void Update()
	{
		if (isInTransit)
		{
			Vector3 curPos = Vector3.LerpUnclamped(transitOrigin, transitDestination, transitionCurve.Evaluate(curTransitTime));
			this.pauseMenuTrans.position = curPos;
			curTransitTime += Time.deltaTime;
			if (curTransitTime >= transitionCurve[transitionCurve.length - 1].time)
			{
				isInTransit = false;
				this.pauseMenuTrans.position = transitDestination;
			}
		}
	}

}
