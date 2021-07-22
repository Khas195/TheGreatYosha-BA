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
	public Node parent;
	int heapIndex;
	public int movementPenalty = 0;

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
		if (lhs is null || rhs is null)
		{
			if (lhs is null && rhs is null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		return lhs.worldPosition == rhs.worldPosition && lhs.walkable == rhs.walkable;
	}
	public static bool operator !=(Node lhs, Node rhs)
	{
		return !(lhs == rhs);
	}

}
