using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class WeaponSwitch : NetworkBehaviour
{
    [SerializeField] GameObject[] weapons;
    private int currentWeaponIndex;
    private int maxWeaponIndex;

    public enum KnightWeapon
    {
        Crossbow = 0,
        SpearGun = 1
    }

    public enum NinjaWeapon
    {
        ShurikenGun = 0,
        Bow = 1
    }

    private KnightWeapon currentKnightWeapon;
    private NinjaWeapon currentNinjaWeapon;

    private bool isKnight;
    public bool canSwitch = true;

    public KnightWeapon GetCurrentKnightWeapon()
    {
        return currentKnightWeapon;
    }

    public NinjaWeapon GetCurrentNinjaWeapon()
    {
        return currentNinjaWeapon;
    }

    void Start()
    {
        maxWeaponIndex = weapons.Length - 1;

        if (gameObject.TryGetComponent<KnightPlayerMovement>(out KnightPlayerMovement context))
        {
            isKnight = true;
        }
        else
        {
            isKnight = false;
        }
    }

    public GameObject GetCurrentWeaponIndex()
    {
        return weapons[currentWeaponIndex];
    }

    public void SwitchInput(InputAction.CallbackContext context)
    {
        float switchIndex = context.ReadValue<Vector2>().y;

        SwapWeapon(switchIndex);
    }

    private void SwapWeapon(float switchIndex)
    {
        if(switchIndex > 0 && currentWeaponIndex < maxWeaponIndex)
        {
            currentWeaponIndex ++;
        }
        else if(switchIndex < 0 && currentWeaponIndex > 0)
        {
            currentWeaponIndex --;
        }
        else
        {
            return;
        }

        SetWeapon(currentWeaponIndex);
    }

    //ToDo: Maybe change into a circular buffer    
    private void SetWeapon(int i)
    {
        if(!canSwitch || !this.isLocalPlayer)
        {
            return;
        }

        for(int index = 0; index < weapons.Length; index++)
        {
            if(weapons[index].activeSelf)
            {
                SetNetworkInactive(index);
            }
        }

        SetNetworkActive(i);

        if(isKnight)
        {
            currentKnightWeapon = (KnightWeapon)i;
        }
        else
        {
            currentNinjaWeapon = (NinjaWeapon)i;
        }
    }

    private void SetActive(int i)
    {
        switch(weapons[i].transform.parent.parent.parent.name)
        {
            case "Crossbow":
                weapons[i].GetComponentInParent<CrossBow>().SetEquiped(true);
                break;

            case "SpearGun":
                weapons[i].GetComponentInParent<SpearGun>().SetEquiped(true);
            break;

            case "ShurikenGun":
                weapons[i].GetComponentInParent<ShurikenGun>().SetEquiped(true);
            break;

            case "Bow":
                weapons[i].GetComponentInParent<Bow>().SetEquiped(true);
            break;

            default:
            return;
        }

        weapons[i].SetActive(true);
    }

    private void SetNetworkActive(int i)
    {
        SetActive(i);

        if(isServer)
        {
            RpcSetActive(i);
        }
        else
        {
            CmdSetActive(i);
        }
    }

    private void SetNetworkInactive(int i)
    {
        switch(weapons[i].transform.parent.parent.parent.name)
        {
            case "Crossbow":
                    SetCrossBowInactive(weapons[i].GetComponentInParent<CrossBow>());
                        if (isServer)
                        {
                            RpcSetInactive(i);
                        }
                        else
                        {
                            CmdSetInactive(i);
                        }
                break;

            case "SpearGun":
                SetSpearGunInactive(weapons[i].GetComponentInParent<SpearGun>());
                        if (isServer)
                        {
                            RpcSetInactive(i);
                        }
                        else
                        {
                            CmdSetInactive(i);
                        }
            break;

            case "ShurikenGun":
                    SetShurikenGunInactive(weapons[i].GetComponentInParent<ShurikenGun>());
                        if (isServer)
                        {
                            RpcSetInactive(i);
                        }
                        else
                        {
                            CmdSetInactive(i);
                        }
            break;

            case "Bow":
                    SetBowInactive(weapons[i].GetComponentInParent<Bow>());
                        if (isServer)
                        {
                            RpcSetInactive(i);
                        }
                        else
                        {
                            CmdSetInactive(i);
                        }
            break;

            default:
            return;
        }
    }

    public void SetCurrentWeapon(bool state)
    {
        canSwitch = state;

        if(state)
        {
            SetNetworkActive(currentWeaponIndex);
        }
        else
        {
            SetNetworkInactive(currentWeaponIndex);
        }
    }

    public void SetCrossBowInactive(CrossBow crossBow)
    {
        crossBow.SetEquiped(false);

        GetComponent<AudioManager>().NetworkStop("Laser");

        if(crossBow.GetScoping())
        {
            crossBow.Scope();
        }

        crossBow.transform.localRotation = crossBow.GetStartingRotation();
        weapons[0].SetActive(false);
    }

    public void SetSpearGunInactive(SpearGun spearGun)
    {
        spearGun.SetEquiped(false);

        spearGun.SetReloading(false);
        GetComponent<AudioManager>().NetworkStop("Laser");
        spearGun.transform.localRotation = spearGun.GetStartingRotation();
        weapons[1].SetActive(false);
    }

    public void SetShurikenGunInactive(ShurikenGun shurikenGun)
    {
        shurikenGun.SetEquiped(false);

        shurikenGun.SetReloading(false);
        GetComponent<AudioManager>().NetworkStop("Reload");
        GetComponent<AudioManager>().NetworkStop("ShurikenShoot");

        if(shurikenGun.GetScoping())
        {
            shurikenGun.Scope(false);
        }

        weapons[0].gameObject.SetActive(false);
    }

    public void SetBowInactive(Bow bow)
    {
        bow.SetEquiped(false);
        bow.transform.localRotation = bow.GetStartingRotation();
        weapons[1].SetActive(false);
    }

    [Command]
    private void CmdSetActive(int i)
    {
        SetActive(i);
        RpcSetActive(i);
    }

    [ClientRpc]
    void RpcSetActive(int i)
    {
        if(this.isLocalPlayer)
        {
            return;
        }

        SetActive(i);
    }

    
    [Command]
    public void CmdSetInactive(int i)
    {
        switch (weapons[i].transform.parent.parent.parent.name)
        {
            case "Crossbow":
                SetCrossBowInactive(weapons[i].GetComponentInParent<CrossBow>());
                break;

            case "SpearGun":
                SetSpearGunInactive(weapons[i].GetComponentInParent<SpearGun>());
            break;

            case "ShurikenGun":
                SetShurikenGunInactive(weapons[i].GetComponentInParent<ShurikenGun>());
            break;

            case "Bow":
                SetBowInactive(weapons[i].GetComponentInParent<Bow>());
            break;

            default:
            return;
        }
        RpcSetInactive(i);
    }

    [ClientRpc]
    public void RpcSetInactive(int i)
    {
        if(this.isLocalPlayer)
        {
            return;
        }

        switch (weapons[i].transform.parent.parent.parent.name)
        {
            case "Crossbow":
                SetCrossBowInactive(weapons[i].GetComponentInParent<CrossBow>());
                break;

            case "SpearGun":
                SetSpearGunInactive(weapons[i].GetComponentInParent<SpearGun>());
            break;

            case "ShurikenGun":
                SetShurikenGunInactive(weapons[i].GetComponentInParent<ShurikenGun>());
            break;

            case "Bow":
                SetBowInactive(weapons[i].GetComponentInParent<Bow>());
            break;

            default:
            return;
        }
    }
}
