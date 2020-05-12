using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitch : MonoBehaviour
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

        if(gameObject.name.Equals("KnightPlayer"))
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

    private void SetWeapon(int i)
    {
        if(!canSwitch)
        {
            return;
        }

        foreach(GameObject weapon in weapons)
        {
            if(weapon.activeSelf)
            {
                SetInactive(weapon);
            }
        }
        weapons[i].SetActive(true);

        if(isKnight)
        {
            currentKnightWeapon = (KnightWeapon)i;
        }
        else
        {
            currentNinjaWeapon = (NinjaWeapon)i;
        }
    }

    private void SetInactive(GameObject weapon)
    {
        switch (weapon.name)
        {
            case "Crossbow":
                weapon.GetComponent<CrossBow>().SetInactive();
            break;

            case "SpearGun":
                weapon.GetComponent<SpearGun>().SetInactive();
            break;

            case "ShurikenGun":
                weapon.GetComponent<ShurikenGun>().SetInactive();
            break;

            case "Bow":
                weapon.GetComponent<Bow>().SetInactive();
            break;

            default:
            return;
        }
    }

    public void SetCurrentWeapon(bool state)
    {
        canSwitch = state;
        weapons[currentWeaponIndex].SetActive(state);
    }
}
