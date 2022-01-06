using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
public class CharacterPositionControl : SingletonMonobehavior<CharacterPositionControl>
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
	[Button]
	public void FindAllCharacterPositions()
	{
		charPositions.Clear();
		charPositions.AddRange(this.GetComponentsInChildren<CharacterPosition>(true));
		foreach (var pos in charPositions)
		{
			pos.transition.StartFadeOut();
			pos.gameObject.SetActive(true);
		}
	}
	public void ChangeCharacterPosition(string charName, string posName, bool fadeOutCharOtherPos = true)
	{
		LogHelper.Log("Change Character position: " + charName + " Pos: " + posName);
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
		LogHelper.Log("Fade In character : " + charName + " Pos: " + posName);
	}

	protected override void Awake()
	{
		base.Awake();
	}

	public string GetCurrentPositionName(string name)
	{
		var targetCharPositions = this.charPositions.FindAll(delegate (CharacterPosition charPos)
			    {
				    if (charPos.characterName == name)
				    {
					    return true;
				    }
				    return false;
			    });
		foreach (var pos in targetCharPositions)
		{
			if (pos.transition.IsFadeOut() == false)
			{
				return pos.positionName;
			}
		}
		return "None";
	}
}
