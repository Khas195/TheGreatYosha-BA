using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class roate the camera entity based on the motion of the mouse.
 */
public class MouseRotateCamera : MonoBehaviour
{
    [SerializeField]
    /** Determined the speed of the rotation */
    float sensitivity = 80;
    /** The limit of which the camera will not rotate */
    [SerializeField]
    float pitchUpperLimit = 40;
    [SerializeField]
    /** The limit of which the camera will not rotate */
    float pitchLowerLimit = -30;
    [SerializeField]
    /** The pivot of which the script will rotate vertically*/
    Transform pitchPivot = null;
    [SerializeField]
    /** The pivot of which the script will rotate horizontally*/
    Transform yawPivot = null;
    /** The current pitch of the pitch pivot*/
    float pitch = 0f;
    /** the current yaw of the yaw pivot*/
    float yaw = 0f;
    void Update()
    {
        RotateYawAndPitch();
    }

    /** 
     * This function rotate the yaw and pitch pivot accoring to the mouse input
     * The pitch angle is limited by the pitchLowerLimit and the pitchUpperLimit
     */
    private void RotateYawAndPitch()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, pitchLowerLimit, pitchUpperLimit);
        yawPivot.localEulerAngles = new Vector3(0f, yaw, 0f);
        pitchPivot.localEulerAngles = new Vector3(pitch, 0f, 0f);
    }
}
