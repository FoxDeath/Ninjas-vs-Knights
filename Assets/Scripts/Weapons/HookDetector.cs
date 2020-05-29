using UnityEngine;

public class HookDetector : MonoBehaviour
{
    private GameObject grapplingHook;
    private GameObject player;
    private AudioManager audioManager;

    void Awake()
    {
        grapplingHook = GetComponentInParent<GrapplingHook>().gameObject;
        player = GetComponentInParent<GrapplingHookMovement>().gameObject;
        audioManager = GetComponentInParent<AudioManager>();
    }

    //When colliding, check if the collider is a hookable object, if it is attach to it
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Hookable") && grapplingHook.GetComponent<GrapplingHook>().firing)
        {
            player.GetComponent<GrapplingHookMovement>().hooking = true;
            grapplingHook.GetComponent<GrapplingHook>().hookedObject = other.gameObject;
            audioManager.NetworkPlay("GrapplingConnecting", GetComponentInParent<AudioSource>());
        }
    }
}
