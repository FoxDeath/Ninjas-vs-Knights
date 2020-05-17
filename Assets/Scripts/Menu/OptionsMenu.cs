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
    private MouseLook mouseLook;
    private CrossBow crossBow;
    private Slider volumeSlider;
    private Slider lookSensSlider;
    private Slider scopedSensSlider;

    #region Getters and Setters

    public void SetVolume(float value)
    {
        volumeSlider.value = value;
        AudioListener.volume = value;
    }

    public float GetVolume()
    {
        return volumeSlider.value;
    }

    public void SetLookSensitivity(float sens)
    {
        lookSensSlider.value = sens;

        if(mouseLook != null)
        {
            mouseLook.SetSensitivity(sens);
        }
    }

    public float GetLookSensitivity()
    {
        return lookSensSlider.value;
    }

    public void SetScopedSensitivity(float sens)
    {
        scopedSensSlider.value = sens;

        if(crossBow != null)
        {
            crossBow.SetScopedSensitivity(sens);
        }
    }

    public float GetScopedSensitivity()
    {
        return scopedSensSlider.value;
    }

    #endregion

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        mouseLook = FindObjectOfType<MouseLook>();

        if(FindObjectOfType<CrossBow>() != null)
        {
            crossBow = FindObjectOfType<CrossBow>();
        }

        volumeSlider = transform.Find("Volume Slider").GetComponent<Slider>();
        lookSensSlider = transform.Find("Look Slider").GetComponent<Slider>();
        scopedSensSlider = transform.Find("Scoped Slider").GetComponent<Slider>();

        lookSensSlider.minValue = 0.1f;
        lookSensSlider.maxValue = 5f;
        scopedSensSlider.minValue = 0.1f;
        scopedSensSlider.maxValue = 1f;
    }

    void Start()
    {
        volumeSlider.value = AudioListener.volume;

        if(mouseLook != null)
        {
            lookSensSlider.value = mouseLook.GetSensitivity();
        }

        if(crossBow != null)
        {
            scopedSensSlider.value = crossBow.GetScopeSensitivity();
        }
    }

    void OnEnable()
    {
        SaveManager.GetInstance().LoadOptions();
    }

    void OnDisable()
    {
        SaveManager.GetInstance().SaveOptions();
        controls.Enable();
    }

    void Update()
    {
        if (controls.enabled)
        {
            controls.Disable();
        }

        audioManager.SetMasterVolume(volumeSlider.value);

        if(mouseLook != null)
        {
            mouseLook.SetSensitivity(lookSensSlider.value);
        }
        
        if(crossBow != null)
        {
            crossBow.SetScopedSensitivity(scopedSensSlider.value);
        }
    }
}
