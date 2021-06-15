using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "Data/CameraSettings", order = 1)]
public class CameraSettings : ScriptableObject
{
	public bool useBox = false;
	[ShowIf("useBox")]
	public Vector3 deadZoneBox;
	[HideIf("useBox")]
	public float deadZoneRadius;
	public float cameraSpeed;
}
