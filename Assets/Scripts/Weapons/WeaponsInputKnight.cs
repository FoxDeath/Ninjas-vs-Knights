using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class WeaponsInputKnight : MonoBehaviour
{
    private WeaponSwitch.KnightWeapon currentWeapon;
    private WeaponSwitch weaponSwitch;
    private PlayerInput playerInput;
    private AudioManager audioManager;

    void Start()
    {
        weaponSwitch = GetComponent<WeaponSwitch>();
        audioManager = FindObjectOfType<AudioManager>();
        currentWeapon = weaponSwitch.GetCurrentKnightWeapon();
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
        if (currentWeapon != weaponSwitch.GetCurrentKnightWeapon())
        {
            currentWeapon = weaponSwitch.GetCurrentKnightWeapon();
        }
    }

    private void WeaponsInput()
    {
        switch ((int)currentWeapon)
        {
            case 0:
                CrossbowInput();
                break;

            case 1:
                SpearGunInput();
                break;

            default:
                return;
        }
    }

    private void GrenadeInput()
    {
        if (playerInput.Weapon.Grenade.triggered)
        {
            FindObjectOfType<Slingshot>().Grenade();
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

    private void CrossbowInput()
    {
        CrossBow crossBow = weaponSwitch.GetCurrentWeaponIndex().GetComponent<CrossBow>();

        if(playerInput.Weapon.Fire.triggered)
        {
            crossBow.Fire();
        }

        if(playerInput.Weapon.Scope.triggered)
        {
            playerInput.Weapon.Scope.started += ctx =>
            {
                if (ctx.interaction is PressInteraction && (int)currentWeapon == 0)
                {
                    crossBow.Scope();
                }
            };
        }

        if(playerInput.Weapon.ScopeZoom.triggered)
        {
            crossBow.ScopeZoom();
        }
    }

    private void SpearGunInput()
    {
        SpearGun spearGun = weaponSwitch.GetCurrentWeaponIndex().GetComponent<SpearGun>();

        if(playerInput.Weapon.Fire.triggered)
        {
            spearGun.Fire();
        }
        if(playerInput.Weapon.Reload.triggered)
        {
            spearGun.Reload();
        }
        if(playerInput.Weapon.Scope.triggered)
        {
            spearGun.Charge();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SpearGun spearGun = weaponSwitch.GetCurrentWeaponIndex().GetComponent<SpearGun>();
        
        if(other.tag.Equals("Ammo"))
        {
            audioManager.Play("Pickup", GetComponent<AudioSource>());
            spearGun.RestockAmmo();
            Destroy(other.gameObject);
        }
    }

    private void OnDisable() 
    {
        playerInput.Disable();
    }
}
