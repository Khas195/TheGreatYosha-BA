using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class NPC : IInteractable
{
	[SerializeField]
	[BoxGroup("UIs")]
	Image interactIcon = null;
	[SerializeField]
	[BoxGroup("Conversation")]
	DialogueSystemTrigger conversationTrigger = null;

	[ConversationPopup]
	public string test;

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
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
