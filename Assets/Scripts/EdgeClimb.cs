using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EdgeClimb : MonoBehaviour
{
    private NinjaPlayerMovement playerMovement;
    private Animator anim;

    private Camera parkourCamera;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<NinjaPlayerMovement>();
        anim = GetComponent<Animator>();

        parkourCamera = gameObject.transform.Find("Parkour Camera").GetComponent<Camera>();
        mainCamera = gameObject.transform.Find("Main Camera").GetComponent<Camera>();
    }

    public void StartEdgeClimb()
    {
        StartCoroutine(EdgeClimbing());
        StartCoroutine(MovePlayer());
    }

    IEnumerator EdgeClimbing()
    {
        parkourCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);

        anim.SetTrigger("EdgeClimb");

        FindObjectOfType<AudioManager>().Play("Climb");

        playerMovement.edgeHanging = false;
        playerMovement.edgeClimbing = true;
        yield return new WaitForSeconds(1f);
        playerMovement.edgeClimbing = false;

        mainCamera.gameObject.SetActive(true);
        parkourCamera.gameObject.SetActive(false);
    }

    IEnumerator MovePlayer()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 3.033f, transform.position.z);
        Vector3 moveVector = targetPos - transform.position;

        while (Mathf.Abs(targetPos.y - transform.position.y) >= 0.5f)
        {
            playerMovement.controller.Move(moveVector);
        }

        targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.495f);
        moveVector = targetPos - transform.position;

        while (Mathf.Abs(targetPos.z - transform.position.z) >= 0.5f)
        {
            playerMovement.controller.Move(moveVector);
        }

        yield break;
    }
}
