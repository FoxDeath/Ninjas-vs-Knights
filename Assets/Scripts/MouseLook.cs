using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    public float mouseSensitivity;
    private float lookX;
    private float lookY;
    private float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector2.up, lookX);
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        lookX = look.x * mouseSensitivity * Time.deltaTime;
        lookY = look.y * mouseSensitivity * Time.deltaTime;
    }
}
