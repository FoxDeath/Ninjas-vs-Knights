using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    [SerializeField] GameObject hookHolder;
    [SerializeField] GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable" && hookHolder.GetComponent<GrapplingHook>().fired)
        {
            player.GetComponent<GrapplingHookMovement>().isHooked = true;
            hookHolder.GetComponent<GrapplingHook>().hookedObject = other.gameObject;
        }
    }
}
