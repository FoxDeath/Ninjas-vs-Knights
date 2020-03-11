using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity;
    private Vector3 move;
    private Vector3 lastMove;

    public float jumpHeight;
    public float speed;
    public float fallDecrease;
    private float horizontal;
    private float vertical;
    private float gravity = -25f;
    private float groungDistance = 0.4f;
    private float seconds = 0f;


    public bool isGrounded;
    public bool doubleJumped;
    public bool resetFall;
    public bool sprinting;
    public bool crouching;
    public bool sliding;

    private RaycastHit frontCast;
    private RaycastHit rightCast;
    private RaycastHit leftCast;
    bool canWallJump;
    bool wallRun;
  
    Vector3 normal;


    private void Start()
    {
        move = new Vector3();
        velocity = new Vector3();
    }

    void Update()
    {
        if (sliding)
        {
            seconds += Time.deltaTime;
            if (seconds >= 0.8f)
            {
                seconds = 0f;
                controller.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                sliding = false;
            }
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);
        move = (transform.right * horizontal + transform.forward * vertical) * speed;

        if (isGrounded)
        {
            doubleJumped = false;
            move = (transform.right * horizontal + transform.forward * vertical) * speed;
            wallRun = false;

        }
        else
        {
            controller.Move(lastMove * 0.4f * Time.deltaTime);
            move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
            lastMove = move;
        }

        controller.Move(move * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        if (velocity.y < 0 && !isGrounded && resetFall && !GetComponent<GrapplingHookMovement>().isHooked)
        {
            velocity.y -= fallDecrease;
        }
        else
        {
            resetFall = false;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10f, 15f);
        velocity.x = Mathf.Clamp(velocity.x, -15f, 15f);
        velocity.z = Mathf.Clamp(velocity.z, -15f, 15f);

        controller.Move(velocity * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!GetComponent<GrapplingHookMovement>().isHooked)
        {
            velocity.x = 0;
            velocity.z = 0;
        }
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
            else if (!isGrounded && !doubleJumped && !crouching)
            {
                doubleJumped = true;
                resetFall = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -2.4f * gravity);
            }
        }
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

    public void Sprint(InputAction.CallbackContext context)
    {
        if (vertical > 0)
        {
            if (context.action.phase == InputActionPhase.Started)
            {
                speed *= 1.6f;
            }
            else if (context.action.phase == InputActionPhase.Canceled)
            {
                speed /= 1.6f;
            }
        }
    }

    void WallSkeet()
    {
        Physics.Raycast(transform.position, transform.right, out rightCast, 1);
        Physics.Raycast(transform.position, -transform.right, out leftCast, 1);
        Physics.Raycast(transform.position, transform.forward, out frontCast, 1);


        if (rightCast.normal != Vector3.zero && rightCast.transform.tag == "Wall")
        {
            if (context.action.phase == InputActionPhase.Started && !crouching)
            {
                sprinting = true;
                speed *= 1.6f;
            }
            else if (context.action.phase == InputActionPhase.Canceled && sprinting)
            {
                speed /= 1.6f;
                sprinting = false;
            }
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            wallJumped = false;
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
            speed *= 1.2f;
            sliding = true;
            transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
        else if (context.action.phase == InputActionPhase.Canceled && sliding)
        {
            speed /= 1.2f;
            seconds = 0f;
            transform.localScale = new Vector3(1f, 1f, 1f);
            sliding = false;
        }
    }

    public ref Vector3 GetVelocityByReference()
    {
        return ref velocity;
    }
}
    