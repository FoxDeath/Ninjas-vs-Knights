using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NinjaPlayerMovement : MonoBehaviour
{
    [SerializeField] public CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private PauseMenu pauseMenu;
    private AudioManager audioManager;
    private EdgeClimb edgeClimb;
    private Vector3 velocity;
    private Vector3 move;
    private Vector3 lastMove;
    private Vector2 moveInput;
    private Vector2 normal;

    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float speed = 10f;
    [SerializeField] float fallDecrease = 2f;
    [SerializeField] float wallJupmForce = 20f;
    private float horizontal;
    private float vertical;
    private float gravity = -25f;
    private float groungDistance = 0.4f;

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool edgeHanging;
    [HideInInspector] public bool edgeClimbing;
    private bool doubleJumped;
    private bool resetFall;
    private bool sliding;
    private bool crouching;
    private bool sprinting;
    private bool canWallJump;
    public bool wallRun;

    private void Start()
    {
        edgeClimb = GetComponent<EdgeClimb>();

        move = new Vector3();
        velocity = new Vector3();
        
        audioManager = FindObjectOfType<AudioManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if(!isGrounded)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);
            if(isGrounded)
            {
                audioManager.Play("Falling");
            }
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);
    }

    void FixedUpdate()
    {
        if(PauseMenu.GameIsPaused)
        {
            return;
        }
        if (edgeHanging)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Move();
        MoveAudio();

        Fall();

        velocity.y = Mathf.Clamp(velocity.y, -25f, 15f);

        if (!edgeClimbing)
        {
            controller.Move(velocity * Time.deltaTime);
        }

        if (wallRun)
        {
            gravity = -17f;
            fallDecrease = 0.1f;
            canWallJump = true;
            doubleJumped = false;
        }
        else
        {
            gravity = -25f;
            fallDecrease = 0.4f;
            canWallJump = false;
        }

        if(velocity.x > 0f)
        {
            velocity.x -= wallJupmForce * Time.deltaTime;
        }
        else if(velocity.x < 0f)
        {
            velocity.x += wallJupmForce * Time.deltaTime;
        }
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        horizontal = moveInput.x;
        vertical = moveInput.y;
    }
    
    private void Move()
    {
        if (isGrounded)
        {
            doubleJumped = false;
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

        if(moveInput != Vector2.zero && wallRun)
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

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            if (edgeHanging)
            {
                edgeClimb.StartEdgeClimb();
            }
            else
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        if (canWallJump)
        {
            velocity.x = wallJupmForce * normal.x;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (isGrounded)
        {
            audioManager.Play("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (!isGrounded && !doubleJumped && !edgeClimbing)
        {
            audioManager.Play("Jump");
            doubleJumped = true;
            resetFall = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2.4f * gravity);
        }
    }

    public void SprintInput(InputAction.CallbackContext context)
    {
        if (vertical > 0)
        {
            if (context.action.phase == InputActionPhase.Started && !sprinting)
            {
                sprinting = true;
                speed *= 1.6f;
            }
            else if (context.action.phase == InputActionPhase.Canceled && sprinting)
            {
                sprinting = false;
                speed /= 1.6f;
            }
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

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started && !sprinting)
        {
            crouching = true;
            transform.localScale = new Vector3(1f, 0.5f, 1f);
            speed *= 0.6f;

        }
        else if (context.action.phase == InputActionPhase.Canceled && crouching)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            speed /= 0.6f;
            crouching = false;
        }
        else if (context.action.phase == InputActionPhase.Started && sprinting)
        {
            StartCoroutine("Sliding");
        }
        else if (context.action.phase == InputActionPhase.Canceled && sliding)
        {
            EndSliding();
        }
    }

    private void EndSliding()
    {
        speed /= 1.2f;
        transform.localScale = new Vector3(1f, 1f, 1f);
        sliding = false;
    }

    IEnumerator Sliding()
    {
        speed *= 1.2f;
        sliding = true;
        transform.localScale = new Vector3(1f, 0.5f, 1f);
        yield return new WaitForSeconds(0.8f);
        EndSliding();
    }

    public ref Vector3 GetVelocityByReference()
    {
        return ref velocity;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        pauseMenu.MenuInput();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        normal = hit.normal;
    }
}
