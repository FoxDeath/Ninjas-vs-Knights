using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InactivityController : NetworkBehaviour
{
    [SerializeField] Camera[] cameras;
    [SerializeField] Canvas[] canvases;
    [SerializeField] AudioListener[] audioListeners;
    [SerializeField] Animator[] animators;
    [SerializeField] GameObject[] playerModels;


    void Update()
    {
        foreach(Camera camera in cameras)
        {
            if(!this.isLocalPlayer)
            {
                camera.enabled = false;
            }
        }

        foreach(Canvas canvas in canvases)
        {
            if(!this.isLocalPlayer)
            {
                canvas.enabled = false;
            }
        }

        foreach(AudioListener audio in audioListeners)
        {
            if(!this.isLocalPlayer)
            {
                audio.enabled = false;
            }
        }

        foreach(Animator animator in animators)
        {
            if(!this.isLocalPlayer)
            {
                //animator.enabled = false;
            }
        }

        foreach(GameObject gameObject in playerModels)
        {
            if(this.isLocalPlayer)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
