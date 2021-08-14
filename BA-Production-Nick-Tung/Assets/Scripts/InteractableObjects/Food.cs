using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class Food : IInteractable
{
	[SerializeField]
	string dishOwnserVarName = "";
	[SerializeField]
	string poisonMarkVarName = "";
	[SerializeField]
	bool valueToSet = false;
	[SerializeField]
	Image hoverImage = null;
	[SerializeField]
	string timeLineVarName = "";
	[SerializeField]
	Transform interactPoint = null;
	bool interacted = false;


	public override void Defocus()
	{
		hoverImage.gameObject.SetActive(false);
		base.Defocus();
	}

	public override void Focus()
	{
		var poisonItem = DialogueLua.GetVariable(this.poisonMarkVarName).asBool;
		if (poisonItem == true)
		{
			hoverImage.gameObject.SetActive(true);
			base.Focus();
		}
	}

	public override Vector3 GetInteractPoint()
	{
		return this.interactPoint.position;
	}

	public override string GetKindOfInteraction()
	{
		return base.GetKindOfInteraction();
	}

	public override bool Interact()
	{
		if (interacted == true) return false;

		var poisonItem = DialogueLua.GetVariable(this.poisonMarkVarName).asBool;
		if (poisonItem == false) return false;

		DialogueLua.SetVariable(this.dishOwnserVarName, valueToSet);
		var timelineVar = DialogueLua.GetVariable(this.timeLineVarName).asInt;
		timelineVar += 1;
		DialogueLua.SetVariable(this.timeLineVarName, timelineVar);
		interacted = true;

		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIEnum.TransitionState);
		Invoke("TriggerScenarioUpdate", 2.0f);
		return base.Interact();
	}
	public void TriggerScenarioUpdate()
	{
		GameMaster.GetInstance().UpdateScenario();
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		if (this.interactPoint != null)
		{
			var gridPos = TileGrid.GetInstance().GetNodeFromWorldPoint(this.interactPoint.position);
			Gizmos.DrawWireCube(gridPos.worldPosition, Vector3.one);
		}
	}
}
