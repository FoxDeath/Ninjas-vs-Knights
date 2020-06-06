using System.Collections;
using UnityEngine;

public class EdgeClimb : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator anim;
    private PlayerInput playerInput;

    private Camera parkourCamera;
    private Camera mainCamera;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        playerInput = new PlayerInput();
    }

    void Start()
    {
        parkourCamera = transform.Find("Parkour Camera").GetComponent<Camera>();
        mainCamera = transform.Find("Main Camera").GetComponent<Camera>();
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    public void EdgeClimbStart()
    {
        StartCoroutine(EdgeClimbBehaviour());
        StartCoroutine(MovePlayer());
    }

    IEnumerator EdgeClimbBehaviour()
    {
        //switches cameras
        parkourCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);

        //start anim
        anim.SetTrigger("EdgeClimb");

        //plays sound
        GetComponent<AudioManager>().NetworkPlay("Climb");

        playerMovement.SetEdgeHanging(false);
        playerMovement.SetEdgeClimbing(true);

        yield return new WaitForSeconds(1f);

        playerMovement.SetEdgeClimbing(false);

        //switches cameras back to default
        mainCamera.gameObject.SetActive(true);
        parkourCamera.gameObject.SetActive(false);
    }

    IEnumerator MovePlayer()
    {
        //gets destination position and move direction
        Vector3 targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y + 3.033f, transform.localPosition.z + 1.495f);
        Vector3 moveVector = targetPos - transform.localPosition;

        //moves the player
        playerMovement.GetController().Move(transform.up * moveVector.y);
        playerMovement.GetController().Move(transform.forward * moveVector.z);

        yield break;
    }
}
