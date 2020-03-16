using UnityEngine;
using UnityEngine.InputSystem;



public class WallRun : MonoBehaviour
{
    private NinjaPlayerMovement playerMovement;
    private MouseLook mouseLook;


    private RaycastHit frontCast;
    private RaycastHit rightCast;
    private RaycastHit leftCast;

    private void Start()
    {
        playerMovement = GetComponent<NinjaPlayerMovement>();
        mouseLook = GetComponentInChildren<MouseLook>();
    }

    void Update()
    {
        WallRuning();
    }

    void WallRuning()
    {
        // Right now you can climb up the walls by switching between looking right and left
        Physics.Raycast(transform.position, transform.right, out rightCast, 1);
        Physics.Raycast(transform.position, -transform.right, out leftCast, 1);
        Physics.Raycast(transform.position, transform.forward, out frontCast, 1);


        if (rightCast.normal != Vector3.zero && rightCast.transform.tag == "Wall" && !playerMovement.isGrounded)
        {
            playerMovement.wallRun = true;
            mouseLook.zRotation = 17f;
        }
        else if (leftCast.normal != Vector3.zero && leftCast.transform.tag == "Wall" && !playerMovement.isGrounded)
        {
            playerMovement.wallRun = true;
            mouseLook.zRotation = -17f;
        }
        // For climbing walls while looking at them
        else if (frontCast.normal != Vector3.zero && frontCast.transform.tag == "Wall" && !playerMovement.isGrounded)
        {
            playerMovement.wallRun = true;
            mouseLook.zRotation = 0f;
        }
        else if (playerMovement.wallRun)
        {
            playerMovement.wallRun = false;
            mouseLook.zRotation = 0f;
        }
    }
}