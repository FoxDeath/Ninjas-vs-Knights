using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name, AudioSource source = null)
    {
        if(PauseMenu.GameIsPaused)
        {
            return;
        }

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

        s.source.Play();
        s.isPlaying = true;
    }

    public void Stop(string name)
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

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null)
        {
            return false;
        }

        return s.isPlaying;
    }

    public void SetPitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null || pitch < 0 || pitch > 3)
        {
            return;
        }

        s.source.pitch = pitch; 
    }

    public float GetPitch(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null) 
        {
            return 0f;
        }

        return s.source.pitch;
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
