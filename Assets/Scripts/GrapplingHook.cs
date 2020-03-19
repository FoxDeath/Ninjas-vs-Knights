using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//TO DO: Refactor(Ben)
public class GrapplingHook : MonoBehaviour
{

    [SerializeField] GameObject hook;
    [SerializeField] GameObject hookHolder;
    [SerializeField] GameObject player;
    private AudioManager audioManager;

    private LineRenderer rope;
    [SerializeField] float hookTravelSpeed = 40f;

    public bool fired;
    public GameObject hookedObject;

    [SerializeField] float maxDistance = 20f;
    private float currentDistance;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    void FixedUpdate()
    {
        Hook();
        ReturnHook();
    }

    private void Hook()
    {
        if (fired)
        {
            rope = GetComponent<LineRenderer>();
            rope.positionCount = 2;
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }
        if (fired && !player.GetComponent<GrapplingHookMovement>().isHooked)
        {
            hook.transform.parent = null;
            hook.transform.Translate(-Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(player.transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
            {
                EndHook();
            }
        }
    }

    public void HookInput(InputAction.CallbackContext context)
    {
        if(!fired && context.action.phase == InputActionPhase.Started)
        { 
            audioManager.Play("GrapplingShooting");
        }

        fired = true;
        if (context.canceled)
        {
            EndHook();
        }
    }

    public void ReturnHook()
    {
        if (player.GetComponent<GrapplingHookMovement>().isHooked)
        {
            hook.transform.parent = hookedObject.transform;

            float distanceToHook = Vector3.Distance(player.transform.position, hook.transform.position);

            if (distanceToHook < 5f)
            {
                EndHook();
            }
        }
        
    }

    private void EndHook()
    {
        rope.positionCount = 0;
        currentDistance = 0f;
        hook.transform.parent = hookHolder.transform;
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        hook.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        fired = false;
        player.GetComponent<GrapplingHookMovement>().isHooked = false;
    }
}
