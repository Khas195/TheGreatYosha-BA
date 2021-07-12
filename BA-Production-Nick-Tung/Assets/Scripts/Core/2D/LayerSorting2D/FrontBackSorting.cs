using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
/**
 * This class offers the most simple sorting.
 * Whether the character's sprite is aoove or below the sprite is determined by the character's y position in comparison to the host's sprite
 */
[ExecuteInEditMode]
public class FrontBackSorting : IFrontBackSorting
{
	private void Start()
	{
		FrontBackSortingManager.GetInstance().RegisterSorting(this);
	}
	public override bool IsInFront(IFrontBackSorting other)
	{
		if (basePoint.transform.position.y < other.GetBasePoint().y)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public override bool IsBehind(IFrontBackSorting other)
	{
		if (basePoint.transform.position.y > other.GetBasePoint().y)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
