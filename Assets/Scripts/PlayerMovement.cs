﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    protected CharacterController controller;
    protected AudioManager audioManager;
    protected LayerMask groundMask;
    protected Transform groundCheck;

    protected Vector3 velocity;
    protected Vector3 move;
    protected Vector3 lastMove;

    protected Vector2 moveInput;

    [SerializeField] protected float jumpHeight = 5f;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float fallDecrease = 2f;
    protected float gravity = -25f;
    protected float groungDistance = 0.4f;
    protected float defaultSpeed;
    protected float horizontal;
    protected float vertical;

    protected bool isGrounded;
    protected bool crouching;
    protected bool isCrouched;
    public bool edgeHanging;
    public bool edgeClimbing;
    protected bool sliding;
    protected bool scoping;
    protected bool sprinting;
    protected bool resetFall;

    #region Getters and Setters
    public CharacterController GetController() 
    {
        return controller;
    }
    public float GetVertical()
    {
        return vertical;
    }
    public bool GetGrounded()
    {
        return isGrounded;
    }
    public bool GetEdgeHanging()
    {
        return edgeHanging;
    }

    public void ZeroVelocity()
    {
        velocity = Vector3.zero;
    }

    public bool GetSprinting()
    {
        return sprinting;
    }

    public bool GetSliding()
    {
        return sliding;
    }

    public bool GetCrouching()
    {
        return crouching;
    }
    public void SetCrouching(bool crouching)
    {
        this.crouching = crouching;
    }
    public void SetScoping(bool scoping)
    {
        this.scoping = scoping;
    }
    #endregion

    protected virtual void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = transform.Find("Cylinder").Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");
        audioManager = FindObjectOfType<AudioManager>();

        defaultSpeed = speed;
        move = new Vector3();
        velocity = new Vector3();
    }

    protected virtual void Update()
    {
        //isGrounded is true if the groundCheck object is touching the Ground layer
        if (!isGrounded)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);
            if (isGrounded)
            {
                audioManager.Play("Falling");
            }
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);

        //if sprinting and moving backwards
        if (sprinting && vertical <= 0)
        {
            Sprint(false);
        }

        SpeedCalculation();
    }

    protected virtual void FixedUpdate()
    {
        //if game is paused
        if (PauseMenu.GameIsPaused)
        {
            return;
        }

        if (edgeHanging)
        {
            //turns off gravity while hanging on edge
            velocity.y = 0f;
        }
        else
        {
            //sets vertical pull
            velocity.y += gravity * Time.deltaTime;
        }

        Move();
        MoveAudio();
        Fall();
        Crouch();

        //restricts the max vertical speed
        velocity.y = Mathf.Clamp(velocity.y, -25f, 15f);

        if (!edgeClimbing)
        {
            //applies gravity
            controller.Move(velocity * Time.deltaTime);
        }
    }

    //moves the player according to its state and attributes
    protected virtual void Move()
    {
        if (isGrounded)
        {
            move = (transform.right * horizontal + transform.forward * vertical) * speed;
            lastMove = move;
        }
        else
        {
            if (!edgeClimbing && !edgeHanging)
            {
                controller.Move(lastMove * 0.3f * Time.deltaTime);
                move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
            }

            if (edgeHanging && !edgeClimbing)
            {
                vertical = Mathf.Clamp(vertical, -1f, 0f);
                move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
            }

            if (!edgeClimbing)
            {
                move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
            }
        }
        controller.Move(move * Time.deltaTime);
    }

    //Calculates the speed depending on the situation
    protected void SpeedCalculation()
    {
        if (sprinting)
        {
            speed = defaultSpeed * 1.6f;
        }
        else
        {
            if (crouching && !scoping)
            {
                speed = defaultSpeed * 0.6f;
            }
            else if (scoping)
            {
                speed = defaultSpeed * 0.4f;
            }
            else
            {
                speed = defaultSpeed;
            }
        }
    }

    public void Crouch()
    {
        //You can't crouch if you are not on the ground or if there is something above you
        if (!isGrounded || Physics.Raycast(transform.position, Vector3.up, 5f))
        {
            return;
        }

        if (crouching)
        {
            isCrouched = true;
            transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
        else
        {
            isCrouched = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    protected void Fall()
    {
        if (velocity.y < 0 && !isGrounded && !resetFall)
        {
            velocity.y -= fallDecrease;
        }
        else
        {
            resetFall = false;
        }
    }

    //gets input from PlayerInputSrcipt script
    public void SetMoveInput(Vector2 moveInput)
    {
        this.moveInput = moveInput;
        horizontal = moveInput.x;
        vertical = moveInput.y;
    }

    protected virtual void MoveAudio()
    {
        if (moveInput != Vector2.zero && isGrounded)
        {
            if (!audioManager.IsPlaying("Walking"))
            {
                audioManager.Play("Walking");
            }
        }
        else
        {
            audioManager.Stop("Walking");
        }
    }

    public void Sprint(bool state)
    {
        //turn on sprint
        if (state && !crouching && !scoping && !sprinting)
        {
            audioManager.SetPitch("Walking", 2);
            sprinting = true;
        }
        //turn off sprint
        else if (!state && sprinting)
        {
            audioManager.SetPitch("Walking", 1);
            sprinting = false;
        }
    }

    public virtual void Jump()
    {
        //Cant jump while crouching
        if (isCrouched)
        {
            return;
        }

        else if (isGrounded)
        {
            //if grounded play jump sound and move upwards
            audioManager.Play("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    //pass reference references into functions... obviously lol xdd
    public ref Vector3 GetVelocityByReference()
    {
        return ref velocity;
    }

    public IEnumerator Sliding()
    {
        speed = defaultSpeed * 1.2f;
        sliding = true;
        transform.localScale = new Vector3(1f, 0.5f, 1f);

        yield return new WaitForSeconds(0.8f);

        speed = defaultSpeed;
        transform.localScale = new Vector3(1f, 1f, 1f);

        yield return new WaitForSeconds(1.5f);

        sliding = false;
    }
}
