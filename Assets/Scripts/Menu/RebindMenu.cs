﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;

public class RebindMenu : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    void Awake()
    {
        if(inputActions == null)
        {
            inputActions = GetComponentInParent<UnityEngine.InputSystem.PlayerInput>().actions;
        }
    }

    void OnEnable()
    {
        SaveManager.GetInstance().LoadConfig();

        foreach(RebindActionUI x in GetComponentsInChildren<RebindActionUI>())
        {
            x.UpdateBindingDisplay();
        }
    }

    void OnDisable()
    {
        SaveManager.GetInstance().SaveConfig();
        inputActions.Enable();
    }

    void Update()
    {
        if(inputActions.enabled)
        {
            inputActions.Disable();
        }
    }
}
