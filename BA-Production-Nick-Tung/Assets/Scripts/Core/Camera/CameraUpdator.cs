using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpdator : MonoBehaviour
{
	[SerializeField]
	CameraFollow follow = null;
	[SerializeField]
	CameraZoom zoom = null;
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
		if (zoom)
		{
			zoom.Zoom();
		}
	}
	public void UseCameraDataSet(NPC npc)
	{
		var cameraSetting = npc.GetCameraStateForConversation();
		follow.Clear(true);
		follow.AddEncapsolateObject(cameraSetting.cameraPosition);
		zoom.ZoomToConversation(cameraSetting.cameraZoom);
	}
	public void ResetCameraState(NPC npc)
	{
		follow.Clear(true);
		follow.AddPlayer();
		zoom.ZoomToNormalSize();
	}

}
