using UnityEngine;
/**
 * This class offers a 2 points for the process of determination of whether the character's sprite is above or blow the host's sprite.
 */
public class FrontBackTwoPointSort : IFrontBackSorting
{
	[SerializeField]
	/** The left pivot of the host object. */
	Transform leftPoint = null;
	[SerializeField]
	/** The right pivot of the host object. */
	Transform rightPoint = null;
	private void Start()
	{
		FrontBackSortingManager.GetInstance().RegisterSorting(this);
	}
	private bool IsLeftOfPivot(Vector3 otherPos)
	{
		return otherPos.x < (leftPoint.position.x + (rightPoint.position.x - leftPoint.position.x) / 2);
	}
	public override bool IsInFront(IFrontBackSorting other)
	{
		if (IsLeftOfPivot(other.GetBasePoint()))
		{
			return leftPoint.position.y < other.GetBasePoint().y;
		}
		else
		{
			return rightPoint.position.y < other.GetBasePoint().y;
		}
	}

	public override bool IsBehind(IFrontBackSorting other)
	{
		if (IsLeftOfPivot(other.GetBasePoint()))
		{
			return leftPoint.position.y > other.GetBasePoint().y;
		}
		else
		{
			return rightPoint.position.y > other.GetBasePoint().y;
		}
	}
	public override int GetTopLayer()
	{
		int highestLayer = baseRenderer.sortingOrder;
		foreach (var render in renderers)
		{
			if (render.sortingOrder > highestLayer)
			{
				highestLayer = render.sortingOrder;
			}
		}
		return highestLayer;
	}
	public override Vector3 GetBasePoint()
	{
		return (leftPoint.position + (rightPoint.position - leftPoint.position) / 2);
	}
}
