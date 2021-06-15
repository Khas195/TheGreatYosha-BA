using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour, IObserver
{
	[SerializeField]
	float cameraLeadOffset = 2.5f;
	[SerializeField]
	float cameraLeadOffsetRuning = 2.5f;
	[SerializeField]
	Rigidbody2D body = null;


	[SerializeField]
	Transform cameraFollowPivot = null;
	[SerializeField]
	Character2D character = null;
	[SerializeField]
	float currentLeadOffset = 0;

	// Update is called once per frame
	void Update()
	{
		var side = Input.GetAxisRaw("Horizontal");
		var forward = Input.GetAxisRaw("Vertical");
		if (body.velocity.magnitude != 0)
		{
			var velDir = body.velocity.normalized;
			cameraFollowPivot.localPosition = velDir * currentLeadOffset;
		}
		else
		{
			cameraFollowPivot.localPosition = Vector3.zero;
		}
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			character.SwitchToRun();
			currentLeadOffset = cameraLeadOffsetRuning;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			character.SwitchToWalk();
			currentLeadOffset = cameraLeadOffset;
		}
		character.Move(side, forward);
	}

	public void ReceiveData(DataPack pack, string eventName)
	{
	}
	private void OnDrawGizmos()
	{
		if (cameraFollowPivot)
		{
			Gizmos.DrawWireSphere(cameraFollowPivot.position, 1);
		}
	}

}
