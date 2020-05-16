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

    private bool canLook;

    #region Getters and Setters

    public void SetCanLook(bool state)
    {
        canLook = state;
        lookX = 0f;
        lookY = 0f;
    }

    public void SetSensitivity(float sens)
    {
        mouseSensitivity = sens;
    }

    public float GetSensitivity()
    {
        return mouseSensitivity;
    }

    #endregion

    void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        canLook = true;
        InvokeRepeating("CalculateLook", 0f, 0.005f);
    }

    private void CalculateLook()
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
        if(canLook)
        {
            Vector2 look = context.ReadValue<Vector2>() * 0.1f;
            lookX = look.x * mouseSensitivity;
            lookY = look.y * mouseSensitivity;
        }
    }
}
