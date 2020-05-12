using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//TO DO: Add to Menu Manager
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] InputActionAsset controls;
    private AudioManager audioManager;
    private Slider volumeSlider;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        volumeSlider = GetComponentInChildren<Slider>(); 
        volumeSlider.value = AudioListener.volume;
    }

    void OnEnable()
    {
        SaveManager.GetInstance().LoadOptions();
    }

    void OnDisable()
    {
        controls.Enable();
    }

    void Update()
    {
        if (controls.enabled)
        {
            controls.Disable();
        }

        audioManager.SetMasterVolume(volumeSlider.value);
    }

    public void SetVolume(float value)
    {
        volumeSlider.value = value;
        AudioListener.volume = value;
    }

    public float GetVolume()
    {
        return volumeSlider.value;
    }
}
