using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NinjaPlayerMovement : MonoBehaviour
{
    [SerializeField]
    public CharacterController controller;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    private EdgeClimb edgeClimb;
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

    public bool isGrounded;
    private bool doubleJumped;
    private bool resetFall;
    private bool edgeHanging;
    private bool edgeClimbing;
    private bool sliding;
    private bool crouching;
    private bool sprinting;

    private void Start()
    {
        edgeClimb = GetComponent<EdgeClimb>();

        move = new Vector3();
        velocity = new Vector3();

        edgeHanging = false;
        edgeClimbing = false;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);
    }

    void FixedUpdate()
    {
        if (edgeHanging)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Move();

        Fall();

        velocity.y = Mathf.Clamp(velocity.y, -15f, 15f);

        if (!edgeClimbing)
        {
            controller.Move(velocity * Time.deltaTime);
        }
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
            if (isGrounded)
            {
                Jump();
            }
            else if (edgeHanging)
            {
                edgeClimb.StartEdgeClimb();
            }
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
                sprinting = true;
                speed *= 1.6f;
            }
            else if (context.action.phase == InputActionPhase.Canceled)
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

    public void SetEdgeClimbing(bool edgeClimbing)
    {
        this.edgeClimbing = edgeClimbing;
    }

    public bool GetEdgeClimbing()
    {
        return edgeClimbing;
    }

    public void SetEdgeHanging(bool edgeHanging)
    {
        this.edgeHanging = edgeHanging;
    }

    public bool GetEdgeHanging()
    {
        return edgeHanging;
    }
}
