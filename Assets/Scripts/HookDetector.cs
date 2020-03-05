using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject hookHolder;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable" && hookHolder.GetComponent<GrapplingHook>().fired)
        {
            hookHolder.GetComponent<GrapplingHook>().player.GetComponent<GrapplingHookMovement>().isHooked = true;
            hookHolder.GetComponent<GrapplingHook>().hookedObject = other.gameObject;
        }
    }
}
