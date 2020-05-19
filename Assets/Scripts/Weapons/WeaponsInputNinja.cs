using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class WeaponsInputNinja : MonoBehaviour
{
    private WeaponSwitch.NinjaWeapon currentWeapon;
    private WeaponSwitch weaponSwitch;
    private PlayerInput playerInput;
    private AudioManager audioManager;

    void Start()
    {
        weaponSwitch = GetComponent<WeaponSwitch>();
        audioManager = FindObjectOfType<AudioManager>();
        currentWeapon = weaponSwitch.GetCurrentNinjaWeapon();
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    void Update()
    {
        SetCurrentWeapon();

        CanShoot();

        WeaponsInput();

        GrenadeInput();
    }

    private void SetCurrentWeapon()
    {
        if (currentWeapon != weaponSwitch.GetCurrentNinjaWeapon())
        {
            playerInput.Dispose();
            playerInput = new PlayerInput();
            playerInput.Enable();
            currentWeapon = weaponSwitch.GetCurrentNinjaWeapon();
        }
    }

    private void CanShoot()
    {
        if (!GetComponent<WeaponSwitch>().canSwitch)
        {
            playerInput.Disable();
        }
        else
        {
            playerInput.Enable();
        }
    }

    private void WeaponsInput()
    {
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

    private void GrenadeInput()
    {
        if (playerInput.Weapon.Grenade.triggered)
        {
            FindObjectOfType<KunaiNadeInput>().ThrowKunai();
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
            playerInput.Weapon.Scope.started += ctx =>
            {
                if (ctx.interaction is PressInteraction)
                {
                    shurikenGun.Scope(true);
                }
            };

            playerInput.Weapon.Scope.canceled += ctx =>
            {
                if(ctx.interaction is PressInteraction)
                {
                    shurikenGun.Scope(false);
                }
            };
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
            playerInput.Weapon.Fire.started += _ => 
            {
                bow.SetCharging(true);
            };

            playerInput.Weapon.Fire.canceled += _ =>
            {
                bow.SetCharging(false);
                bow.Fire();
            };
        }

        playerInput.Weapon.Scope.started += ctx => 
        {
            if(ctx.interaction is PressInteraction)
            {
                bow.SetArrowMenuState(true);
            }
        };

        playerInput.Weapon.Scope.canceled += ctx =>
        {
            if (ctx.interaction is PressInteraction)
            {
                bow.SetArrowMenuState(false);
            }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        ShurikenGun shurikenGun = weaponSwitch.GetCurrentWeaponIndex().GetComponent<ShurikenGun>();

        if(other.tag.Equals("Ammo"))
        {
            audioManager.Play("Pickup", GetComponent<AudioSource>());
            shurikenGun.RestockAmmo();
            Destroy(other.gameObject);
        }
    }

    private void OnDisable() 
    {
        playerInput.Disable();
    }
}
