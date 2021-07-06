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
}
public class ArcheTypesUI : MonoBehaviour
{
	[SerializeField]
	[ReorderableList]
	List<ArchetypesUIFill> archeTypeUIs = new List<ArchetypesUIFill>();

	// Update is called once per frame
	void Update()
	{
		foreach (var ui in archeTypeUIs)
		{
			if (ui.image && ui.data)
			{
				ui.image.fillAmount = ui.data.GetValue() / ui.data.GetMaxValue();
			}
		}
	}
}
