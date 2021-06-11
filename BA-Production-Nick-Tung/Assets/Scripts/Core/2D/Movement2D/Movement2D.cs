using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
/** 
 * This class handles all movement related behavior in 2D.
 * The host object (mentioned in blow) is the object which this script will perform its functions on .
 * The host object is not necessary be the parent gameobject of the script.
 */
public class Movement2D : IMovement
{
    [SerializeField]
    /** The Rigid body of the host object
     * body2D is needed to be assigned for this class to work
     */
    [ShowIf("noCharacter")]
    Rigidbody2D body2D = null;
    [SerializeField]
    /**
     * this value smoothen the acceleration of the host object while speeding up to his intended speed
     * A value of 1 means the host object will snap to his intended speed
     */
    float smoothAcceleration = 0.2f;
    /** The current speed of the host object */
    [SerializeField]
    [ReadOnly]

    float currentSpeed;
    /** represents whether the host object is moving or not */
    [SerializeField]
    [ReadOnly]

    bool inMotion = false;
    void Update()
    {
        UpdateCurrentSpeed();
    }

    /**
     * Update the current movespeed and move it toward the intended runSpeed in the data container.
     * It uses the smoothAcceleration to smoothen the transition to the intended runSpeed
     */
    private void UpdateCurrentSpeed()
    {
        if (inMotion == true && currentSpeed < data.runSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, data.runSpeed, smoothAcceleration);
        }
    }
    /**
     *  Move the host object towards the forward and side inputs of the player.
     * '\param forward is how much the host object should move forward or backward, in the case of 2D -> vertical movement.
     *  \param side is how much the host object should move sideway.
     */
    public override void Move(float forward, float side)
    {
        if (forward != 0 || side != 0)
        {
            inMotion = true;
        }
        else
        {
            inMotion = false;
            currentSpeed = 0;
        }
        var vel = new Vector2(side, forward).normalized * currentSpeed;
        body2D.velocity = vel;
    }
    /** this function signal the jump input of the player and the jump action should be handle in the next fixed update function  */
    public override void SignalJump()
    {
        return;
    }

    public override Vector3 GetVelocity()
    {
        return body2D.velocity;
    }


    public override void SetRigidBody(Rigidbody2D body)
    {
        this.body2D = body;
    }

    public override bool IsTouchingGround()
    {
        return base.IsTouchingGround();
    }


    public override void RotateToward(Vector3 direction, bool rotateY)
    {
        base.RotateToward(direction, rotateY);
    }

}
