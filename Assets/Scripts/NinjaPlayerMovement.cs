using System.Collections;
using UnityEngine;


public class NinjaPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Transform groundCheck;
    private LayerMask groundMask;
    private AudioManager audioManager;
    private Vector3 velocity;
    private Vector3 move;
    private Vector3 lastMove;
    private Vector2 moveInput;
    private Vector3 normal;

    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float speed = 10f;
    [SerializeField] float fallDecrease = 2f;
    [SerializeField] float wallJumpForce = 20f;
    private float gravity = -25f;
    private float groungDistance = 0.4f;
    private float defaultSpeed;
    private float horizontal;
    private float vertical;
    private float defaultSpeed;

    public bool wallRunning;
    private bool isCrouched;
    public bool isGrounded;
    public bool edgeHanging;
    public bool edgeClimbing;
    private bool doubleJumped;
    private bool resetFall;
    private bool sliding;
    private bool crouching;
    private bool sprinting;
    private bool canWallJump;
    private bool wallJumping;
    private bool scoping;

    #region Getters and Setters
    public float GetVertical()
    {
        return vertical;
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
    #endregion

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = transform.Find("Cylinder").Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");
        audioManager = FindObjectOfType<AudioManager>();

        defaultSpeed = speed;
        move = new Vector3();
        velocity = new Vector3();

        defaultSpeed = speed;
    }

    void Update()
    {
        //isGrounded is true if the groundCheck object is touching the Ground layer
        if(!isGrounded)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);
            if(isGrounded)
            {
                audioManager.Play("Falling");
            }
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);

        //if sprinting and moving backwards
        if(sprinting && vertical <= 0)
        {
            Sprint(false);
        }

        SpeedCalculation();
    }

    void FixedUpdate()
    {
        //if game is paused
        if(PauseMenu.GameIsPaused)
        {
            return;
        }

        if(edgeHanging)
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

        //restricts the max vertical speed
        velocity.y = Mathf.Clamp(velocity.y, -25f, 15f);

        if(!edgeClimbing)
        {
            //applies gravity
            controller.Move(velocity * Time.deltaTime);
        }

        if(wallRunning)
        {
            if(!canWallJump)
            {
                //sets vertical pull when starting wall running
                velocity.y = 10f;
            }

            //sets vertical pull attributes for wall running
            gravity = 0f;
            velocity.y -= 0.25f;
            fallDecrease = 0.1f;
            canWallJump = true;
        }
        else
        {
            //sets vertical pull attributes back to default
            gravity = -25f;
            fallDecrease = 0.4f;
            canWallJump = false;
        }

        if(velocity.x > 0f)
        {
            velocity.x -= wallJumpForce * Time.deltaTime;
        }
        else if(velocity.x < 0f)
        {
            velocity.x += wallJumpForce * Time.deltaTime;
        }

        if(velocity.z > 0f)
        {
            velocity.z -= wallJumpForce * Time.deltaTime;
        }
        else if(velocity.z < 0f)
        {
            velocity.z += wallJumpForce * Time.deltaTime;
        }
    }

    private void SpeedCalculation()
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
            else if (crouching && scoping)
            {
                speed = defaultSpeed * 0.4f;
            }
            else if (!crouching && scoping)
            {
                speed = defaultSpeed * 0.4f;
            }
            else
            {
                speed = defaultSpeed;
            }
        }
    }

    //gets input from NinjaPlayerInput script
    public void SetMoveInput(Vector2 moveInput)
    {
        horizontal = moveInput.x;
        vertical = moveInput.y;
    }
    
    //moves the player according to its state and attributes
    private void Move()
    {
        if(isGrounded && !wallJumping)
        {
            doubleJumped = false;
            move = (transform.right * horizontal + transform.forward * vertical) * speed;
            lastMove = move;
        }
        else if(!wallJumping)
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

    public void Jump()
    {
        //Cant jump while crouching
        if(isCrouched)
        {
            return;
        }

        if(canWallJump && !Physics.Raycast(transform.position, transform.forward, 1.5f))
        {
            //if able to wall jump and parallel t the wall, start wall jump
            StartCoroutine(WallJump());
        }
        else if(isGrounded)
        {
            //if grounded play jump sound and move upwards
            audioManager.Play("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if(!isGrounded && !doubleJumped && !edgeClimbing && !wallRunning)
        {
            //if in air and didn't double jump yet, jump again
            audioManager.Play("Jump");
            doubleJumped = true;
            resetFall = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2.4f * gravity);
        }
    }

    public void Sprint(bool state)
    {
        //turn on sprint
        if(state && !sprinting && !scoping)
        {
            audioManager.SetPitch("Walking", 2);
            sprinting = true;
            Crouch(false);
        }
        //turn off sprint
        else if(!state && sprinting)
        {
            audioManager.SetPitch("Walking", 1);
            sprinting = false;
        }
    }
    
    private void Fall()
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

    public void Crouch()
    {
        //You can't crouch if you are not on the ground or if there is something above you
        if(!isGrounded || Physics.Raycast(transform.position, Vector3.up, 5f))
        {
            return;
        }

        if(crouching)
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

    private void MoveAudio()
    {
        if(moveInput != Vector2.zero && isGrounded)
        {
            if(!audioManager.IsPlaying("Walking"))
            {
                audioManager.Play("Walking");
            }
        }
        else
        {
            audioManager.Stop("Walking");
        }

        if(moveInput != Vector2.zero && wallRunning)
        {
            if(!audioManager.IsPlaying("Wallrun"))
            {
                audioManager.Play("Wallrun");
            }
        }
        else
        {
            audioManager.Stop("Wallrun");
        }
    }

    public void SetScoping(bool scoping)
    {
        this.scoping = scoping;
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

    IEnumerator WallJump()
    {
        if(!doubleJumped)
        {
            doubleJumped = true;
        }

        wallJumping = true;
        velocity.x = wallJumpForce * normal.x;
        velocity.z = wallJumpForce * normal.z;
        velocity.y = Mathf.Sqrt(jumpHeight * 50f);
        move.x = lastMove.x;

        yield return new WaitForSeconds(0.5f);

        wallJumping = false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        normal = hit.normal;
    }
}
