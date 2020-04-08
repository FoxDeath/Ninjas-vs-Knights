using System.Collections;
using UnityEngine;

public class EdgeHang : MonoBehaviour
{
    private bool useIK; //used for future animations and shit
    private bool handIK;
    private bool footIK;
    private bool hanging;
    private bool canHang;

    private Vector3 handPosition;
    private Vector3 handOffset;
    private Vector3 footPosition;
    private Vector3 footOffset;
    private Vector3 handOriginalPosition;

    private Quaternion handRotation;
    private Quaternion footRotation;
    private Quaternion footRotationOffset;
    
    private PlayerMovement playerMovement;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerMovement = GetComponent<PlayerMovement>();

        canHang = true;
    }

    //Two raycasts are created, one for the hands and one for the feet. 
    void FixedUpdate()
    {
        RaycastHit handHit;
        RaycastHit footHit;

        //Hand raycast, with the hands rotating coreclty when animations will be added (hopefully)
        if(Physics.Raycast(transform.position + new Vector3(0.0f, 1.5f, 0.0f), transform.forward, out handHit, 1.2f) && canHang)
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

        //Foot raycast
        if(Physics.Raycast(transform.position + new Vector3(0.0f, 1f, 0.0f), transform.forward, out footHit, 1.2f) && canHang)
        {
            footIK = true;
            footPosition = footHit.point - footOffset;
            footRotation = Quaternion.FromToRotation(Vector3.up, footHit.normal) * footRotationOffset;
        }
        else
        {
            footIK = false;
        }

        if(hanging)
        {
            if(!handIK && footIK)
            {
                if(!playerMovement.GetEdgeHanging())
                {       
                    audioManager.Play("EdgeHang");
                    playerMovement.ZeroVelocity();
                    StartCoroutine(HangTimer());
                }

                playerMovement.SetEdgeHanging(true);
            }
            else
            {
                playerMovement.SetEdgeHanging(false);
            }
        }
    }

    IEnumerator HangTimer()
    {
        yield return new WaitForSeconds(3f);

        canHang = false;

        yield return new WaitForSeconds(0.5f);

        canHang = true;
    }

    //Checks the game objects tag when collision happens, to make sure if the player will be able to hang.
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "EdgeHang")
        {
            hanging = true;
        }
        else
        {
            hanging = false;
        }
    }
}
