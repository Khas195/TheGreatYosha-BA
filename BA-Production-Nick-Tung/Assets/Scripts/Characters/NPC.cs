using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class NPC : IInteractable
{
	[SerializeField]
	[BoxGroup("UIs")]
	Image interactIcon = null;

	public override void Defocus()
	{
		base.Defocus();
		interactIcon.gameObject.SetActive(false);
	}
	public override void Focus()
	{
		base.Focus();
		interactIcon.gameObject.SetActive(true);
	}
	public override bool Interact()
	{
		return base.Interact();
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
