using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using NaughtyAttributes;
public class InteractableItem : IInteractable
{
	[SerializeField]
	private Sprite itemSprite;
	[SerializeField]
	[TextArea]
	private string comment;
	[SerializeField]
	bool hasOverlayText = false;
	[SerializeField]
	Vector2 desiredScale = Vector2.one;
	[SerializeField]
	string activateVariableOnOpen = "";

	public override void Defocus()
	{
		base.Defocus();
	}

	public override bool Equals(object other)
	{
		return base.Equals(other);
	}

	public override void Focus()
	{
		base.Focus();
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override Vector3 GetInteractPoint()
	{
		return base.GetInteractPoint();
	}

	public override string GetKindOfInteraction()
	{
		return base.GetKindOfInteraction();
	}

	public override bool Interact()
	{
		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.ItemView);
		ItemViewController.GetInstance().SetContent(this.itemSprite, this.comment, hasOverlayText, desiredScale);
		if (activateVariableOnOpen != "")
		{
			DialogueLua.SetVariable(activateVariableOnOpen, true);
		}
		return base.Interact();
	}

	public override string ToString()
	{
		return base.ToString();
	}
}
