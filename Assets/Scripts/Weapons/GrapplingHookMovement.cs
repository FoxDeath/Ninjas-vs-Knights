using UnityEngine;
using Mirror;

public class GrapplingHookMovement : NetworkBehaviour
{
    private GameObject hook;
    private AudioManager audioManager;

    [SerializeField] float hookSpeed = 6f;

    public bool hooking;

    void Start()
    {
        hook = GetComponentInChildren<HookDetector>().gameObject;
        audioManager = GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        if(!this.isLocalPlayer)
        {
            return;
        }
        
        HookMovement();
    }

    //If the player is hooking change its velocity to move towards the hook, if we land reset the velocity
    private void HookMovement()
    {
        ref Vector3 velocity = ref GetComponent<NinjaPlayerMovement>().GetVelocityByReference();

        if(hooking)
        {
            if(!audioManager.IsPlaying("GrapplingHooking"))
            {
                audioManager.NetworkPlay("GrapplingHooking");
            }
            
            //Tried to make the speed not dependent on the distance of the hook, not sure if i should use it or not
            Vector3 hookDir = (hook.transform.position - transform.position) / hookSpeed;
            velocity += hookDir;
        }
        else if(GetComponent<NinjaPlayerMovement>().GetGrounded())
        {
            velocity.x = 0f;
            velocity.z = 0f;
        }
        else
        {
            audioManager.NetworkStop("GrapplingHooking");
        }
    }
}
