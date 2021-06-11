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
    private void Start() {
        if (useSelfAsHost) {
            host = this.transform;
        }
    }
    
    public override bool IsAboveCharacter(Vector3 characterPos)
    {
        if (IsLeftOfPivot(characterPos))
        {
            return characterPos.y > leftPoint.position.y;
        }
        else
        {
            return characterPos.y > rightPoint.position.y;
        }
    }
    /**  
     * Return whether the character's position is left of the host's pivot.
     * This function determined which pivot is used in the comparision to check whether the character's sprite should be above or below the host's sprite.
    */
    private bool IsLeftOfPivot(Vector3 characterPos)
    {
        return characterPos.x < host.position.x;
    }

    public override bool IsBelowCharacter(Vector3 characterPos)
    {
        if (IsLeftOfPivot(characterPos))
        {
            return characterPos.y < leftPoint.position.y;
        }
        else
        {
            return characterPos.y < rightPoint.position.y;
        }
    }
}
