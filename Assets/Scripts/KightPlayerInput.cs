using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class KightPlayerInput : MonoBehaviour
{
    private KnightPlayerMovement playerMovement;
    private PauseMenu pauseMenu;
    private EdgeClimb edgeClimb;

    void Start()
    {
        playerMovement = GetComponent<KnightPlayerMovement>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        edgeClimb = GetComponent<EdgeClimb>();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        playerMovement.SetMoveInput(context.ReadValue<Vector2>());
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.interaction is PressInteraction)
        {
            if (context.action.phase == InputActionPhase.Started)
            if (playerMovement.GetEdgeHanging())
            {
                //if edge hanging start edge climb
                edgeClimb.EdgeClimbStart();
            }
            else
            {
                //if not edge hanging, just jump
                playerMovement.Jump();
            }

        if (context.interaction is HoldInteraction)
        {
            if (context.action.phase == InputActionPhase.Performed)
            {
                playerMovement.jetpackOn = true;
            }
            else if(context.action.phase == InputActionPhase.Canceled)
            {
                playerMovement.jetpackOn = false;
            }
        }
    }

    public void JetPackDashInput(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed)
        {
            playerMovement.Dash();
        }
    }
    
    public void ChargeInput(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            playerMovement.Charge();
        }
    }

    public void CrouchInput(InputAction.CallbackContext context)
    {
        //if button is pressed and not crouching
        if (context.action.phase == InputActionPhase.Started && !playerMovement.GetSprinting())
        {
            playerMovement.SetCrouching(true);
        }
        //if button is released and crouching
        else if (context.action.phase == InputActionPhase.Canceled && playerMovement.GetCrouching())
        {
            playerMovement.SetCrouching(false);
        }
        //if button is pressed, sprinting and not sliding
        else if (context.action.phase == InputActionPhase.Started && playerMovement.GetSprinting() && !playerMovement.GetSliding())
        {
            StartCoroutine(playerMovement.Sliding());
        }
    }

    public void SprintInput(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed && !playerMovement.GetSprinting() && playerMovement.GetVertical() > 0)
        {
            playerMovement.Sprint(true);
        }
        else if (context.action.phase == InputActionPhase.Canceled && playerMovement.GetSprinting())
        {
            playerMovement.Sprint(false);
        }
    }

    public void PauseInput(InputAction.CallbackContext context)
    {
        pauseMenu.MenuInput();
    }
}
