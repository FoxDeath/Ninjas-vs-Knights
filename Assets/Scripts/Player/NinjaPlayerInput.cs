using UnityEngine;
using UnityEngine.InputSystem;

public class NinjaPlayerInput : MonoBehaviour
{
    private NinjaPlayerMovement playerMovement;
    private PauseMenu pauseMenu;
    private EdgeClimb edgeClimb;


    void Start()
    {
        playerMovement = GetComponent<NinjaPlayerMovement>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        edgeClimb = GetComponent<EdgeClimb>();
    }

    public void PauseInput(InputAction.CallbackContext context)
    {
        pauseMenu.MenuInput();
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
        else if (context.action.phase == InputActionPhase.Started && playerMovement.GetSprinting() && playerMovement.GetCanSlide())
        {
            StartCoroutine(playerMovement.Sliding());
        }
    }

    public void SprintInput(InputAction.CallbackContext context)
    {
        //if button is pressed, not sprinting and moving forward
        if(context.action.phase == InputActionPhase.Started && !playerMovement.GetSprinting() && playerMovement.GetVertical() > 0)
        {
            playerMovement.Sprint(true);
        }
        //if button is released and sprinting
        else if (context.action.phase == InputActionPhase.Canceled && playerMovement.GetSprinting())
        {
            playerMovement.Sprint(false);
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        //if button is pressed
        if(context.action.phase == InputActionPhase.Performed)
        {
            if(playerMovement.GetEdgeHanging())
            {
                //if edge hanging start edge climb
                edgeClimb.EdgeClimbStart();
            }
            else
            {
                //if not edge hanging, just jump
                playerMovement.Jump();
            }
        }
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        if(!playerMovement.GetEdgeClimbing())
        {
            //passes on the move vector to the movement script
            playerMovement.SetMoveInput(context.ReadValue<Vector2>());
        }
    }
}
