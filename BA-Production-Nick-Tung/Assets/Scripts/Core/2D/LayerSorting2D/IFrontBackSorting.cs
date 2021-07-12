using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

/**
 * This class aims to generalize the possible sorting method for 2d sprites in a top down view.
 * The host object (mentioned in blow) is the object which this script will perform its functions on .!--
 * The host object is not necessary be the parent gameobject of the script.!-- 

 */
public abstract class IFrontBackSorting : MonoBehaviour, IComparable<IFrontBackSorting>
{
	[SerializeField]
	protected Transform basePoint = null;

	[SerializeField]
	protected SpriteRenderer baseRenderer = null;
	[SerializeField]
	protected List<SpriteRenderer> renderers = new List<SpriteRenderer>();

	public virtual Vector3 GetBasePoint()
	{
		return basePoint.position;
	}

	public virtual void SetSortingLayer(float newLayer)
	{
		if (baseRenderer == null) return;

		List<int> differences = new List<int>();
		foreach (var render in renderers)
		{
			differences.Add(render.sortingOrder - baseRenderer.sortingOrder);
		}
		baseRenderer.sortingOrder = Mathf.RoundToInt(newLayer);
		for (int i = 0; i < renderers.Count; i++)
		{
			renderers[i].sortingOrder = baseRenderer.sortingOrder + differences[i];
		}

	}

	public virtual int GetTopLayer()
	{
		return baseRenderer.sortingOrder;
	}

	/**
* Check whether the host's sprite should be above the character's sprite.
* \param characterPos is the position (vector3) of the character
* \param hostPos is the position of the host 
*/
	public abstract bool IsInFront(IFrontBackSorting other);

	/**
	* Check whether the host's sprite should be below the character's sprite.
	* \param characterPos is the position (vector3) of the character
	* \param hostPos is the position of the host 
	*/
	public abstract bool IsBehind(IFrontBackSorting other);

	public int CompareTo(IFrontBackSorting other)
	{
		if (this.IsInFront(other))
		{
			return 1;
		}
		else if (this.IsBehind(other))
		{
			return -1;
		}
		else
		{
			return 0;
		}
	}
}
