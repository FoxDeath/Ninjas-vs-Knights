using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingHookMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] GameObject hook;
    [SerializeField] GameObject hookHolder;
    public bool isHooked;
    [SerializeField] float hookSpeed = 10f;

    void FixedUpdate()
    {
        HookMovement();
    }

    private void HookMovement()
    {
        ref Vector3 velocity = ref GetComponent<NinjaPlayerMovement>().GetVelocityByReference();
        if (isHooked)
        {
            Vector3 hookDir = (hook.transform.position - transform.position) / hookSpeed;
            velocity += hookDir;
        }
        else if (GetComponent<NinjaPlayerMovement>().isGrounded)
        {
            velocity.x = 0f;
            velocity.z = 0f;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ref Vector3 velocity = ref GetComponent<NinjaPlayerMovement>().GetVelocityByReference();
        if (!GetComponent<NinjaPlayerMovement>().isGrounded && isHooked &&
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
