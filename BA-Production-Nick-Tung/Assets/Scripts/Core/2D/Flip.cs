using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Flip : MonoBehaviour
{
    [SerializeField]
    bool hasFlipTarget = false;
    [SerializeField]
    [ShowIf("hasFlipTarget")]
    Transform flipTarget = null;
    [SerializeField]
    bool useRigidBody = false;
    [SerializeField]
    [Required]
    Rigidbody2D body = null;

    [SerializeField]
    bool useMovementScript = false;
    [SerializeField]
    [Required]
    [ShowIf("useMovementScript")]
    IMovement movementBehavior = null;
    [SerializeField]
    [ReadOnly]
    bool isFacingRight = true;
    [SerializeField]
    [ReadOnly]
    float velocityX = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velToAsset = Vector3.zero;
        if (useRigidBody)
        {
            velToAsset = body.velocity;
        }
        else if (useMovementScript)
        {
            velToAsset = movementBehavior.GetVelocity();
        }
        if (velToAsset.x < 0 && isFacingRight)
        {
            FlipModel();
        }
        else if (velToAsset.x > 0 && isFacingRight == false)
        {
            FlipModel();
        }
        velocityX = velToAsset.x;
    }

    public void FlipModel()
    {
        var flip = body.transform;
        if (hasFlipTarget)
        {
            flip = flipTarget;
        }
        isFacingRight = !isFacingRight;
        var localScale = flip.localScale;
        localScale.x *= -1;
        flip.localScale = localScale;
    }
}
