using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TO DO: Refactor(Ben)
public class HookDetector : MonoBehaviour
{
    [SerializeField] GameObject hookHolder;
    [SerializeField] GameObject player;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable" && hookHolder.GetComponent<GrapplingHook>().fired)
        {
            player.GetComponent<GrapplingHookMovement>().isHooked = true;
            hookHolder.GetComponent<GrapplingHook>().hookedObject = other.gameObject;
            audioManager.Play("GrapplingConnecting", GetComponent<AudioSource>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hookable")
        {
            audioManager.Play("GrapplingDisconnecting", GetComponent<AudioSource>());
        }
    }
}
