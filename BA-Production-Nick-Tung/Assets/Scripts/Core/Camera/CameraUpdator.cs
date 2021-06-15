using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpdator : MonoBehaviour
{
	[SerializeField]
	CameraFollow follow = null;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	private void FixedUpdate()
	{
		if (follow)
		{
			follow.Follow();
		}
	}
	void LateUpdate()
	{

	}
}
