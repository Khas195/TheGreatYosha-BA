using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour, IObserver
{
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
	[BoxGroup("Interactable")]
	[ReadOnly]
	NPC inRangeNPC = null;
	[SerializeField]
	Camera playerCamera = null;
	[SerializeField]
	[ReadOnly]
	Vector3[] currentPath = null;
	[SerializeField]
	[ReadOnly]
	int currentTravelIndex = 0;
	[SerializeField]
	float travelTolerance = 0.5f;
	[SerializeField]
	Animator animator = null;
	void Update()
	{
		HandleKeyboardInput();
		if (Input.GetMouseButtonDown(0))
		{
			var mousPos = Input.mousePosition;
			mousPos = playerCamera.ScreenToWorldPoint(mousPos);
			var worldPos = TileGrid.GetInstance().GetNodeFromWorldPoint(mousPos).worldPosition;
			worldPos.z = 0;
			PathRequestManager.GetInstance().RequestPath(this.transform.position, worldPos, OnPathFound);
		}
		if (currentPath != null)
		{
			if (currentTravelIndex < currentPath.Length)
			{
				var targetPos = currentPath[currentTravelIndex];
				targetPos.z = 0;
				var travelDir = targetPos - this.transform.position;
				travelDir.Normalize();
				if (Vector2.Distance(this.transform.position, targetPos) <= travelTolerance)
				{
					currentTravelIndex += 1;
				}
				else
				{
					this.character.Move(travelDir.x, travelDir.y);
					animator.SetFloat("moveHorizontal", travelDir.x);
					animator.SetFloat("moveVertical", travelDir.y);
					animator.SetBool("IsMoving", true);
				}
			}
			else
			{
				this.character.Move(0, 0);
				animator.SetFloat("moveHorizontal", 0);
				animator.SetFloat("moveVertical", 0);
				animator.SetBool("IsMoving", false);
			}
		}

	}

	private void OnPathFound(Vector3[] path, bool pathFoundSuccessful)
	{
		this.currentPath = path;
		currentTravelIndex = 0;
	}

	private void HandleKeyboardInput()
	{
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
