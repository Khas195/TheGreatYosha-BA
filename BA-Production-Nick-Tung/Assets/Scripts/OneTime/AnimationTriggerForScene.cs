using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerForScene : MonoBehaviour
{
	[SerializeField]
	Animator animator;
	[SerializeField]
	string triggerName;

	[SerializeField]
	string deathName;
	void Start()
	{
		animator.SetTrigger(triggerName);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{

			animator.SetTrigger(deathName);
		}
	}
}
