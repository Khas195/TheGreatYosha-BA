using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    Camera host = null;
    [SerializeField]
    List<Transform> encapsulatedTargets = new List<Transform>();
    [SerializeField]
    private Transform character = null;

    [SerializeField]
    float minZoom = 0;
    [SerializeField]
    float maxZoom = 1;
    [SerializeField]
    [Tooltip("Each frame the camera move x percentage closer to the target")]
    float followPercentage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var greatestDistance = GetGreatestDistance(encapsulatedTargets);
        var clampedZoomValue = Mathf.Clamp(greatestDistance, minZoom, maxZoom);
        host.orthographicSize = Mathf.Lerp(host.orthographicSize, clampedZoomValue, followPercentage);
    }

    public void Clear(bool clearPlayer)
    {
        encapsulatedTargets.Clear();
        if (clearPlayer == false)
        {
            encapsulatedTargets.Add(character);
        }
    }

    public float GetMaxZoom()
    {
        return maxZoom;
    }

    public void SetMaxZoom(float newMaxZoom)
    {
        this.maxZoom = newMaxZoom;
    }

    public void AddEncapsolateObject(Transform obj)
    {
        encapsulatedTargets.Add(obj);
    }

    public float GetFollowPercentage()
    {
        return followPercentage;
    }

    float GetGreatestDistance(List<Transform> encapsulatedTargets)
    {
        if (encapsulatedTargets.Count <= 1)
        {
            return 0;
        }
        var bounds = new Bounds();
        foreach (var target in encapsulatedTargets)
        {
            bounds.Encapsulate(target.position);
        }
        var result = bounds.size.x < bounds.size.y ? bounds.size.y : bounds.size.x;
        return result;
    }

    public void SetFollowPercentage(float zoomFollowPercentage)
    {
        followPercentage = zoomFollowPercentage;
    }
}
