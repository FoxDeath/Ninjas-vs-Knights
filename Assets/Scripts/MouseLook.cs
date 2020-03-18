using UnityEngine;
using UnityEngine.InputSystem;


public class MouseLook : MonoBehaviour
{
    private Transform playerBody;

    [HideInInspector] public float zRotation;
    public float mouseSensitivity = 2f;
    private float lookX;
    private float lookY;
    private float xRotation;
    
    void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //if game is not paused
        if(!PauseMenu.GameIsPaused)
        {
            //sets and restricts rotation value
            xRotation -= lookY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, zRotation);

            //applies rotation to player
            playerBody.Rotate(Vector2.up, lookX);
        }
    }

    //gets input from player
    public void LookInput(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>() * 0.1f;
        lookX = look.x * mouseSensitivity;
        lookY = look.y * mouseSensitivity;
    }
}
