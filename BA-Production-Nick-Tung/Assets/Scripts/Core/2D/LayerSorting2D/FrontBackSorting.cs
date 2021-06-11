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
    [SerializeField]
    Transform pivot;
    void Start()
    {
        if (useSelfAsHost)
        {
            host = this.transform;
        }
    }
    [Button]
    public void AddPivot()
    {
        var pivot = new GameObject("Pivot");
        pivot.transform.parent = this.host.transform;
        pivot.transform.localPosition = Vector3.zero;
        this.pivot = pivot.transform;
    }

    private void Update()
    {
        if (host && pivot)
        {
            var newPos = host.transform.position;
            newPos.z = pivot.position.y;
            host.transform.position = newPos;
        }
    }

    /**
     * This function return whether the host's sprite should be above the character's sprite
     */
    public override bool IsAboveCharacter(Vector3 characterPos)
    {
        if (pivot == null)
        {
            LogHelper.LogError(this.gameObject + " is missing its pivot for sorting.");
        }
        return characterPos.y > pivot.position.y;
    }
    /**
    * This function return whether the host's sprite should be below the character's sprite
    */
    public override bool IsBelowCharacter(Vector3 characterPos)
    {
        if (pivot == null)
        {
            LogHelper.LogError(this.gameObject + " is missing its pivot for sorting.");
        }
        return characterPos.y < pivot.position.y;
    }
}
