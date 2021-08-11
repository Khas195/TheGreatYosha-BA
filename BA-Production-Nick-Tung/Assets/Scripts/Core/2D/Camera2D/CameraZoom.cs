using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
	[SerializeField]
	Camera host = null;
	[SerializeField]
	private Transform character = null;
	[SerializeField]
	float normalZoomValue = 12;

	[SerializeField]
	float targetSize;
	[SerializeField]
	[Tooltip("Each frame the camera move x percentage closer to the target")]
	float followPercentage;
	private void Start()
	{
		targetSize = normalZoomValue;
	}

	public void Zoom()
	{
		host.orthographicSize = Mathf.Lerp(host.orthographicSize, targetSize, followPercentage);
	}
	public void ZoomToConversation(float zoomValue)
	{
		targetSize = zoomValue;
	}
	public void ZoomToNormalSize()
	{
		targetSize = normalZoomValue;

	}


	public float GetFollowPercentage()
	{
		return followPercentage;
	}



	public void SetFollowPercentage(float zoomFollowPercentage)
	{
		followPercentage = zoomFollowPercentage;
	}
}
