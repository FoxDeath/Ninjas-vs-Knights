using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnightPlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Camera fpsCamera;
    private AudioManager audioManager;
    private PauseMenu pauseMenu;
    private Vector3 velocity;
    private Vector3 movement;
    private Vector3 lastMove;
    private Vector2 movementInput;

    [SerializeField] float jumpHeight;
    [SerializeField] float speed;
    private float defaultSpeed;
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
    private bool dashing;
    private bool sprinting;
    private bool jetPacking;
    

    private void Start()
    {
        Application.targetFrameRate = 60;
        movement = new Vector3();
        velocity = new Vector3();
        jetpackFuel = maxJetpackFuel;
        defaultSpeed = speed;

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
            audioManager.Play("Jump");
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
            
            if(jetPacking)
            {
                speed = speed / 1.5f;
            }
            
            jetPacking = false;
        }
    }
    
    void JetPackJump()
    {
        // Observer pattern
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
            if(!jetPacking)
            {
                speed = speed * 1.5f;
            }

            jetPacking = true;

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

            if(!dashing)
            {
                velocity.y = Mathf.Sqrt(jetpackForce * -2f * gravity * currentForce);
            }
        }
        else if(jetpackFuel <= 0f)
        {
            if(jetPacking)
            {
                speed = speed / 1.5f;
            }
            
            jetPacking = false;
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
            if (context.action.phase == InputActionPhase.Performed && !sprinting)
            {
                audioManager.SetPitch("Walking", audioManager.GetPitch("Walking")*2f);
                sprinting = true;
                speed *= 1.6f;
            }
            else if (context.action.phase == InputActionPhase.Canceled && sprinting)
            {
                sprinting = false;
                audioManager.SetPitch("Walking", audioManager.GetPitch("Walking")/2f);
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
        if(!jetpack && isGrounded)
        {
            canDash = false;
            dashing = true;
            SendMessage("SetSliderColour", Color.red);

            audioManager.Play("Jetpack Dash");

            float oldVertical = vertical;
            float oldHorizontal = horizontal;

            vertical = dashForce * movementInput.y;
            horizontal = dashForce * movementInput.x;

            yield return new WaitForSeconds(0.4f);
            
            dashing = false;

            vertical = oldVertical;
            horizontal = oldHorizontal;

            yield return new WaitForSeconds(2f);

            canDash = true;
            SendMessage("SetSliderColour", Color.green);
        }
        else if(!isGrounded)
        {
            canDash = false;
            dashing = true;

            SendMessage("SetSliderColour", Color.red);

            audioManager.Play("Jetpack Dash");

            float oldVerticalX = this.vertical;
            float oldVerticalY = this.horizontal;
            float oldHorizontal = this.velocity.y;
            float oldGravity = this.gravity;

            Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(fpsCamera.transform.forward);

            vertical = dashForce * localForward.z;
            horizontal = dashForce * localForward.x;

            gravity = 0f;
            velocity.y = dashForce * localForward.y;

            yield return new WaitForSeconds(0.4f);
            
            dashing = false;

            vertical = oldVerticalX;
            horizontal = oldVerticalY;
            velocity.y = oldHorizontal;
            gravity = oldGravity;

            yield return new WaitForSeconds(2f);

            canDash = true;
            SendMessage("SetSliderColour", Color.green);   
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

    public void Pause(InputAction.CallbackContext context)
    {
        pauseMenu.MenuInput();
    }
}
