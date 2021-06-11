using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid), typeof(PathRequestManager))]
public class PathFinding2D : MonoBehaviour
{
    Grid grid = null;
    PathRequestManager requestManager;
    private void Awake()
    {
        this.grid = this.GetComponent<Grid>();
        this.requestManager = this.GetComponent<PathRequestManager>();
    }
    public void StartFindPath(Vector3 start, Vector3 end)
    {
        StartCoroutine(FindPath(start, end));
    }
    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        List<Node> path = new List<Node>();
        bool pathSuccess = false;

        Node startNode = grid.GetNodeFromWorldPoint(startPos);
        Node targetNode = grid.GetNodeFromWorldPoint(targetPos);


        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closeSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closeSet.Add(currentNode);
                if (currentNode == targetNode)
                {
                    path = RetracePath(startNode, targetNode);
                    pathSuccess = true;
                    break;
                }
                List<Node> neighbourNodes = grid.GetNeighbourNodes(currentNode);
                foreach (var neighbour in neighbourNodes)
                {
                    if (neighbour.walkable == false || closeSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            Vector3[] waypoints = GenerateWayPoints(path);
            requestManager.FinishedProcessPath(waypoints, pathSuccess);
        }
        requestManager.FinishedProcessPath(new Vector3[0], pathSuccess);
    }

    private Vector3[] GenerateWayPoints(List<Node> path)
    {
        List<Vector3> result = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                result.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        result.Add(path[path.Count - 1].worldPosition);
        return result.ToArray();
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }
    int GetDistance(Node nodeX, Node nodeB)
    {
        int distance = 0;
        int dstX = Mathf.Abs(nodeX.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeX.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            distance = 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            distance = 14 * dstX + 10 * (dstY - dstX);
        }
        return distance;
    }

}
