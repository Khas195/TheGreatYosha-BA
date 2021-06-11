using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    Transform destination = null;
    List<Vector3> waypoints = new List<Vector3>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            waypoints.Clear();
            PathRequestManager.GetInstance().RequestPath(this.transform.position, destination.transform.position, OnPathFinished);
        }
    }
    public void OnPathFinished(Vector3[] waypoints, bool pathfindSuccess)
    {
        if (pathfindSuccess)
        {
            this.waypoints.AddRange(waypoints);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (waypoints.Count > 0)
        {
            Gizmos.DrawCube(waypoints[0], Vector3.one);
            Gizmos.DrawLine(waypoints[0], this.transform.position);
        }

        for (int i = 1; i < waypoints.Count; i++)
        {
            Gizmos.DrawCube(waypoints[i], Vector3.one);
            Gizmos.DrawLine(waypoints[i - 1], waypoints[i]);
        }
    }
}
