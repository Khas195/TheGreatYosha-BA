using System;
using NaughtyAttributes;
using UnityEngine;

/**
 * This class aims to generalize the possible sorting method for 2d sprites in a top down view.
 * The host object (mentioned in blow) is the object which this script will perform its functions on .!--
 * The host object is not necessary be the parent gameobject of the script.!-- 

 */
public abstract class IFrontBackSorting : MonoBehaviour
{
    [SerializeField]
    [HideIf("useSelfAsHost")]
    /** The host game object of which this script will act upon */
    protected Transform host;
    [SerializeField]
    /** This option allowed this script to use the parent game object as host instead. */
    protected bool useSelfAsHost = true;
    /** Get the host position */
    public virtual Vector3 HostPosition()
    {
        if (host == null)
        {
            LogHelper.LogError(this.gameObject + " is missing its host for frontback sorting.");
        }
        return host.position;
    }
    /** Set the host position
     * \param pos is the new host's position.
     */
    public virtual void SetHostPosition(Vector3 pos)
    {
        host.position = pos;
    }
    /**
     * Check whether the host's sprite should be above the character's sprite.
     * \param characterPos is the position (vector3) of the character
     * \param hostPos is the position of the host 
     */
    public abstract bool IsAboveCharacter(Vector3 characterPos);

    /**
    * Check whether the host's sprite should be below the character's sprite.
    * \param characterPos is the position (vector3) of the character
    * \param hostPos is the position of the host 
    */
    public abstract bool IsBelowCharacter(Vector3 characterPos);
}
