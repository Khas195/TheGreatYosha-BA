using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "Data/CameraSettings", order = 1)]
public class CameraSettings : ScriptableObject
{
    public Vector3 cameraFollowDeadZoneBoxSize;
    public float cameraSpeed;
}
