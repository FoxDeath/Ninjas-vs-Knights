using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//TO DO: Add to Menu Manager
public class OptionsMenu : MonoBehaviour
{
    private AudioManager audioManager;
    private Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        volumeSlider = GetComponentInChildren<Slider>(); 
        volumeSlider.value = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        audioManager.SetMasterVolume(volumeSlider.value);
    }
}
