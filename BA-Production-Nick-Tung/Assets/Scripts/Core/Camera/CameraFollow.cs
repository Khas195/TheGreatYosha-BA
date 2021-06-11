using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform host = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform characterFollowPoint = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform characterBody = null;

    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    CameraSettings settings = null;
    [BoxGroup("Settings")]
    [SerializeField]
    bool followX = false;
    [BoxGroup("Settings")]
    [SerializeField]
    bool followY = false;
    [BoxGroup("Settings")]
    [SerializeField]
    bool followZ = false;
    [BoxGroup("Current Status")]
    [SerializeField]
    List<Transform> encapsolatedTarget = new List<Transform>();

    [BoxGroup("Current Status")]
    [SerializeField]
    bool honeInX = false;
    bool honeInY = false;

    // Start is called before the first frame update
    void Start()
    {
        if (characterFollowPoint != null)
        {
            encapsolatedTarget.Add(characterFollowPoint);
        }
        this.SetPosition(characterBody.transform.position);
    }

    public bool IsHoningX()
    {
        return honeInX;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(host.position, settings.cameraFollowDeadZoneBoxSize);
        var targetPos = GetCenterPosition(encapsolatedTarget);
        Gizmos.DrawWireSphere(targetPos, 1f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(host.position, 0.5f);
    }

    public Camera GetCamera()
    {
        return this.host.GetComponentInChildren<Camera>();
    }

    public void Follow()
    {
        var targetPos = GetCenterPosition(encapsolatedTarget);
        this.AddEncapsolateObject(characterBody.transform);
        var targetPosY = GetCenterPosition(encapsolatedTarget);
        this.RemoveEncapsolate(characterBody.transform);
        var hostPos = host.position;

        var rightSide = hostPos.x + settings.cameraFollowDeadZoneBoxSize.x / 2;
        var leftSide = hostPos.x - settings.cameraFollowDeadZoneBoxSize.x / 2;
        var topSide = hostPos.y + settings.cameraFollowDeadZoneBoxSize.y / 2;
        var bottomSide = hostPos.y - settings.cameraFollowDeadZoneBoxSize.y / 2;
        if (targetPos.x < leftSide || targetPos.x > rightSide)
        {
            honeInX = true;
        }
        if (targetPosY.y < bottomSide || targetPosY.y > topSide)
        {
            honeInY = true;
        }
        if (honeInX && followX)
        {
            hostPos.x = Mathf.Lerp(hostPos.x, targetPos.x, settings.cameraSpeed * Time.deltaTime);
        }
        if (honeInY && followY)
        {
            hostPos.y = Mathf.Lerp(hostPos.y, targetPosY.y, settings.cameraSpeed * Time.deltaTime);
        }

        if (Mathf.Abs(targetPos.x - hostPos.x) <= 0.1f)
        {
            honeInX = false;
        }
        if (Mathf.Abs(targetPos.y - hostPos.y) <= 0.1f)
        {
            honeInY = false;
        }
        host.transform.position = hostPos;
    }

    public void RemoveEncapsolate(Transform transform)
    {
        if (encapsolatedTarget.Contains(transform))
        {
            encapsolatedTarget.Remove(transform);
        }
    }

    public void SetFollowPercentage(float value)
    {
        settings.cameraSpeed = value;
    }

    public void Clear(bool clearPlayer)
    {
        encapsolatedTarget.Clear();
        if (clearPlayer == false)
        {
            encapsolatedTarget.Add(characterFollowPoint);
        }
    }

    public float GetFollowPercentage()
    {
        return settings.cameraSpeed;
    }

    public void AddEncapsolateObject(Transform obj)
    {
        if (encapsolatedTarget.Contains(obj))
        {
            return;
        }

        this.encapsolatedTarget.Add(obj);
    }

    private Vector3 GetCenterPosition(List<Transform> listOfTargets)
    {
        if (listOfTargets.Count <= 0) return characterFollowPoint.transform.position;
        var bounds = new Bounds(listOfTargets[0].position, Vector3.zero);
        foreach (var target in listOfTargets)
        {
            bounds.Encapsulate(target.position);
        }
        return bounds.center;
    }
    public Vector3 GetCenterPosition()
    {
        return this.GetCenterPosition(encapsolatedTarget);
    }

    public void SetPosition(Vector3 landingPosition)
    {
        var pos = landingPosition;
        pos.z = host.position.z;
        host.position = pos;
    }
}
