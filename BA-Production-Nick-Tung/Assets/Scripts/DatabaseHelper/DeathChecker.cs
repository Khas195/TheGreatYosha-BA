using UnityEngine;

public class DeathChecker : MonoBehaviour
{
	[SerializeField]
	string deathVarName = "";
	public void OnConversationEnd(Transform actor)
	{
		if (PixelCrushers.DialogueSystem.DialogueLua.GetVariable(deathVarName).asBool == true)
		{
			GameMaster.GetInstance().RestartFromLastSave();
		}
	}
}
