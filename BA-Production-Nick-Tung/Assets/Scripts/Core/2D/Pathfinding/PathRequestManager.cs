using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PathRequestManager : SingletonMonobehavior<PathRequestManager>
{
	[SerializeField]
	PathFinding2D pathFindingAlgorithm = null;
	Queue<PathRequest> requestQueue = new Queue<PathRequest>();
	PathRequest currentPathRequest;
	bool isProcessingPath;
	protected override void Awake()
	{
		base.Awake();
		this.pathFindingAlgorithm = this.GetComponent<PathFinding2D>();
	}
	public void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
	{
		PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
		this.requestQueue.Enqueue(newRequest);
		this.TryProcessNext();
	}

	private void TryProcessNext()
	{
		if (!isProcessingPath && requestQueue.Count > 0)
		{
			currentPathRequest = requestQueue.Dequeue();
			isProcessingPath = true;
			this.pathFindingAlgorithm.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
		}
	}
	public void FinishedProcessPath(Vector3[] path, bool success)
	{
		currentPathRequest.callback(path, success);
		isProcessingPath = false;
		TryProcessNext();
	}

	struct PathRequest
	{
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public Action<Vector3[], bool> callback;
		public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
		{
			this.pathStart = start;
			this.pathEnd = end;
			this.callback = callback;
		}
	}
}
