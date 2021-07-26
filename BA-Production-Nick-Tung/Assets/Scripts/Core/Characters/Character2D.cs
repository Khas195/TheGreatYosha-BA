using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Character2D : MonoBehaviour
{
	[SerializeField]
	[BoxGroup("Requirements")]
	[Required]
	Rigidbody2D body = null;

	[SerializeField]
	[BoxGroup("Requirements")]
	[Required]
	MovementData moveData = null;

	IMovement currentMovementBehavior = null;

	[SerializeField]
	[BoxGroup("Requirements")]
	[Required]
	IMovement characterMovement = null;




	// Start is called before the first frame update
	void Awake()
	{
		characterMovement.SetRigidBody(body);
		characterMovement.SetMovementData(moveData);
		currentMovementBehavior = characterMovement;
	}

	public GameObject GetHost()
	{
		return body.gameObject;
	}

	public void Jump()
	{
		characterMovement.SignalJump();
	}


	public void Move(float horizontal, float vertical)
	{
		currentMovementBehavior.Move(vertical, horizontal);
	}

	public void SwitchToRun()
	{
		this.characterMovement.SetMovementMode(IMovement.MovementType.Run);
	}

	public void SwitchToWalk()
	{
		this.characterMovement.SetMovementMode(IMovement.MovementType.Walk);
	}

	public string GetName()
	{
		return name;
	}

}
