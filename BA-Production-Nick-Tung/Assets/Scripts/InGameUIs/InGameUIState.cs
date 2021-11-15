using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
public abstract class InGameUIState : State
{
	public enum InGameUIEnum
	{
		InGameOverlay,
		Menu,
		DeathMenu,
		TransitionState,
		InConversation,
		ItemView
	}
	[SerializeField]
	[ReadOnly]
	protected InGameUIControl control;
	protected void Awake()
	{
		this.control = InGameUIControl.GetInstance();
		this.Init();
	}
	protected abstract void Init();
}
