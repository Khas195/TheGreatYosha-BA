using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ArchetypesUIFill
{
	public Image image = null;
	[Expandable]
	public ArcheTypeData data = null;

	public int curValue = 0;
	public int maxValue = 0;
}
public class ArcheTypesUI : MonoBehaviour
{
	[SerializeField]
	[ReorderableList]
	List<ArchetypesUIFill> archeTypeUIs = new List<ArchetypesUIFill>();
	[SerializeField]
	float duration = 3.0f;

	// Update is called once per frame
	void Update()
	{
		foreach (var ui in archeTypeUIs)
		{
			if (ui.image && ui.data)
			{
				ui.curValue = ui.data.GetValue();
				ui.maxValue = ui.data.GetMaxValue();
				ui.image.fillAmount = (float)ui.curValue / ui.maxValue;
			}
		}
	}
}
