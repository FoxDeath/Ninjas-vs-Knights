using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity;

    public float jumpHeight;
    public float speed;
    private float horizontal;
    private float vertical;
    private float gravity = -9.81f;
    private float fallMultiplier = 1.15f;
    private float groungDistance = 0.4f;

    public bool isGrounded;
    public bool doubleJumped;
    public bool resetFall;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            doubleJumped = false;
        }

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        if (velocity.y < 0 && !isGrounded && !resetFall)
        {
            velocity.y *= fallMultiplier;
        }
        else
        {
            resetFall = false;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10f, 10f);
        controller.Move(velocity * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        horizontal = move.x;
        vertical = move.y;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (!isGrounded && !doubleJumped)
            {
                doubleJumped = true;
                resetFall = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -2.4f * gravity);
            }
        }
    }
}
