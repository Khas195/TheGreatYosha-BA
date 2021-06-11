using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ConstraintCamera : SingletonMonobehavior<ConstraintCamera>
{
    [SerializeField]
    Transform host = null;
    [SerializeField]
    BoxCollider2D cameraCapturingBound = null;
    [SerializeField]
    [ReadOnly]
    BoxCollider2D mapBound = null;
    [SerializeField]
    Camera cam = null;

    public void RegisterCameraBound(BoxCollider2D cameraBound)
    {
        this.mapBound = cameraBound;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateCameraMapBound()
    {
        if (this.mapBound == null)
        {
            LogHelper.LogWarning("Camera Bound is missing the map Boundary Box!!.", true);
            return;
        }

        var hostPos = host.position;

        var cameraSizeY = 2 * cam.orthographicSize;
        var cameraSizeX = cameraSizeY * cam.aspect;
        hostPos = Constraint(hostPos, cameraSizeY, cameraSizeX, mapBound);

        host.transform.position = hostPos;
    }
    public void UpdateCameraCapturingBound()
    {
        if (this.cameraCapturingBound == null)
        {
            LogHelper.LogError("Camera Bound is missing the capturing Boundary Box!!.", true);
            return;
        }

        var hostPos = host.position;

        var cameraSizeY = 2 * cam.orthographicSize;
        var cameraSizeX = cameraSizeY * cam.aspect;
        hostPos = Constraint(hostPos, cameraSizeY, cameraSizeX, cameraCapturingBound);

        host.transform.position = hostPos;
    }

    private Vector3 Constraint(Vector3 hostPos, float cameraSizeY, float cameraSizeX, BoxCollider2D targetBound)
    {
        var rightBound = targetBound.transform.position.x + targetBound.bounds.extents.x;
        var leftBound = targetBound.transform.position.x - targetBound.bounds.extents.x;
        if (hostPos.x + cameraSizeX / 2 >= rightBound)
        {
            hostPos.x = rightBound - cameraSizeX / 2;
        }
        else if (hostPos.x - cameraSizeX / 2 <= leftBound)
        {
            hostPos.x = leftBound + cameraSizeX / 2;
        }

        var upperBound = targetBound.transform.position.y + targetBound.bounds.extents.y;
        var lowerBound = targetBound.transform.position.y - targetBound.bounds.extents.y;
        if (hostPos.y + cameraSizeY / 2 >= upperBound)
        {
            hostPos.y = upperBound - cameraSizeY / 2;
        }
        else if (hostPos.y - cameraSizeY / 2 <= lowerBound)
        {
            hostPos.y = lowerBound + cameraSizeY / 2;
        }

        return hostPos;
    }
}
