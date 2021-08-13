using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
/**
 * This class manages all the game object that runs the different sorting methods.
 * This class also sorts the host object of these scripts appropriately.
 */
[ExecuteInEditMode]
public class FrontBackSortingManager : SingletonMonobehavior<FrontBackSortingManager>, IObserver
{
	[SerializeField]
	List<IFrontBackSorting> sortingList = new List<IFrontBackSorting>();
	protected override void Awake()
	{
		base.Awake();
		PostOffice.Subscribes(this, GameMasterEvent.ON_LOAD_NEW_STANCE_START);
		PostOffice.Subscribes(this, GameMasterEvent.ON_INSTANCE_LOADED);
		this.Clean();
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
	public void Clean()
	{
		this.sortingList.RemoveAll(item => item == null);
	}
	private void OnDestroy()
	{
		this.sortingList.Clear();
		PostOffice.Unsubscribes(this, GameMasterEvent.ON_INSTANCE_LOADED);
		PostOffice.Unsubscribes(this, GameMasterEvent.ON_LOAD_NEW_STANCE_START);
	}

	public void ReceiveData(DataPack pack, string eventName)
	{
		if (eventName.Equals(GameMasterEvent.ON_LOAD_NEW_STANCE_START) || eventName.Equals(GameMasterEvent.ON_INSTANCE_LOADED))
		{
			this.Clean();
		}
	}
}
