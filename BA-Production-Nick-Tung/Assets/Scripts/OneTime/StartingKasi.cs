using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class StartingKasi : MonoBehaviour
{
	[SerializeField]
	FadeManyTransition fadeTransition;
	// Start is called before the first frame update
	void Start()
	{
		Lua.RegisterFunction("FadeOutBG", this, SymbolExtensions.GetMethodInfo(() => FadeOutBG()));
		Lua.RegisterFunction("FadeInBG", this, SymbolExtensions.GetMethodInfo(() => FadeInBG()));
		Lua.RegisterFunction("GoToCredits", this, SymbolExtensions.GetMethodInfo(() => GoToCredits()));
	}

	public void GoToCredits()
	{
		GameMaster.GetInstance().LoadCredits();
	}

	public void FadeOutBG()
	{
		fadeTransition.FadeOut();
	}
	public void FadeInBG()
	{
		fadeTransition.FadeIn();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
