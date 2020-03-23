using UnityEngine;
using UnityEngine.InputSystem;

public class KightPlayerInput : MonoBehaviour
{
    private KnightPlayerMovement playerMovement;
    private PauseMenu pauseMenu;

    void Start()
    {
        playerMovement = GetComponent<KnightPlayerMovement>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        playerMovement.SetMoveInput(context.ReadValue<Vector2>());
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            playerMovement.Jump();
        }
    }
    
    public void JetPackInput(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed)
        {
            playerMovement.jetpackOn = true;
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            playerMovement.jetpackOn = false;
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
        if (context.action.phase == InputActionPhase.Started && !playerMovement.GetSprinting())
        {
            playerMovement.SetCrouching(true);
            
        }
        else if (context.action.phase == InputActionPhase.Canceled && playerMovement.GetCrouching())
        {
            playerMovement.SetCrouching(false);
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
