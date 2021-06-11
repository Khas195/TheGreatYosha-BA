using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/**
 * This class processes the player's input and tell the character to perform the intended action.
 */
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    /** \brief Reference to the character*/
    Character character = null;
    [SerializeField]
    Camera playerCameraView = null;
    [SerializeField]
    UnityEvent interact = new UnityEvent();

    void Update()
    {
        ProcessInput();
    }
    /**
     * All inputs by the player are and should be done in this function .
     * Current Handled Input: . 
     * LeftShift (switching between run and walk mode) .
     * Space (Calls SignalJump function in IMovement) .
     * Recieved Horizontal and vertical then call the Move function in IMovement with those parameters .
     */
    private void ProcessInput()
    {
        if (GameMaster.GetInstance().GetCurrentState().GetEnum().Equals(GameState.GameStateEnum.InGame))
        {
            ProcessInputs();
        }
    }

    private void ProcessInputs()
    {
        var side = Input.GetAxisRaw("Horizontal");
        var forward = Input.GetAxisRaw("Vertical");
        character.RequestMovementType(IMovement.MovementType.Run);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.RequestJump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            interact.Invoke();
        }

        if (forward != 0 || side != 0)
        {
            var movedir = Util.CalculateMoveDirection(horizontalInput: side, forwardInput: forward
                                            , playerCameraView.transform.forward, playerCameraView.transform.right);

            character.RotateToward(movedir.normalized, rotateY: false);
        }
        forward = Mathf.Abs(forward) > Mathf.Abs(side) ? Mathf.Abs(forward) : Mathf.Abs(side);
        character.RequestMove(forward, 0);
    }
}
