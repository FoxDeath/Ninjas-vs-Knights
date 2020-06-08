using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;


public class MouseLook : NetworkBehaviour
{
    private Transform cameraTransform;

    [SerializeField] Vector3 recoilRotation = new Vector3(2f, 2f, 4f);
    [SerializeField] Vector3 recoilRotationAiming = new Vector3(1f, 1f, 2.5f);
    private Vector3 currentRotation;
    private Vector3 rotation;

    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float returnSpeed = 20f;
    [HideInInspector] public float zRotation;
    public float mouseSensitivity = 2f;
    private float lookX;
    private float lookY;
    private float xRotation;

    private bool canLook;

    #region Getters and Setters

    public void SetCanLook(bool state)
    {
        canLook = state;
        lookX = 0f;
        lookY = 0f;
    }

    public void SetSensitivity(float sens)
    {
        mouseSensitivity = sens;
    }

    public float GetSensitivity()
    {
        return mouseSensitivity;
    }

    #endregion

    void Start()
    {
        cameraTransform = transform.Find("Main Camera");
        Cursor.lockState = CursorLockMode.Locked;
        canLook = true;
        InvokeRepeating("CalculateLook", 0f, 0.005f);
    }

    public void Fire(bool aiming)
    {
        if (aiming)
        {
            currentRotation += new Vector3(-recoilRotationAiming.x, Random.Range(-recoilRotationAiming.y, recoilRotationAiming.y), Random.Range(-recoilRotationAiming.z, recoilRotationAiming.z));
        }
        else
        {
            currentRotation += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
        }
    }

    private void CalculateLook()
    {
        //if game is paused
        if(GetComponentInChildren<PauseMenu>().GameIsPaused || !this.isLocalPlayer)
        {
            return;
        }

        //sets and restricts rotation value
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, zRotation);

        //applies rotation to player
        transform.Rotate(Vector2.up, lookX);

        //applies weapon recoil
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        rotation = Vector3.Slerp(rotation, currentRotation, rotationSpeed * Time.fixedDeltaTime);
        cameraTransform.Rotate(rotation.x, rotation.y, rotation.z, Space.Self);
    }

    //gets input from player
    public void LookInput(InputAction.CallbackContext context)
    {
        if(canLook)
        {
            Vector2 look = context.ReadValue<Vector2>() * 0.1f;
            lookX = look.x * mouseSensitivity;
            lookY = look.y * mouseSensitivity;
        }
    }
}
