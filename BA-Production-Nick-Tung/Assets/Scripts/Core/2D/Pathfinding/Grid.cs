using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
public abstract class Grid : SingletonMonobehavior<Grid>
{
	public abstract List<Node> GetNeighbourNodes(Node node);


	public abstract Node GetNodeFromWorldPoint(Vector3 worldPosition);
	public abstract int GetMaxSize();
}
