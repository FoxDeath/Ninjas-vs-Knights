using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;

public class RebindMenu : MonoBehaviour
{
    [SerializeField] InputActionAsset controls;

    void OnEnable()
    {
        SaveManager.GetInstance().LoadConfig();

        foreach (RebindActionUI x in GetComponentsInChildren<RebindActionUI>())
        {
            x.UpdateBindingDisplay();
        }
    }

    void OnDisable()
    {
        controls.Enable();
    }

    void Update()
    {
        if(controls.enabled)
        {
            controls.Disable();
        }
    }
}
