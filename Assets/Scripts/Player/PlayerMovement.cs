using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
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
    protected float fallDecrease = 0.4f;
    protected float fallVelocity = 0f;
    protected float gravity = -9.8f;
    protected float groungDistance = 0.1f;
    protected float defaultSpeed;
    protected float horizontal;
    protected float vertical;

    protected bool isGrounded;
    protected bool crouching;
    protected bool isCrouched;
    protected bool edgeHanging;
    protected bool edgeClimbing;
    protected bool sliding;
    protected bool scoping;
    protected bool sprinting;
    protected bool moving;
    protected bool resetFall;
    protected bool canSlide = true;
    protected bool charging;

    private Animator animator;

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

    public void SetEdgeHanging(bool edgeHanging)
    {
        this.edgeHanging = edgeHanging;
    }
    
    public bool GetEdgeClimbing()
    {
        return edgeClimbing;
    }

    public void SetEdgeClimbing(bool edgeClimbing)
    {
        this.edgeClimbing = edgeClimbing;
    }

    public void ZeroVelocity()
    {
        velocity = Vector3.zero;
    }

    public bool GetSprinting()
    {
        return sprinting;
    }

    public bool GetMoving()
    {
        return moving;
    }

    public bool GetCanSlide()
    {
        return canSlide;
    }

    public bool GetCrouching()
    {
        return crouching;
    }

    public bool GetScoping()
    {
        return scoping;
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
        groundCheck = transform.Find("player").Find("body").Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");
        audioManager = FindObjectOfType<AudioManager>();
        animator = transform.Find("player").GetComponent<Animator>();

        defaultSpeed = speed;
        move = new Vector3();
        velocity = new Vector3();
    }

    protected virtual void Update()
    {
        if(!this.isLocalPlayer)
        {
            return;
        }

        //isGrounded is true if the groundCheck object is touching the Ground layer
        if(!isGrounded)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);

            if (isGrounded)
            {
                fallVelocity = 1f;
                audioManager.NetworkPlay("Falling");
            }
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);

        //if sprinting and moving backwards
        if(sprinting && vertical <= 0 && isGrounded && !edgeClimbing)
        {
            Sprint(false);
        }

        SpeedCalculation();
    }
   

    protected virtual void FixedUpdate()
    {
        //if game is paused
        if(!this.isLocalPlayer)
        {
            return;
        }

        //restricts the max vertical speed
        if(!isGrounded)
        {
            velocity.y = Mathf.Clamp(velocity.y, -40f, 15f);
        }
        else
        {
            velocity.y = Mathf.Clamp(velocity.y, 0f, 15f); 
        }

        if(edgeHanging)
        {
            //turns off gravity while hanging on edge
            velocity.y = 0f;
            fallVelocity = 0f;
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
        velocity.y = Mathf.Clamp(velocity.y, -40f, 15f);

        if(!edgeClimbing)
        {
            //applies gravity
            controller.Move(velocity * Time.deltaTime);
        }
    }

    //moves the player according to its state and attributes
    protected virtual void Move()
    {
        if(isGrounded)
        {
            move = (transform.right * horizontal + transform.forward * vertical) * speed;
            lastMove = move;

        }
        else
        {
            if(!edgeClimbing && !edgeHanging)
            {
                controller.Move(lastMove * 0.3f * Time.deltaTime);
                move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
            }

            if(edgeHanging && !edgeClimbing)
            {
                vertical = Mathf.Clamp(vertical, -1f, 0f);
                move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
            }

            if(!edgeClimbing)
            {
                move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
            }
        }
        controller.Move(move * Time.deltaTime);

    }

    //Calculates the speed depending on the situation
    protected virtual void SpeedCalculation()
    {
        if(sliding && sprinting)
        {
            speed = defaultSpeed * 2f;
        }
        else if(!sliding && sprinting)
        {
            speed = defaultSpeed * 1.6f;
        }
        else
        {
            if(isCrouched && !scoping)
            {
                speed = defaultSpeed * 0.6f;
            }
            else if(scoping)
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
        if(!isGrounded || Physics.Raycast(transform.position, Vector3.up, 5f) || charging)
        {
            return;
        }

        if(crouching)
        {
            isCrouched = true;
            animator.SetBool("crouch",true);
            print("yessss");
        }
        else if(!sliding)
        {
            isCrouched = false;
            animator.SetBool("crouch", false);
        }
    }

    protected void Fall()
    {
        if(velocity.y < fallVelocity && !isGrounded && !resetFall)
        {
            velocity.y -= fallDecrease;
            animator.SetBool("Fall", true);
        }
        else
        {
            resetFall = false;
            animator.SetBool("Fall", false);

        }
    }

    //gets input from PlayerInputSrcipt script
    public void SetMoveInput(Vector2 moveInput)
    {
        if(charging)
        {
            return;
        }
        
        this.moveInput = moveInput;
        horizontal = moveInput.x;
        vertical = moveInput.y;

        animator.SetFloat("velX", moveInput.x);
        animator.SetFloat("velY", moveInput.y);
    }

    protected virtual void MoveAudio()
    {
        if(moveInput != Vector2.zero && isGrounded)
        {
            if(!audioManager.IsPlaying("Walking"))
            {
                audioManager.NetworkPlay("Walking");
            }

            moving = true;
        }
        else
        {
            audioManager.NetworkStop("Walking");
            moving = false;
        }
    }

    public void Sprint(bool state)
    {
        //turn on sprint
        if(state && !isCrouched && !scoping && !sprinting)
        {
            audioManager.SetPitch("Walking", 2);
            sprinting = true;
            animator.SetBool("Running", true);
        }
        //turn off sprint
        else if(!state && sprinting)
        {
            audioManager.SetPitch("Walking", 1);
            sprinting = false;
            animator.SetBool("Running", false);
        }       
    }

    public virtual void Jump()
    {
        //Cant jump while crouching
        if(isCrouched)
        {
            return;
        }

        else if(isGrounded)
        {
            //if grounded play jump sound and move upwards
            audioManager.NetworkPlay("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            fallVelocity = 10f;
            animator.SetTrigger("Jump");

        }
        else if(isGrounded && sprinting)
            {
            animator.SetTrigger("RunJump");
        }
        
    }

    //pass reference references into functions... obviously lol xdd
    public ref Vector3 GetVelocityByReference()
    {
        return ref velocity;
    }

    public IEnumerator Sliding()
    {
        sliding = true;
        canSlide = false;

        transform.localScale = new Vector3(1f, 0.5f, 1f);

        yield return new WaitForSeconds(1f);

        if(!Physics.Raycast(transform.position, Vector3.up, 5f))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            isCrouched = true;
            Sprint(false);
        }

        sliding = false;

        yield return new WaitForSeconds(2f);

        canSlide = true;
    }
}
