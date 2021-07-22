using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
	[SerializeField]
	bool showGizmos = false;
	[SerializeField]
	LayerMask unwalkableMask;
	[SerializeField]
	[ReadOnly]
	Vector2 gridWorldSize;
	[SerializeField]
	[OnValueChanged("CreateGrid")]
	Vector2 tileSize = new Vector2();
	[SerializeField]
	[OnValueChanged("CreateGrid")]
	float gridRotateDegree = 90;
	[SerializeField]
	float gizmosCenterSize = 0.3f;

	Node[,] grid = null;
	[SerializeField]
	int gridSizeX, gridSizeY;

	private void Awake()
	{
		CreateGrid();
	}

	private void CalculateGridSize()
	{
		gridWorldSize.x = gridSizeX * tileSize.x;
		gridWorldSize.y = gridSizeY * tileSize.y;
	}
	public Vector2 ConvertWorldToIsometricPos(int x, int y, Vector2 origin)
	{
		var result = new Vector2(origin.x + (x - y) * (tileSize.x / 2), origin.y + (x + y) * (tileSize.y / 2));
		result = RotatePoints(result, gridRotateDegree);
		return result;
	}
	public Vector2 RotatePoints(Vector2 oldPoint, float angle)
	{
		Vector2 newPoint = new Vector2(oldPoint.x, oldPoint.y);
		newPoint.x = oldPoint.x * Mathf.Cos(angle) - oldPoint.y * Mathf.Sin(angle);
		newPoint.y = oldPoint.x * Mathf.Sin(angle) + oldPoint.y * Mathf.Cos(angle);
		return newPoint;
	}

	[Button]
	private void CreateGrid()
	{

		CalculateGridSize();
		grid = new Node[gridSizeX, gridSizeY];
		var origin = this.transform.position;
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector2[] vertices = new Vector2[4];
				vertices[0] = ConvertWorldToIsometricPos(x, y, origin);
				vertices[1] = ConvertWorldToIsometricPos(x + 1, y, origin);
				vertices[2] = ConvertWorldToIsometricPos(x + 1, y + 1, origin);
				vertices[3] = ConvertWorldToIsometricPos(x, y + 1, origin);
				var centroid = Vector2.zero;
				for (int i = 0; i < vertices.Length; i++)
				{
					centroid += vertices[i];
				}
				centroid = centroid / vertices.Length;
				grid[x, y] = new Node(true, centroid, x, y, vertices);
			}
		}
	}

	public List<Node> GetNeighbourNodes(Node node)
	{
		List<Node> neighbours = new List<Node>();
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0) continue;
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}
		return neighbours;
	}


	private void OnDrawGizmos()
	{
		if (grid != null && showGizmos == true)
		{
			foreach (var n in grid)
			{
				n.DrawGizmos(gizmosCenterSize);
			}
		}
	}
	public Node GetNodeFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

		return grid[x, y];

	}
	public int MaxSize
	{
		get
		{
			return gridSizeX * gridSizeY;
		}
	}
	private void Update()
	{

	}
}
