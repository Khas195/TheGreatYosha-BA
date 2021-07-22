using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
	[SerializeField]
	[Required]
	UnityEngine.Grid unityGrid = null;
	Node[,] nodes = null;

	public void CreateNodes()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}
}
