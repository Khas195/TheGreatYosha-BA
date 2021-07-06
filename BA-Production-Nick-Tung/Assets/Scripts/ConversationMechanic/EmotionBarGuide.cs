using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class EmotionBarGuide : MonoBehaviour
{
	[SerializeField]
	[BoxGroup("UI")]
	[Required]
	Image loveSlide = null;

	[SerializeField]
	[BoxGroup("UI")]
	[Required]
	Image hateSlide = null;

	[SerializeField]
	[BoxGroup("UI")]
	[Required]
	RectTransform indicateBar = null;


	[SerializeField]
	[BoxGroup("DataBase Settings")]
	DialogueDatabase database;

	[SerializeField]
	[BoxGroup("DataBase Settings")]
	[Dropdown("GetLuaVariables")]
	string luaVariable = null;

	[SerializeField]
	[BoxGroup("Control")]
	[Range(0.0f, 1.0f)]
	float hateLoveControl = 0.5f;
	[SerializeField]
	[BoxGroup("Control")]
	int maxHateValue = -10;
	[SerializeField]
	[BoxGroup("Control")]
	int maxLoveValue = 10;
	[SerializeField]
	[BoxGroup("Control")]
	bool useTestValue = false;
	[SerializeField]
	[BoxGroup("Control")]
	[ShowIf("useTestValue")]
	int testValue = 0;
	[SerializeField]
	[BoxGroup("Control")]
	[ReadOnly]
	int luaValue = 0;



	[SerializeField]
	[BoxGroup("Transition")]
	float transitionDuration;

	[SerializeField]
	[BoxGroup("Transition")]
	[ReadOnly]
	float timeElapsed;
	[SerializeField]
	[BoxGroup("Transition")]
	[ReadOnly]
	float targetHateLove = 0.5f;
	[SerializeField]
	[BoxGroup("Transition")]
	[ReadOnly]
	float startHateLove = 0.5f;
	[SerializeField]
	[BoxGroup("Transition")]
	[ReadOnly]
	bool isTransition = false;

	private void Start()
	{
		UpdateLuaValue();
		startHateLove = targetHateLove = hateLoveControl;
		isTransition = false;
		timeElapsed = 0;
	}

	private DropdownList<string> GetLuaVariables()
	{
		var result = new DropdownList<string>();
		result.Add("Empty", "");
		if (database != null)
		{
			foreach (var variable in database.variables)
			{
				if (variable.Name.ToLower().Contains(GameKeyWord.EMOTION_LOOKUP))
				{
					result.Add(variable.Name, variable.Name);
				}
			}
		}
		return result;
	}

	void Update()
	{
		UpdateLuaValue();
		MapValueToControl();
		UpdateSlidersToControl();
		HandleTransition();
	}

	private void HandleTransition()
	{
		if (isTransition)
		{
			hateLoveControl = Mathf.Lerp(startHateLove, targetHateLove, timeElapsed / transitionDuration);
			timeElapsed += Time.deltaTime;
			if (timeElapsed > transitionDuration)
			{
				isTransition = false;
				hateLoveControl = targetHateLove;
			}
		}
	}

	private void MapValueToControl()
	{
		var valueToCheck = useTestValue ? testValue : luaValue;

		targetHateLove = Mathf.InverseLerp(maxHateValue, maxLoveValue, valueToCheck);

		if (targetHateLove != hateLoveControl && isTransition == false)
		{
			startHateLove = hateLoveControl;
			isTransition = true;
			timeElapsed = 0;
		}
	}

	private void UpdateLuaValue()
	{
		if (luaVariable != "")
		{
			luaValue = DialogueLua.GetVariable(luaVariable).asInt;
		}
	}
	private void UpdateSlidersToControl()
	{
		if (hateSlide != null) hateSlide.fillAmount = 1 - hateLoveControl;
		if (loveSlide != null) loveSlide.fillAmount = hateLoveControl;

		if (indicateBar != null)
		{
			var rotateDegree = Mathf.Lerp(-90, 90, hateLoveControl);
			indicateBar.rotation = Quaternion.Euler(0, 0, rotateDegree * -1);
		}
	}
}
