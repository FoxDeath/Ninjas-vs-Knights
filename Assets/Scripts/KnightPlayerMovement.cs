using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnightPlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    private AudioManager audioManager;
    private Vector3 velocity;
    private Vector3 movement;
    private Vector3 lastMove;
    private Vector2 movementInput;

    [SerializeField] float jumpHeight;
    [SerializeField] float speed;
    [SerializeField] float dashForce;
    [SerializeField] float jetpackForce;
    [SerializeField] float maxJetpackFuel = 5f; 
    [SerializeField] float fallDecrease = 0.8f;
    private float horizontal;
    private float vertical;
    private float gravity = -25f;
    private float fallMultiplier = 2.5f;
    private float groungDistance = 0.4f;
    private float currentForce = 0f;
    private float jetpackFuel;

    private bool isGrounded;
    private bool resetFall;
    private bool jetpack;
    private bool canDash = true;
    

    private void Start()
    {
        Application.targetFrameRate = 60;
        movement = new Vector3();
        velocity = new Vector3();
        jetpackFuel = maxJetpackFuel;

        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groungDistance, groundMask);
    }

    void FixedUpdate()
    {
        velocity.y += gravity * Time.deltaTime;

        Move();
        MoveAudio();

        Fall();

        JetPackJump();

        velocity.y = Mathf.Clamp(velocity.y, -25f, 10f);
        controller.Move(velocity * Time.deltaTime);
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        horizontal = movementInput.x;
        vertical = movementInput.y;
    }
    
    private void Move()
    {
        if (isGrounded)
        {
            movement = (transform.right * horizontal + transform.forward * vertical) * speed;
            lastMove = movement;
        }
        else
        {
            controller.Move(lastMove * 0.3f * Time.deltaTime);
            movement = (transform.right * horizontal + transform.forward * vertical) * speed * 0.8f;
        }

        controller.Move(movement * Time.deltaTime);
    }

    private void MoveAudio()
    {
        if(movementInput != Vector2.zero && isGrounded)
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
    }

    public void JetPackInput(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed)
        {
            jetpack = true;
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            jetpack = false;
        }
    }
    
    void JetPackJump()
    {
        SendMessage("SetSliderValue", jetpackFuel/maxJetpackFuel);
        if(!jetpack)
        {
            audioManager.Stop("Jetpack");
            if(isGrounded && jetpackFuel < maxJetpackFuel)
            {
                jetpackFuel += Time.deltaTime * 2;
            }
            currentForce -= Time.deltaTime/5f;    
            if(currentForce < 0f)
            {
                currentForce = 0f;
            }
            return;
        }
        else if(jetpackFuel > 0f)
        {
            if(!audioManager.IsPlaying("Jetpack"))
            {
                audioManager.Play("Jetpack");
            }
        jetpackFuel -= Time.deltaTime;
        currentForce += Time.deltaTime/10f;
            if(currentForce > 1f)
            {
                currentForce = 1f;
            }
        velocity.y = Mathf.Sqrt(jetpackForce * -2f * gravity * currentForce);
        }
        else if(jetpackFuel <= 0f)
        {
            if(audioManager.IsPlaying("Jetpack"))
            {
                audioManager.Stop("Jetpack");
            }
        }
    }

    public void SprintInput(InputAction.CallbackContext context)
    {
        if (vertical > 0)
        {
            if (context.action.phase == InputActionPhase.Performed)
            {
                audioManager.SetPitch("Walking", 2);
                speed *= 1.6f;
            }
            else if (context.action.phase == InputActionPhase.Canceled)
            {
                audioManager.SetPitch("Walking", 1);
                speed /= 1.6f;
            }
        }
    }

    public void JetPackDashInput(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed && canDash)
        {
            StartCoroutine("Dash");
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        SendMessage("SetSliderColour", Color.red);

        audioManager.Play("Jetpack Dash");

        float oldVertical = vertical;
        float oldHorizontal = horizontal;

        vertical = dashForce * movementInput.y;
        horizontal = dashForce * movementInput.x;

        yield return new WaitForSeconds(0.2f);
        
        vertical = oldVertical;
        horizontal = oldHorizontal;

        yield return new WaitForSeconds(2f);

        canDash = true;
        SendMessage("SetSliderColour", Color.green);
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
