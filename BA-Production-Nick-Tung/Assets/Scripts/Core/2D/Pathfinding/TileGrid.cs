using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TileGrid : Grid
{
	[SerializeField]
	[Required]
	UnityEngine.Grid unityGrid = null;
	[SerializeField]
	Vector2 gridSize = new Vector2();
	List<Node> nodes = new List<Node>();
	public override int GetMaxSize()
	{
		return (int)(gridSize.x * gridSize.y);
	}

	public override List<Node> GetNeighbourNodes(Node node)
	{
		List<Node> neighbors = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if ((x == 0 && y == 0))
				{
					continue;
				}
				int neighborX = node.gridX + x;
				int neighborY = node.gridY + y;
				var cellIndex = new Vector3Int(neighborX, neighborY, 0);
				Node neighborNode = nodes.Find(node => node.gridX == cellIndex.x && node.gridY == cellIndex.y);
				if (neighborNode == null)
				{
					neighborNode = CreateNewNodeFromIndex(cellIndex);
				}
				neighbors.Add(neighborNode);

			}
		}
		return neighbors;
	}

	public override Node GetNodeFromWorldPoint(Vector3 worldPosition)
	{
		Node result = null;
		var cellIndex = unityGrid.WorldToCell(worldPosition);
		if (nodes.Count > 0)
		{
			result = nodes.Find(node => node.gridX == cellIndex.x && node.gridY == cellIndex.y);
		}
		if (result == null)
		{
			result = CreateNewNodeFromIndex(cellIndex);
		}
		return result;
	}

	private Node CreateNewNodeFromIndex(Vector3Int cellIndex)
	{
		Node result;
		var cellPos = unityGrid.GetCellCenterWorld(cellIndex);
		result = new Node(true, cellPos, cellIndex.x, cellIndex.y);
		this.nodes.Add(result);
		return result;
	}
}
