using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RebindMenu : MonoBehaviour
{
    public InputActionAsset playerInput;

    // Start is called before the first frame update
    void OnEnable()
    {
        playerInput.Disable();
    }

    void OnDisable()
    {
        playerInput.Enable();
    }
}
