using UnityEngine;

public class HookDetector : MonoBehaviour
{
    private GameObject grapplingHook;
    private GameObject player;
    private AudioManager audioManager;

    void Start()
    {
        grapplingHook = GetComponentInParent<GrapplingHook>().gameObject;
        player = GetComponentInParent<GrapplingHookMovement>().gameObject;
        audioManager = FindObjectOfType<AudioManager>();
    }

    //When colliding, check if the collider is a hookable object, if it is attach to it
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Hookable") && grapplingHook.GetComponent<GrapplingHook>().firing)
        {
            player.GetComponent<GrapplingHookMovement>().hooking = true;
            grapplingHook.GetComponent<GrapplingHook>().hookedObject = other.gameObject;
            audioManager.Play("GrapplingConnecting", GetComponent<AudioSource>());
        }
    }
}
