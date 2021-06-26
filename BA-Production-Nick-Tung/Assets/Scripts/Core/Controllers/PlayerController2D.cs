using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour, IObserver
{
	[SerializeField]
	[BoxGroup("Setting")]
	float cameraLeadOffset = 2.5f;
	[SerializeField]
	[BoxGroup("Setting")]
	float cameraLeadOffsetRuning = 2.5f;
	[SerializeField]
	[BoxGroup("Setting")]
	Rigidbody2D body = null;


	[SerializeField]
	[BoxGroup("Setting")]
	Transform cameraFollowPivot = null;
	[SerializeField]
	[BoxGroup("Setting")]
	Character2D character = null;
	[SerializeField]
	[BoxGroup("Setting")]
	float currentLeadOffset = 0;
	[SerializeField]
	[BoxGroup("Interactable")]
	[ReadOnly]
	NPC inRangeNPC = null;

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
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (this.inRangeNPC != null)
			{
				this.inRangeNPC.Interact();
			}
		}
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
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (inRangeNPC != null)
		{
			return;
		}

		var npc = other.gameObject.GetComponent<NPC>();
		if (npc != null && npc.IsFocus() == false)
		{
			npc.Focus();
			this.inRangeNPC = npc;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		var npc = other.gameObject.GetComponent<NPC>();
		if (npc == this.inRangeNPC)
		{
			this.inRangeNPC = null;
		}

		if (npc != null && npc.IsFocus() == true)
		{
			npc.Defocus();

		}
	}

}
