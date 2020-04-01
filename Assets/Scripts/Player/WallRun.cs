using UnityEngine;
using System.Collections;


public class WallRun : MonoBehaviour
{
    private NinjaPlayerMovement playerMovement;
    private MouseLook mouseLook;


    private RaycastHit frontCast;
    private RaycastHit frontLeftCast;
    private RaycastHit frontRightCast;
    private RaycastHit rightCast;
    private RaycastHit leftCast;
    private RaycastHit backCast;
    private RaycastHit backLeftCast;
    private RaycastHit backRightCast;
    
    private bool canWallRun = true;
    private bool reseting;
    private int side = 0;
    private int currentWallID;

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
        //Raycast calculcations for right and left casts are done here because they are needed in both statements
        Physics.Raycast(transform.position, transform.right, out rightCast, 0.7f);
        Physics.Raycast(transform.position, -transform.right, out leftCast, 0.7f);


        if(!playerMovement.wallRunning && !playerMovement.GetGrounded() && canWallRun)
        {
            //FrontLeft and FrontRight raycast calculations with smaller size for detecting the walls
            Physics.Raycast(transform.position, transform.forward + -transform.right, out frontLeftCast, 0.8f);
            Physics.Raycast(transform.position, transform.forward + transform.right, out frontRightCast, 0.8f);

            //Detecting if you colide with a wall on the right side of the player, either directily right or front right
            if (rightCast.normal != Vector3.zero && rightCast.transform.tag == "Wall" && rightCast.transform.gameObject.GetInstanceID() != currentWallID)
            {
                playerMovement.wallRunning = true;
                playerMovement.runningVector = Vector3.Cross(-rightCast.normal, Vector3.up);
                playerMovement.normal = rightCast.normal;
                mouseLook.zRotation = 17f;
                side = 1;
                currentWallID = rightCast.transform.gameObject.GetInstanceID();
            }
            else if (frontRightCast.normal != Vector3.zero && frontRightCast.transform.tag == "Wall" && frontRightCast.transform.gameObject.GetInstanceID() != currentWallID)
            {
                playerMovement.wallRunning = true;
                playerMovement.runningVector = Vector3.Cross(-frontRightCast.normal, Vector3.up);
                playerMovement.normal = frontRightCast.normal;
                mouseLook.zRotation = 17f;
                side = 1;
                currentWallID = frontRightCast.transform.gameObject.GetInstanceID();
            }
            //Detecting if you colide with a wall on the left side of the player, either directily left or front left
            else if (leftCast.normal != Vector3.zero && leftCast.transform.tag == "Wall" && leftCast.transform.gameObject.GetInstanceID() != currentWallID)
            {
                playerMovement.wallRunning = true;
                playerMovement.runningVector = Vector3.Cross(leftCast.normal, Vector3.up);
                playerMovement.normal = leftCast.normal;
                mouseLook.zRotation = -17f;
                side = -1;
                currentWallID = leftCast.transform.gameObject.GetInstanceID();
            }
            else if (frontLeftCast.normal != Vector3.zero && frontLeftCast.transform.tag == "Wall" && frontLeftCast.transform.gameObject.GetInstanceID() != currentWallID)
            {
                playerMovement.wallRunning = true;
                playerMovement.runningVector = Vector3.Cross(frontLeftCast.normal, Vector3.up);
                playerMovement.normal = frontLeftCast.normal;
                mouseLook.zRotation = -17f;
                side = -1;
                currentWallID = frontLeftCast.transform.gameObject.GetInstanceID();
            }
        }
        else if(playerMovement.wallRunning)
        {
            //Raycast calculations for detaching from the wall after you are already running on it
            Physics.Raycast(transform.position, transform.forward, out frontCast, 0.8f);
            Physics.Raycast(transform.position, transform.forward + -transform.right, out frontLeftCast, 1.2f);
            Physics.Raycast(transform.position, transform.forward + transform.right, out frontRightCast, 1.2f);
            Physics.Raycast(transform.position, -transform.forward, out backCast, 0.8f);
            Physics.Raycast(transform.position, -transform.forward + -transform.right, out backLeftCast, 1.2f);
            Physics.Raycast(transform.position, -transform.forward + transform.right, out backRightCast, 1.2f);

            if (backCast.normal != Vector3.zero && backCast.transform.tag == "Wall")
            {
                StartCoroutine(EndWallrun());
            }
            else if (frontCast.normal != Vector3.zero && frontCast.transform.tag == "Wall") 
            {
                StartCoroutine(EndWallrun()); 
            }
            else if(side == -1 && leftCast.normal == Vector3.zero  && frontLeftCast.normal == Vector3.zero && backLeftCast.normal == Vector3.zero)
            {
                StartCoroutine(EndWallrun());
            }
            else if(side == 1 && rightCast.normal == Vector3.zero && frontRightCast.normal == Vector3.zero &&  backRightCast.normal == Vector3.zero)
            {
                StartCoroutine(EndWallrun());
            }
        }
    }

    //Coroutine for detaching from the wall
    public IEnumerator EndWallrun()
    {
        playerMovement.wallRunning = false;
        side = 0;
        mouseLook.zRotation = 0f;
        canWallRun = false;

        StartCoroutine(ResetID(currentWallID));

        yield return new WaitForSeconds(0.5f);

        canWallRun = true;
    }

    /*Coroutine for reseting the ID of the current wall, so you can't jump back on a wall unless 
      you jumped on another one first or jumped off and waited n seconds*/
    private IEnumerator ResetID(int lastWallID)
    {
        yield return new WaitForSeconds(3f);

        if(!playerMovement.wallRunning && currentWallID == lastWallID)
        {
            currentWallID = 0;
        }
        else
        {
            yield break;
        }
    }
}