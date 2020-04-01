using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] IWeapon[] weapons;
    [SerializeField] PlayerInput PlayerInput;
    private int currentWeaponIndex;
    private int maxWeaponIndex;
    void Start()
    {
        maxWeaponIndex = weapons.Length - 1;
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
        //SetWeapon(currentWeaponIndex);
    }

    // private void SetWeapon(int i)
    // {
    //     foreach(IWeapon weapon in weapons)
    //     {
    //         weapon.gameObject.SetActive(false);
    //     }
    //     weapons[i].gameObject.SetActive(true);
    // }

}
