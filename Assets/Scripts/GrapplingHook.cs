using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingHook : MonoBehaviour
{

    public GameObject hook;
    public GameObject hookHolder;
    public GameObject player;

    LineRenderer rope;
    public float hookTravelSpeed = 40f;
    //public float playerTravelSpeed = 30f;

    public bool fired;
    public GameObject hookedObject;

    public float maxDistance = 20f;
    private float currentDistance;

    // Update is called once per frame
    void Update()
    {
        if (fired)
        {
            rope = GetComponent<LineRenderer>();
            rope.positionCount = 2;
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, hook.transform.position);
        }
        if (fired && !player.GetComponent<GrapplingHookMovement>().isHooked)
        {
            hook.transform.parent = null;
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(player.transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
            {
                ReturnHook();
            }
        }

        if (player.GetComponent<GrapplingHookMovement>().isHooked)
        {
            hook.transform.parent = hookedObject.transform;

            //player.transform.position = Vector3.MoveTowards(player.transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);

            float distanceToHook = Vector3.Distance(player.transform.position, hook.transform.position);

            if (distanceToHook < 5f)
            {
                ReturnHook();
            }
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        fired = true;
        if (context.canceled)
        {
            ReturnHook();
        }
    }

    public void ReturnHook()
    {
        rope.positionCount = 0;
        hook.transform.parent = hookHolder.transform;
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        hook.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        fired = false;
        player.GetComponent<GrapplingHookMovement>().isHooked = false;
    }
}
