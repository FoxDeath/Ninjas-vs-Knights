using System;
using UnityEngine;
using Mirror;


//Audio Manager who controlls all the sounds and their behaviours
public class AudioManager : NetworkBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        //Adding a sourse to all sounds
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = 1f;
        }
    }

    /*Playing the sound whose name is the string "name" 
    and also giving the posibility to change the audio source with the optional paramarer "source" */
    private void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));

        if(s == null)
        {
            return;
        }

        s.source.Play();
        s.isPlaying = true;
    }

    //Calling the play method for multiplayer, both in server and on all clients
    public void NetworkPlay(string name, AudioSource source = null)
    {
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));

        if(s == null)
        {
            return;
        }

        if(source != null)
        {
            s.source = source;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        Play(name);

        if (isServer)
        {
            RpcPlay(name);
        }
        else
        {
            CmdPlay(name);
        }
    }

    [Command]
    private void CmdPlay(string name)
    {
        Play(name);
        RpcPlay(name);
    }

    [ClientRpc]
     void RpcPlay(string name)
     {
         if(this.isLocalPlayer)
         {
            return;
         }

         Play(name);
    }

    //Stopping the sound whose name is the string "name"
    private void Stop(string name)
    {
        if(!IsPlaying(name))
        {
            return;
        }
        else
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            
            if(s == null)
            {
                return;
            }

            s.source.Stop();
            s.isPlaying = false;
        }
    }

    //Calling the stop method for multiplayer, both in server and on all clients
    public void NetworkStop(string name)
    {
        Stop(name);

        if (isServer)
        {
            RpcStop(name);
        }
        else
        {
            CmdStop(name);
        }
    }

    [Command]
    private void CmdStop(string name)
    {
        Stop(name);
        RpcStop(name);
    }

    [ClientRpc]
     void RpcStop(string name)
     {
         if(this.isLocalPlayer)
         {
            return;
         }

         Stop(name);
    }

    //Stopping all sounds who are currently playing
    public void StopAll()
    {
        foreach(Sound s in sounds)
        {
            if(s.source != null && s.isPlaying)
            {
                Stop(s.name);
            }
        }
    }

    //Returning true if the sound whose name is the string "name" is playing or false if it's not playing
    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null)
        {
            return false;
        }

        return s.isPlaying;
    }

    //Setting the pitch of the sound whose name is the string "name" withing the rance 0.1 and 3
    private void SetPitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null || pitch < 0.1f || pitch > 3f)
        {
            return;
        }

        s.source.pitch = pitch; 
    }

    //Calling the SetPitch method for multiplayer, both in server and on all clients
    public void NetworkSetPitch(string name, float pitch)
    {
        SetPitch(name, pitch);

        if (isServer)
        {
            RpcSetPitch(name, pitch);
        }
        else
        {
            CmdSetPitch(name, pitch);
        }
    }

    [Command]
    private void CmdSetPitch(string name, float pitch)
    {
        SetPitch(name, pitch);
        RpcSetPitch(name, pitch);
    }

    [ClientRpc]
     void RpcSetPitch(string name, float pitch)
     {
         if(this.isLocalPlayer)
         {
            return;
         }

         SetPitch(name, pitch);
    }

    //Gets the pitch of the sound whose name is the string "name"
    public float GetPitch(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null) 
        {
            return 0f;
        }

        return s.source.pitch;
    }

    //Setting the master volume of the whole game
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
