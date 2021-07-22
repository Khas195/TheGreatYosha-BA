using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node : IHeapItem<Node>
{
	public bool walkable;
	public Vector3 worldPosition;
	public int gridX = 0;
	public int gridY = 0;
	public int gCost;
	public int hCost;
	public Vector2[] vertices;
	public Node parent;
	int heapIndex;
	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}

	public int HeapIndex
	{
		get
		{
			return heapIndex;
		}
		set
		{
			heapIndex = value;
		}
	}

	public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
	{
		this.walkable = walkable;
		this.worldPosition = worldPosition;
		this.gridX = gridX;
		this.gridY = gridY;
	}
	public Node(bool walkable, Vector2 worldPosition, int gridX, int gridY, Vector2[] vertices)
	{
		this.walkable = walkable;
		this.worldPosition = worldPosition;
		this.gridX = gridX;
		this.gridY = gridY;
		this.vertices = vertices;
	}

	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}
	public int CompareTo(Node other)
	{
		int compare = fCost.CompareTo(other.fCost);
		if (compare == 0)
		{
			compare = hCost.CompareTo(other.hCost);
		}
		return -compare;
	}

	public static bool operator ==(Node lhs, Node rhs)
	{
		return lhs.worldPosition == rhs.worldPosition && lhs.walkable == rhs.walkable;
	}
	public static bool operator !=(Node lhs, Node rhs)
	{
		return !(lhs == rhs);
	}
	public void DrawGizmos(float centerSize)
	{
		Gizmos.DrawWireSphere(worldPosition, centerSize);
		Gizmos.DrawLine(vertices[0], vertices[1]);
		Gizmos.DrawLine(vertices[1], vertices[2]);
		Gizmos.DrawLine(vertices[2], vertices[3]);
		Gizmos.DrawLine(vertices[3], vertices[0]);
	}
}
