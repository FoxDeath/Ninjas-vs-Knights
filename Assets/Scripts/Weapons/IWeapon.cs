﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IWeapon
{
    void FireInput(InputAction.CallbackContext context);
}
