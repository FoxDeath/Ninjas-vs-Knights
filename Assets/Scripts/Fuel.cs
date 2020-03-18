using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;


//TO DO: Add to UI Manager
public class Fuel : MonoBehaviour
{
    [SerializeField] Slider slider;
    
    public void SetSliderValue(float value)
    {
        slider.value = value;
    }

    public void SetSliderColour(Color color)
    {
        GameObject handle =  slider.transform.GetChild(2).GetChild(0).gameObject;
        Image handleImage = handle.GetComponent<Image>();
        handleImage.color = color;
    }
}
