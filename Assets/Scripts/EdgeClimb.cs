using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EdgeClimb : MonoBehaviour
{
    private NinjaPlayerMovement playerMovement;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<NinjaPlayerMovement>();
        anim = GetComponent<Animator>();
    }

    public void StartEdgeClimb()
    {
        StartCoroutine(EdgeClimbing());
        StartCoroutine(MovePlayer());
    }

    IEnumerator EdgeClimbing()
    {
        anim.SetTrigger("EdgeClimb");
        playerMovement.SetEdgeHanging(false);
        playerMovement.SetEdgeClimbing(true);
        yield return new WaitForSeconds(1f);
        playerMovement.SetEdgeClimbing(false);
    }

    IEnumerator MovePlayer()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 3.033f, transform.position.z);
        Vector3 moveVector = targetPos - transform.position;

        while (Mathf.Abs(targetPos.y - transform.position.y) >= 0.5f)
        {
            playerMovement.controller.Move(moveVector);
        }

        targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.495f);
        moveVector = targetPos - transform.position;

        while (Mathf.Abs(targetPos.z - transform.position.z) >= 0.5f)
        {
            playerMovement.controller.Move(moveVector);
        }

        yield break;
    }
}
