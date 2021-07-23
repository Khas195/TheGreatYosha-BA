using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NPC : IInteractable
{
	[SerializeField]
	[BoxGroup("UIs")]
	Image interactIcon = null;
	[SerializeField]
	[BoxGroup("Conversation")]
	DialogueSystemTrigger conversationTrigger = null;
	[SerializeField]
	[BoxGroup("Conversation")]
	Transform interactingPoint = null;


	[SerializeField]
	UnityEvent OnConversationEndEvent = null;

	public override void Defocus()
	{
		base.Defocus();
		interactIcon.gameObject.SetActive(false);
	}
	public override void Focus()
	{
		base.Focus();
		interactIcon.gameObject.SetActive(true);
	}
	public override bool Interact()
	{
		if (this.conversationTrigger != null)
		{
			this.conversationTrigger.OnUse();
			this.Defocus();
		}
		return base.Interact();

	}

	public void OnConversationEnd(Transform actor)
	{
		LogHelper.Log("Dialogue System - Conversation Ended");
		this.OnConversationEndEvent.Invoke();
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		if (this.interactingPoint != null)
		{
			var gridPos = TileGrid.GetInstance().GetNodeFromWorldPoint(this.interactingPoint.position);
			Gizmos.DrawWireCube(gridPos.worldPosition, Vector3.one);
		}
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public Vector3 GetInteractPoint()
	{
		return TileGrid.GetInstance().GetNodeFromWorldPoint(this.interactingPoint.position).worldPosition;
	}
}
