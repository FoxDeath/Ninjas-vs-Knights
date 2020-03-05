using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingHookMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject hook;
    public GameObject hookHolder;
    public bool isHooked;
    public float hookSpeed = 10f;

    void Update()
    {
        ref Vector3 velocity = ref GetComponent<PlayerMovement>().GetVelocityByReference();
        if (isHooked)
        {
            Vector3 hookDir = (hook.transform.position - transform.position) / hookSpeed;
            velocity += hookDir;
        }
        else if (GetComponent<PlayerMovement>().isGrounded)
        {
            velocity.x = 0f;
            velocity.z = 0f;
        }

        if (!isHooked && velocity.x != 0)
        {
            if (velocity.x < 0)
                velocity.x = velocity.x / 1.002f;
            else
                velocity.x = velocity.x / 1.002f;
        }

        if (!isHooked && velocity.z != 0)
        {
            if (velocity.z < 0)
                velocity.z = velocity.z / 1.002f;
            else
                velocity.z = velocity.z / 1.002f;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ref Vector3 velocity = ref GetComponent<PlayerMovement>().GetVelocityByReference();
        if (!GetComponent<PlayerMovement>().isGrounded && isHooked &&
            (hit.transform == hook.transform.parent ||
            Vector3.Distance(transform.position, hook.transform.position) > Vector3.Distance(hit.point, hook.transform.position)))
        {
            hookHolder.GetComponent<GrapplingHook>().ReturnHook();
            velocity.y = 0f;
            velocity.x = 0f;
            velocity.z = 0f;
        }
    }
}
