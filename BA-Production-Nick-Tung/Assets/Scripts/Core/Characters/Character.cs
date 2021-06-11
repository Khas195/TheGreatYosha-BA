using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField]
    bool canMove = false;
    [SerializeField]
    [ShowIf("canMove")]
    IMovement movement = null;
    [SerializeField]
    bool haveStats = false;
    [SerializeField]
    [ShowIf("haveStats")]
    CharacterData data = null;

    private void Start()
    {
        this.movement.SetMovementData(data.moveData);
    }
    public void RequestMovementType(IMovement.MovementType newMode)
    {
        if (movement)
        {
            movement.SetMovementMode(newMode);
        }
    }

    public void RequestJump()
    {
        if (movement)
        {
            movement.SignalJump();
        }
    }
    public void RotateToward(Vector3 direction, bool rotateY)
    {
        if (movement)
        {
            movement.RotateToward(direction, rotateY);
        }
    }

    public void RequestMove(float forward, int side)
    {
        if (movement)
        {
            movement.Move(forward, side);
        }
    }
}


