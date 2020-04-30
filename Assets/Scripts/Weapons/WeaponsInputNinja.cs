using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class WeaponsInputNinja : MonoBehaviour
{
    private WeaponSwitch.NinjaWeapon currentWeapon;
    private WeaponSwitch weaponSwitch;
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        weaponSwitch = GetComponent<WeaponSwitch>();
        currentWeapon = weaponSwitch.GetCurrentNinjaWeapon();
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWeapon != weaponSwitch.GetCurrentNinjaWeapon())
        {
            playerInput.Dispose();
            playerInput = new PlayerInput();
            playerInput.Enable();
            currentWeapon = weaponSwitch.GetCurrentNinjaWeapon();
        }

        switch ((int)currentWeapon)
        {
            case 0:
                ShurikenGunInput();
            break;

            case 1:
                BowInput();
            break;

            default:
            return;
        }
    }

    private void ShurikenGunInput()
    {
        ShurikenGun shurikenGun = weaponSwitch.GetCurrentWeaponIndex().GetComponent<ShurikenGun>();
        if(playerInput.Weapon.Fire.triggered)
        {
            shurikenGun.Fire();
        }
        if(playerInput.Weapon.Scope.triggered)
        {
            shurikenGun.Scope();
        }
        if(playerInput.Weapon.Reload.triggered)
        {
            shurikenGun.Reload();
        }
    }

    private void BowInput()
    {
        Bow bow = weaponSwitch.GetCurrentWeaponIndex().GetComponent<Bow>();
        
        if(bow.CanShoot())
        {
            playerInput.Weapon.FireCharge.performed += _ => 
            {
                bow.SetCharging(true);
            };
            playerInput.Weapon.FireCharge.canceled += _ =>
            {
                StartCoroutine(bow.Fire());
            };
        }

        playerInput.Weapon.Scope.performed += _ => 
        {
            bow.SetArrowMenuState(true);
        };
        playerInput.Weapon.Scope.canceled += _ =>
        {
            bow.SetArrowMenuState(false);
        };
    }

    private void OnDisable() 
    {
        playerInput.Disable();
    }
}
