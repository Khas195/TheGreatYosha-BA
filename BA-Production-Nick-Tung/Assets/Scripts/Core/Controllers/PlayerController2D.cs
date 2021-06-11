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
	Transform cameraFollowPivot = null;
	[SerializeField]
	Character2D character = null;
	[SerializeField]
	UnityEvent interactTrigger = new UnityEvent();
	[SerializeField]
	[Required]
	GameObject playerEntity = null;
	[SerializeField]
	Rigidbody2D cameraBody;
	[SerializeField]
	Rigidbody2D playerBody;

	private void Awake()
	{
	}
	private void OnDestroy()
	{
	}
	// Update is called once per frame
	void Update()
	{
		var side = Input.GetAxisRaw("Horizontal");
		var forward = Input.GetAxisRaw("Vertical");
		if (Input.GetKeyDown(KeyCode.E))
		{
			interactTrigger.Invoke();
		}
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			character.SwitchToRun();
			var localPos = cameraFollowPivot.transform.localPosition;
			localPos.x = cameraLeadOffset;
			cameraFollowPivot.transform.localPosition = localPos;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			character.SwitchToWalk();
			var localPos = cameraFollowPivot.transform.localPosition;
			localPos.x = 0.0f;
			cameraFollowPivot.transform.localPosition = localPos;

		}
		character.Move(side, forward);
	}

	public void SetCharacter(Character2D targetCharacter)
	{
		character = targetCharacter;
	}

	public void ReceiveData(DataPack pack, string eventName)
	{
	}
	public void MoveWithCamera()
	{
		character.SetMovementTarget(cameraBody, true);
	}
	public void MoveWithCharacter()
	{
		character.SetMovementTarget(playerBody);
	}
}
