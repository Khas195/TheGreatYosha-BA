using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField]
    Transform host = null;
    [SerializeField]
    Transform cameraTrans = null;
    [SerializeField]
    float minsDistance = 1f;
    [SerializeField]
    float maxDistance = 4f;
    [SerializeField]
    float smoothValue = 0.1f;

    Vector3 dollyDir = Vector3.zero;
    [SerializeField]
    [ReadOnly]
    Vector3 dollyDirAdjusted = Vector3.zero;
    [SerializeField]
    [ReadOnly]
    float currentDistance = 0;
    [SerializeField]
    LayerMask checkMask = 0;
    void Start()
    {
        dollyDir = cameraTrans.localPosition.normalized;
        currentDistance = cameraTrans.localEulerAngles.magnitude;
    }
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(host.transform.position, host.TransformPoint(dollyDir * currentDistance));
    }
    void Update()
    {
        var desiredPos = host.TransformPoint(dollyDir * currentDistance);
        RaycastHit hit;

        if (Physics.Linecast(host.transform.position, desiredPos, out hit, checkMask))
        {
            currentDistance = Mathf.Clamp(hit.distance, minsDistance, maxDistance);
        }
        else
        {
            currentDistance = maxDistance;
        }
        cameraTrans.localPosition = Vector3.Lerp(cameraTrans.localPosition, dollyDir * currentDistance, Time.deltaTime * smoothValue);
    }
}
