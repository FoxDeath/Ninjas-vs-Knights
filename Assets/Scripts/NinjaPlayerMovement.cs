using UnityEngine;
using UnityEngine.InputSystem;

public class NinjaPlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    private Vector3 velocity;
    private Vector3 move;
    private Vector3 lastMove;

    [SerializeField] float jumpHeight;
    [SerializeField] float speed;
    [SerializeField] float fallDecrease;
    private float horizontal;
    private float vertical;
    private float gravity = -11f;
    private float groungDistance = 0.4f;

    private bool isGrounded;
    private bool doubleJumped;
    private bool resetFall;
    public bool edgeHanging;

    private void Start()
    {
        move = new Vector3();
        velocity = new Vector3();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);
    }

    void FixedUpdate()
    {
        if(edgeHanging)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Move();

        Fall();

        velocity.y = Mathf.Clamp(velocity.y, -15f, 10f);
        controller.Move(velocity * Time.deltaTime);
    }


    public void MoveInput(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        horizontal = move.x;
        vertical = move.y;
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
            controller.Move(lastMove * 0.3f * Time.deltaTime);
            move = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
        }

        controller.Move(move * Time.deltaTime);
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            Jump();
        }
    }

    private void Jump()
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

    public void SprintInput(InputAction.CallbackContext context)
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
}
