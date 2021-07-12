using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
/**
 * This class manages all the game object that runs the different sorting methods.
 * This class also sorts the host object of these scripts appropriately.
 */
public class FrontBackSortingManager : SingletonMonobehavior<FrontBackSortingManager>
{
	[SerializeField]
	List<IFrontBackSorting> sortingList = new List<IFrontBackSorting>();
	protected override void Awake()
	{
		base.Awake();
		this.sortingList.Clear();
	}

	public void RegisterSorting(IFrontBackSorting sortingTarget)
	{
		this.sortingList.Add(sortingTarget);
	}
	private void Update()
	{
		if (sortingList.Count >= 1)
		{
			this.Sort();
			this.AssignOrderToList();
		}
	}
	[Button]
	public void Sort()
	{
		sortingList.Sort();
	}
	[Button]
	public void AssignOrderToList()
	{
		sortingList[0].SetSortingLayer(0);
		int currentLayer = sortingList[0].GetTopLayer();
		for (int i = 1; i < sortingList.Count; i++)
		{
			currentLayer = sortingList[i - 1].GetTopLayer() + 1;
			sortingList[i].SetSortingLayer(currentLayer);
		}
	}
	[Button]
	public void Clear()
	{
		this.sortingList.Clear();
	}


}
