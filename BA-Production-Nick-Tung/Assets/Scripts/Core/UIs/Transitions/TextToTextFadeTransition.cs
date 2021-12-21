using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class TextToTextFadeTransition : MonoBehaviour
{
	[SerializeField]
	[ReadOnly]
	Text currentText;

	[SerializeField]
	Text text01;
	[SerializeField]
	Text text02;
	[SerializeField]
	FadeTransition fade01;

	[SerializeField]
	FadeTransition fade02;


	public void SwitchText()
	{
		if (currentText == null || currentText == text02)
		{
			currentText = text01;
		}
		else
		{
			currentText = text02;
		}
	}

	public Text GetCurrentText()
	{
		if (currentText == null)
		{
			SwitchText();
		}
		return currentText;
	}
}
