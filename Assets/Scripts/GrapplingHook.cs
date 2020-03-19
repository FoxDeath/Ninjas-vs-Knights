using UnityEngine;
using UnityEngine.InputSystem;


//TO DO: Refactor(Ben)
public class GrapplingHook : MonoBehaviour
{

    private GameObject hook;
    private GameObject hookHolder;
    private GameObject player;
    private AudioManager audioManager;
    private LineRenderer rope;
    [HideInInspector] public GameObject hookedObject;

    [SerializeField] float hookTravelSpeed = 40f;
    [SerializeField] float maxDistance = 20f;
    [SerializeField] float detachDistance = 5f;
    private float currentDistance;

    public bool firing;

    void Start()
    {
        hook = GetComponentInChildren<HookDetector>().gameObject;
        hookHolder = GameObject.Find("HookHolder");
        player = GetComponentInParent<GrapplingHookMovement>().gameObject;
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    void FixedUpdate()
    {
        Hook();
        ReturnHook();
    }

    //Fire and move the hook,
    //Draw a line to the hook from the hook holder
    //If the hook travels too far return it
    private void Hook()
    {
        if(firing)
        {
            rope = GetComponent<LineRenderer>();
            rope.positionCount = 2;
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        if(firing && !player.GetComponent<GrapplingHookMovement>().hooking)
        {
            hook.transform.parent = null;
            hook.transform.Translate(-Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(player.transform.position, hook.transform.position);

            if(currentDistance >= maxDistance)
            {
                EndHook();
            }
        }
    }

    public void HookInput(InputAction.CallbackContext context)
    {
        if(!firing && context.action.phase == InputActionPhase.Started)
        { 
            audioManager.Play("GrapplingShooting");
        }

        firing = true;

        if(context.canceled)
        {
            EndHook();
        }
    }

    //If player is hooked to a target, calculate the distance between it and the player
    //If player is too close to the hook, detatch it
    public void ReturnHook()
    {
        if(player.GetComponent<GrapplingHookMovement>().hooking)
        {
            hook.transform.parent = hookedObject.transform;

            float distanceToHook = Vector3.Distance(player.transform.position, hook.transform.position);

            if(distanceToHook < detachDistance)
            {
                EndHook();
            }
        }
        
    }

    //Return the hook to the hook holder
    private void EndHook()
    {
        rope.positionCount = 0;
        currentDistance = 0f;
        hook.transform.parent = hookHolder.transform;
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        hook.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        firing = false;
        player.GetComponent<GrapplingHookMovement>().hooking = false;
    }
}