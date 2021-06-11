using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    Rigidbody2D body = null;

    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    MovementData moveData = null;

    IMovement currentMovementBehavior = null;

    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    IMovement characterMovement = null;
    [SerializeField]
    bool canUseCameraMovement = false;
    [SerializeField]
    [ShowIf("canUseCameraMovement")]
    IMovement cameraMovement = null;


    // Start is called before the first frame update
    void Awake()
    {
        characterMovement.SetRigidBody(body);
        characterMovement.SetMovementData(moveData);
        if (canUseCameraMovement)
        {
            cameraMovement.SetMovementData(moveData);
        }
        currentMovementBehavior = characterMovement;
    }

    public GameObject GetHost()
    {
        return body.gameObject;
    }

    public void Jump()
    {
        characterMovement.SignalJump();
    }


    public void Move(float horizontal, float vertical)
    {
        currentMovementBehavior.Move(vertical, horizontal);
    }

    public void SwitchToRun()
    {
        this.characterMovement.SetMovementMode(IMovement.MovementType.Run);
    }

    public void SwitchToWalk()
    {
        this.characterMovement.SetMovementMode(IMovement.MovementType.Walk);
    }

    public string GetName()
    {
        return name;
    }


    public void SetMovementTarget(Rigidbody2D newBody, bool isCameraBody = false)
    {
        if (isCameraBody)
        {
            body.simulated = false;
            this.cameraMovement.SetRigidBody(newBody);
            currentMovementBehavior = cameraMovement;
        }
        else
        {
            body.simulated = true;
            currentMovementBehavior = characterMovement;
        }
    }

}
