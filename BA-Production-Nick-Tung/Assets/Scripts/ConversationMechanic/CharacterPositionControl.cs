using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
public class CharacterPositionControl : MonoBehaviour
{

	[SerializeField]
	List<CharacterPosition> charPositions;
	// Start is called before the first frame update
	void Start()
	{
		Lua.RegisterFunction("ChangeCharacterPosition", this, SymbolExtensions.GetMethodInfo(() => ChangeCharacterPosition(string.Empty, string.Empty, true)));
	}
	private void OnDestroy()
	{
		Lua.UnregisterFunction("ChangeCharacterPosition");
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void ChangeCharacterPosition(string charName, string posName, bool fadeOutCharOtherPos = true)
	{
		var targetCharPositions = this.charPositions.FindAll(delegate (CharacterPosition charPos)
	    {
		    if (charPos.characterName == charName)
		    {
			    return true;
		    }
		    return false;
	    });

		var targetPos = targetCharPositions.Find(x => x.positionName.Equals(posName));
		if (targetPos == null)
		{
			for (int i = 0; i < targetCharPositions.Count; i++)
			{
				if (targetCharPositions[i].transition.IsFadeOut() == false)
				{
					targetCharPositions[i].transition.FadeOut();
				}
			}
			return;
		}

		if (fadeOutCharOtherPos)
		{
			for (int i = 0; i < targetCharPositions.Count; i++)
			{
				if (targetCharPositions[i] != targetPos)
				{
					if (targetCharPositions[i].transition.IsFadeOut() == false)
					{
						targetCharPositions[i].transition.FadeOut();
					}
				}
			}
		}
		targetPos.transition.FadeIn();
	}
}
