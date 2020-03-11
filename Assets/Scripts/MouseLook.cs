using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform playerBody;

    [SerializeField] float mouseSensitivity;
    private float lookX;
    private float lookY;
    private float xRotation;
    public float zRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.GameIsPaused)
        {
            xRotation -= lookY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, zRotation);
        playerBody.Rotate(Vector2.up, lookX);
        }
    }

    public void LookInput(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>() * 0.1f;
        lookX = look.x * mouseSensitivity;
        lookY = look.y * mouseSensitivity;
    }
}
