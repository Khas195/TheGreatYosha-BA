using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractableItem : IInteractable
{
	[SerializeField]
	private Sprite itemSprite;
	[SerializeField]
	private string comment;
	[SerializeField]
	private Sprite commentorSprite;

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
		ItemViewController.GetInstance().SetContent(this.commentorSprite, this.itemSprite, this.comment);

		return base.Interact();
	}

	public override string ToString()
	{
		return base.ToString();
	}
}
