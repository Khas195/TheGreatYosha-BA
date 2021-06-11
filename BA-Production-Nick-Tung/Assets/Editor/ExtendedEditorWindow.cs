using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExtendedEditorWindow : EditorWindow
{
	protected SerializedObject serializedObject;
	protected SerializedProperty currentProperty;

	string selectedPropertyPath;
	protected SerializedProperty selectedProperty;

	protected void DrawProperties(SerializedProperty prop, bool drawChildren)
	{
		string lastPropPath = string.Empty;
		foreach (SerializedProperty p in prop)
		{
			if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
			{
				EditorGUILayout.BeginHorizontal();
				p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
				EditorGUILayout.EndHorizontal();

				if (p.isExpanded)
				{
					EditorGUI.indentLevel++;
					DrawProperties(p, drawChildren);
					EditorGUI.indentLevel--;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath)) { continue; }
				lastPropPath = p.propertyPath;
				EditorGUILayout.PropertyField(p, drawChildren);
			}
		}
		this.Apply();
	}
	protected void DrawSideBar(SerializedProperty prop)
	{
		foreach (SerializedProperty p in prop)
		{
			if (GUILayout.Button(p.displayName))
			{
				selectedPropertyPath = p.propertyPath;
			}
		}
		if (!string.IsNullOrEmpty(selectedPropertyPath))
		{
			selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
		}

	}
	protected void Apply()
	{
		serializedObject.ApplyModifiedProperties();
	}
}
