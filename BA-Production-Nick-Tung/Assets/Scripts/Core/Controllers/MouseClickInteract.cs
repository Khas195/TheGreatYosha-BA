using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class MouseClickInteract : MonoBehaviour
{
	[SerializeField]
	Camera playerCamera;
	[SerializeField]
	LayerMask interactMask;
	[SerializeField]
	private IInteractable hoverInteractable;
	[SerializeField]
	int interactAmountBeforeTriggerScene = 2;
	[SerializeField]
	[ReadOnly]
	int currentInteract = 0;
	[SerializeField]
	DialogueSystemTrigger sceneTrigger;

	// Update is called once per frame
	void Update()
	{
		if ((GameState.GameStateEnum)GameMaster.GetInstance().GetCurrentState().GetEnum() == GameState.GameStateEnum.GamePaused)
		{
			return;
		}
		var mousPos = Input.mousePosition;
		mousPos = playerCamera.ScreenToWorldPoint(mousPos);
		DetectMouseOverInteratable(mousPos);
		if (Input.GetMouseButtonDown(0))
		{
			HandleMouseClick();
		}

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
				this.hoverInteractable.Defocus();
				this.hoverInteractable = null;
			}
		}
	}
	private void HandleMouseClick()
	{
		if (hoverInteractable != null)
		{
			hoverInteractable.Interact();
			hoverInteractable.Defocus();
			hoverInteractable = null;
			if (DialogueLua.GetVariable("Conversations_New.ConversationWithAlbi_Ended").asBool == true
			&& DialogueLua.GetVariable("Conversations_New.IsBreaking").asBool == true)
			{
				currentInteract += 1;
				if (currentInteract >= interactAmountBeforeTriggerScene)
				{
					sceneTrigger.OnUse();
				}
			}
		}
		else
		{
			if (DialogueManager.IsConversationActive)
			{
				InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.InConversation);
			}
			else
			{
				InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.InGameOverlay);
			}
		}
	}
}
