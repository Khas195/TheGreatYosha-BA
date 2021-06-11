using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class move the host object by ultilize root motion in the animations of the host object.
 * This class is greatly dependent on the settings of the animation state machine.
 * This is class should be changed according to the settings of the animation state machine.
 */
public class MovementRootMotion : IMovement
{
    [SerializeField]
    /**
     * The animator of the host object
     */
    Animator anim = null;
    [SerializeField]
    float speedStep = 0;
    float targetForwardSpeed = 0;
    float targetSideSpeed = 0;
    public float lastForward { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var forwardSpeed = anim.GetFloat("Forward");
        forwardSpeed = Mathf.Lerp(forwardSpeed, targetForwardSpeed, speedStep);
        anim.SetFloat("Forward", forwardSpeed);

        var sideSpeed = anim.GetFloat("Side");
        sideSpeed = Mathf.Lerp(sideSpeed, targetSideSpeed, speedStep);
        anim.SetFloat("Side", sideSpeed);

    }

    public override void Move(float forward, float side)
    {
        LogHelper.Log("Forward Value: " + forward);

        targetForwardSpeed = forward == 0 ? 0 : this.GetSpeedBasedOnMode();
        if (forward < 0)
        {
            targetForwardSpeed *= -1;
            targetForwardSpeed = Mathf.Clamp(targetForwardSpeed, -0.5f, 0);
        }
        LogHelper.Log("Side Value: " + forward);
        targetSideSpeed = side == 0 ? 0 : this.GetSpeedBasedOnMode();
        if (side < 0)
        {
            targetSideSpeed *= -1;
        }

    }
    public override void SignalJump()
    {
        anim.SetTrigger("Jump");
    }
}
