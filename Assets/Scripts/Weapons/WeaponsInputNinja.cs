using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Mirror;

public class WeaponsInputNinja : NetworkBehaviour
{
    private InputActionAsset inputActions;
    private WeaponSwitch.NinjaWeapon currentWeapon;
    private WeaponSwitch weaponSwitch;
    private Bow bow;
    private ShurikenGun shurikenGun;
    private KunaiNadeInput kunai;

    private bool openInput = true;

    private AudioManager audioManager;

    void Awake()
    {
        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
        weaponSwitch = GetComponent<WeaponSwitch>();
        audioManager = FindObjectOfType<AudioManager>();
        currentWeapon = weaponSwitch.GetCurrentNinjaWeapon();
        bow = transform.Find("Main Camera").Find("Bow").GetComponent<Bow>();
        shurikenGun = transform.Find("Main Camera").Find("ShurikenGun").GetComponent<ShurikenGun>();
        kunai = GetComponent<KunaiNadeInput>();
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
        if(!this.isLocalPlayer)
        {
            return;
        }
        
        SetCurrentWeapon();

        CanShoot();
    }

    private void SetCurrentWeapon()
    {
        if(currentWeapon != weaponSwitch.GetCurrentNinjaWeapon())
        {
            currentWeapon = weaponSwitch.GetCurrentNinjaWeapon();
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
            kunai.ThrowKunai();
        }
    }

    public void ShurikenGunFireInput(InputAction.CallbackContext context)
    {
        if((int)currentWeapon == 0 && openInput)
        {
            if (context.action.phase == InputActionPhase.Performed)
            {
                shurikenGun.Fire();
            }
        }
    }

    public void ShurikenGunScopeInput(InputAction.CallbackContext context)
    {
        if ((int)currentWeapon == 0 && openInput)
        {
            if (context.interaction is PressInteraction && context.action.phase == InputActionPhase.Started)
            {
                shurikenGun.Scope(true);
            }

            if (context.interaction is PressInteraction && context.action.phase == InputActionPhase.Canceled)
            {
                shurikenGun.Scope(false);
            }
        }
    }

    public void ShurikenGunReloadInput(InputAction.CallbackContext context)
    {
        if ((int)currentWeapon == 0 && openInput)
        {
            if (context.action.phase == InputActionPhase.Performed)
            {
                shurikenGun.Reload();
            }
        }
    }

    public void BowFireInput(InputAction.CallbackContext context)
    {
        if((int)currentWeapon == 1 && openInput)
        {
            if (bow.CanShoot())
            {
                if (context.action.phase == InputActionPhase.Started)
                {
                    bow.SetCharging(true);
                }

                if (context.action.phase == InputActionPhase.Canceled)
                {
                    bow.SetCharging(false);
                    bow.Fire();
                };
            }
        }
    }

    public void BowMenuInput(InputAction.CallbackContext context)
    {
        if ((int)currentWeapon == 1 && openInput)
        {
            if (context.interaction is PressInteraction && context.action.phase == InputActionPhase.Started)
            {
                bow.SetArrowMenuState(true);
            }

            if (context.interaction is PressInteraction && context.action.phase == InputActionPhase.Canceled)
            {
                bow.SetArrowMenuState(false);
            }
        }
    }
}
