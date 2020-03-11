using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovementWallRun : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity;
    private Vector3 move;
    private Vector3 lastMove;

    public float jumpHeight;
    public float speed;
    public float wallJupmForce;
    public float fallDecrease;
    private float horizontal;
    private float vertical;
    private float gravity = -25f;
    private float groungDistance = 0.4f;


    public bool isGrounded;
    public bool doubleJumped;
    public bool resetFall;
    bool canWallJump;
    public bool wallRun;
  
    Vector3 normal;


    private void Start()
    {
        move = new Vector3();
        velocity = new Vector3();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);

        if (isGrounded)
        {
            velocity.x = 0f;
            doubleJumped = false;
            move = (transform.right * horizontal + transform.forward * vertical) * speed;
            wallRun = false;

        }
        else if(!isGrounded && !wallRun)
        {
            controller.Move(lastMove * 0.4f * Time.deltaTime);
            move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
        }
        else if(!isGrounded && wallRun)
        {
            controller.Move(lastMove * 0.4f * Time.deltaTime);
            move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
            move = lastMove;
        }

        controller.Move(move * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        if (velocity.y < 0 && !isGrounded && !resetFall)
        {
            velocity.y -= fallDecrease;
        }
        else
        {
            resetFall = false;
        }

        velocity.y = Mathf.Clamp(velocity.y, -25f, 10f);
        controller.Move(velocity * Time.deltaTime);
        lastMove = move;

        if (wallRun == true)
        {
            gravity = -5.5f;
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
            // wall jump
            if (canWallJump)
            {
                velocity.x = wallJupmForce * normal.x;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (isGrounded)
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        normal = hit.normal;
    }
}
    