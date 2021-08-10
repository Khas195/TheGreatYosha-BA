using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
	[SerializeField]
	PlayerController2D controller2D = null;
	[SerializeField]
	NPC targetNPC = null;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			controller2D.ForceTalk(targetNPC);
		}
	}
}
