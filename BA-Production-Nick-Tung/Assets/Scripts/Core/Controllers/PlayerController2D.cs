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
	[BoxGroup("Setting")]
	Animator animator = null;



	[SerializeField]
	[BoxGroup("Setting")]
	[ReadOnly]
	bool controlLocked = false;
	[SerializeField]
	[BoxGroup("Interactable")]
	UnityEvent<NPC> OnConversationStartEvent = new UnityEvent<NPC>();
	[SerializeField]
	[BoxGroup("Interactable")]
	UnityEvent<NPC> OnConversationEndEvent = new UnityEvent<NPC>();
	[SerializeField]
	[BoxGroup("Interactable")]
	LayerMask interactMask;
	[SerializeField]
	[BoxGroup("Interactable")]
	[ReadOnly]
	IInteractable hoverInteractable = null;
	[SerializeField]
	[BoxGroup("Interactable")]
	[ReadOnly]
	IInteractable targetInteract = null;
	[SerializeField]
	[BoxGroup("Mouse Click Settings")]
	Camera playerCamera = null;
	[SerializeField]
	[ReadOnly]
	[BoxGroup("Pathfinding Settings")]
	List<Vector3> currentPath = new List<Vector3>();
	[SerializeField]
	[ReadOnly]
	[BoxGroup("Pathfinding Settings")]
	int currentTravelIndex = 0;
	[SerializeField]
	[BoxGroup("Pathfinding Settings")]
	float travelTolerance = 0.5f;
	void Update()
	{
		HandlePathfinding();
		UpdateAnimator(body.velocity);

		if (controlLocked == true) return;
		var mousPos = Input.mousePosition;
		mousPos = playerCamera.ScreenToWorldPoint(mousPos);
		DetectMouseOverInteratable(mousPos);
		if (Input.GetMouseButtonDown(0))
		{
			HandleMouseClick(mousPos);
		}
	}

	private void HandleMouseClick(Vector3 mousPos)
	{
		var movePos = mousPos;
		if (this.hoverInteractable != null)
		{
			if (this.targetInteract != null)
			{
				this.targetInteract.Defocus();
			}
			movePos = this.hoverInteractable.GetInteractPoint();
			this.targetInteract = hoverInteractable;
			this.targetInteract.Focus();
		}
		else
		{
			if (this.targetInteract != null)
			{
				this.targetInteract.Defocus();
				this.targetInteract = null;
			}
		}
		MoveToPosition(movePos);
	}
	public void ForceInteract(IInteractable target)
	{
		this.controlLocked = true;
		var movePos = target.GetInteractPoint();
		this.targetInteract = target;
		this.targetInteract.Focus();
		MoveToPosition(movePos);
	}
	private void DetectMouseOverInteratable(Vector3 mousPos)
	{
		RaycastHit2D hit = Physics2D.Raycast(mousPos, Vector2.zero, Mathf.Infinity, interactMask);
		if (hit.collider != null)
		{
			var interactable = hit.collider.GetComponentInParent<IInteractable>();
			if (interactable != null)
			{
				if (this.hoverInteractable != interactable)
				{
					if (this.hoverInteractable != null)
					{
						this.hoverInteractable.Defocus();
					}
					this.hoverInteractable = interactable;
					this.hoverInteractable.Focus();
				}
			}
		}
		else
		{
			if (this.hoverInteractable != null)
			{
				if (targetInteract != hoverInteractable)
				{
					this.hoverInteractable.Defocus();
				}
				this.hoverInteractable = null;
			}
		}
	}

	private void MoveToPosition(Vector3 mousPos)
	{
		var worldPos = TileGrid.GetInstance().GetNodeFromWorldPoint(mousPos).worldPosition;
		worldPos.z = 0;
		PathRequestManager.GetInstance().RequestPath(this.transform.position, worldPos, OnPathFound);
	}

	private void HandlePathfinding()
	{
		if (currentPath.Count > 0)
		{
			if (currentTravelIndex < currentPath.Count)
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
				}
			}
			else
			{
				if (currentPath.Count > 0)
				{
					var targetPos = currentPath[currentTravelIndex - 1];
					targetPos.z = 0;
					this.transform.position = targetPos;
				}
				OnDestinationReached();
			}
		}
		else
		{
			this.character.Move(0, 0);
		}
	}

	private void OnDestinationReached()
	{
		this.character.Move(0, 0);
		if (targetInteract != null)
		{
			targetInteract.Interact();
		}
		currentPath.Clear();
		LogHelper.Log("AI - Reached Destination.");
	}

	private void UpdateAnimator(Vector3 travelDir)
	{
		if (travelDir.y > 0)
		{
			if (travelDir.x > 0)
			{
				animator.SetTrigger("moveUpRight");
			}
			else if (travelDir.x < 0)
			{
				animator.SetTrigger("moveUpLeft");
			}
			else
			{
				animator.SetTrigger("moveUp");
			}
		}
		else if (travelDir.y < 0)
		{
			if (travelDir.x > 0)
			{
				animator.SetTrigger("moveDownRight");
			}
			else if (travelDir.x < 0)
			{
				animator.SetTrigger("moveDownLeft");
			}
			else
			{
				animator.SetTrigger("moveDown");
			}
		}
		else
		{
			if (travelDir.x > 0)
			{
				animator.SetTrigger("moveRight");
			}
			else if (travelDir.x < 0)
			{
				animator.SetTrigger("moveLeft");
			}
			else
			{
				animator.SetTrigger("IdleAnimation");
			}
		}
	}

	private void OnPathFound(Vector3[] path, bool pathFoundSuccessful)
	{
		if (path.Length <= 0)
		{
			OnDestinationReached();
		}
		this.currentPath.Clear();
		this.currentPath.AddRange(path);
		currentTravelIndex = 0;
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

		Gizmos.color = Color.yellow;
		for (int i = 0; i < this.currentPath.Count; i++)
		{
			Gizmos.DrawWireCube(this.currentPath[i], Vector3.one);
		}
	}
	public void OnConversationStart(Transform actor)
	{
		LogHelper.Log("Player - Conversation Start - Control Locked with " + actor);
		controlLocked = true;
		LogHelper.Log("Player - " + actor);
		this.OnConversationStartEvent.Invoke(actor.GetComponent<NPC>());

	}
	public void OnConversationEnd(Transform actor)
	{
		LogHelper.Log("Player - Conversation End - Control Released with " + actor);
		controlLocked = false;

		this.OnConversationEndEvent.Invoke(actor.GetComponent<NPC>());

		this.targetInteract.Defocus();
		this.targetInteract = null;
	}
	public void LockControl()
	{
		controlLocked = true;
	}
}
