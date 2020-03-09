using System.Collections;
using UnityEngine;

public class EdgeHang : MonoBehaviour
{
    public bool useIK; //used for future animations and shit
    public bool handIK;
    public bool footIK;
    public bool canHang;

    public Vector3 handPosition;
    public Vector3 handOffset;
    public Vector3 footPosition;
    public Vector3 footOffset;
    public Vector3 handOriginalPosition;
    public Quaternion handRotation;
    public Quaternion footRotation;
    public Quaternion footRotationOffset;
    public NinjaPlayerMovement playerMovement;

    void FixedUpdate()
    {
        RaycastHit handHit;
        RaycastHit footHit;

        // hand raycast
        if(Physics.Raycast(transform.position + new Vector3(0.0f, 2.0f, 0.0f) + transform.forward, -transform.up, out handHit, 1f))
        {
            handIK = true;
            handPosition = handHit.point - handOffset;
            handPosition.x = handOriginalPosition.x - handOffset.x;
            handPosition.z = footPosition.z - handOffset.z;
            handRotation = Quaternion.FromToRotation(Vector3.forward, handHit.normal);
        }
        else
        {
            handIK = false;
        }

        // foot raycast
        if(Physics.Raycast(transform.position + new Vector3(0.0f, -0.5f, 0.0f), transform.forward, out footHit, 1f))
        {
            footIK = true;
            footPosition = footHit.point - footOffset;
            footRotation = Quaternion.FromToRotation(Vector3.up, footHit.normal) * footRotationOffset;
        }
        else
        {
            footIK = false;
        }

        if(canHang)
        {
            if (handIK && footIK)
            {
                playerMovement.SetEdgeHanging(true);
            }
            else
            {
                playerMovement.SetEdgeHanging(false);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "EdgeHang")
        {
            canHang = true;
        }
        else
        {
            canHang = false;
        }
    }
}
