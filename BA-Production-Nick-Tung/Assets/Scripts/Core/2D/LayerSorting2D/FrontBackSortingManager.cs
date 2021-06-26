using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class manages all the game object that runs the different sorting methods.
 * This class also sorts the host object of these scripts appropriately.
 */
public class FrontBackSortingManager : MonoBehaviour
{
	[SerializeField]
	List<IFrontBackSorting> sortingObjects = new List<IFrontBackSorting>();
	[SerializeField]
	Transform characterPivot = null;
	[SerializeField]
	Transform characterEntity = null;
	// Start is called before the first frame update
	void Start()
	{
		var sortingList = GameObject.FindObjectsOfType<IFrontBackSorting>();
		foreach (var sortingTarget in sortingList)
		{
			sortingObjects.Add(sortingTarget);
		}
	}

	// Update is called once per frame
	void Update()
	{
		var lowestPos = sortingObjects[0].HostPosition();
		foreach (var target in sortingObjects)
		{
			var pos = target.HostPosition();
			target.SetHostPosition(pos);
		}
	}

}
