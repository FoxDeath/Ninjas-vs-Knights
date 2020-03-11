using UnityEngine;
using UnityEngine.InputSystem;



public class WallRun : MonoBehaviour
{
    private PlayerMovementWallRun playerMovement;

    public float speed;


    private RaycastHit frontCast;
    private RaycastHit rightCast;
    private RaycastHit leftCast;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovementWallRun>();
    }

    void Update()
    {
        WallRuning();
    }

    void WallRuning()
    {
        Physics.Raycast(transform.position, transform.right, out rightCast, 1);
        Physics.Raycast(transform.position, -transform.right, out leftCast, 1);
        Physics.Raycast(transform.position, transform.forward, out frontCast, 1);


        if (rightCast.normal != Vector3.zero && rightCast.transform.tag == "Wall" && !playerMovement.isGrounded)
        {
            playerMovement.wallRun = true;
            doLeanRight();
        }
        else if (leftCast.normal != Vector3.zero && leftCast.transform.tag == "Wall" && !playerMovement.isGrounded)
        {
            playerMovement.wallRun = true;
            doLeanLeft();
        }
        else if (frontCast.normal != Vector3.zero && frontCast.transform.tag == "Wall" && !playerMovement.isGrounded)
        {
            playerMovement.wallRun = true;
        }
        else if (playerMovement.wallRun)
        {
            playerMovement.wallRun = false;
        }

    }
    void doLeanLeft()
    {
        Camera.main.transform.Rotate(new Vector3(0, 0, -17));
    }
    void doLeanRight()
    {
        Camera.main.transform.Rotate(new Vector3(0, 0, 17));
    }
}