using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Mirror;

public class WeaponsInputKnight : NetworkBehaviour
{
    private InputActionAsset inputActions;
    private WeaponSwitch.KnightWeapon currentWeapon;
    private WeaponSwitch weaponSwitch;
    private CrossBow crossBow;
    private SpearGun spearGun;
    private Slingshot slingshot;

    private bool openInput = true;

    private AudioManager audioManager;
    void Awake()
    {
        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
        weaponSwitch = GetComponent<WeaponSwitch>();
        audioManager = FindObjectOfType<AudioManager>();
        currentWeapon = weaponSwitch.GetCurrentKnightWeapon();
        crossBow = transform.Find("Main Camera").Find("Crossbow").GetComponent<CrossBow>();
        spearGun = transform.Find("Main Camera").Find("SpearGun").GetComponent<SpearGun>();
        slingshot = GetComponent<Slingshot>();
    }

    void Start()
    {
        SaveManager.GetInstance().LoadConfig();
        SaveManager.GetInstance().LoadOptions();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        SetCurrentWeapon();

        CanShoot();
    }

    private void SetCurrentWeapon()
    {
        if(currentWeapon != weaponSwitch.GetCurrentKnightWeapon())
        {
            currentWeapon = weaponSwitch.GetCurrentKnightWeapon();
        }
    }

    private void CanShoot()
    {
        if(!GetComponent<WeaponSwitch>().canSwitch)
        {
            openInput = false;
        }
        else
        {
            openInput = true;
        }
    }

    public void GrenadeInput(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed)
        {
            slingshot.Grenade();
        }
    }

    public void CrossbowFireInput(InputAction.CallbackContext context)
    {
        if((int)currentWeapon == 0 && openInput)
        {
            if(context.action.phase == InputActionPhase.Performed)
            {
                crossBow.Fire();
            }
        }
    }

    public void CrossbowScopeInput(InputAction.CallbackContext context)
    {
        if ((int)currentWeapon == 0 && openInput)
        {
            if (context.interaction is PressInteraction && context.action.phase == InputActionPhase.Performed)
            {
                crossBow.Scope();
            }
        }
    }

    public void CrossbowScopeZoomInput(InputAction.CallbackContext context)
    {
        if ((int)currentWeapon == 0 && openInput)
        {
            if (context.action.phase == InputActionPhase.Performed)
            {
                crossBow.ScopeZoom();
            }
        }
    }

    public void SpearGunFireInput(InputAction.CallbackContext context)
    {
        if ((int)currentWeapon == 1 && openInput)
        {
            if (context.action.phase == InputActionPhase.Performed)
            {
                spearGun.Fire();
            }
        }
    }

    public void SpearGunReloadInput(InputAction.CallbackContext context)
    {
        if ((int)currentWeapon == 1 && openInput)
        {
            if (context.action.phase == InputActionPhase.Performed)
            {
                spearGun.Reload();
            }
        }
    }
}
