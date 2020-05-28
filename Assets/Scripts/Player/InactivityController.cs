using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InactivityController : NetworkBehaviour
{
    [SerializeField] Camera[] cameras;
    [SerializeField] Canvas[] canvases;
    [SerializeField] AudioListener[] audioListeners;

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
    }
}
