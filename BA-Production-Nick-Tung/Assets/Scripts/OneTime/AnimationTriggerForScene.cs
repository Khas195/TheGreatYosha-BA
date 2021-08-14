using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerForScene : MonoBehaviour
{
	[SerializeField]
	Animator animator;
	[SerializeField]
	string triggerName;

	void Start()
	{
		animator.SetTrigger(triggerName);
	}

}
