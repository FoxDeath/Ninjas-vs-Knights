using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    private TextMeshProUGUI currentAmmo;
    private TextMeshProUGUI maxAmmo;
    
    void Awake()
    {
        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach(TextMeshProUGUI text in texts)
        {
            if(text.name.Equals("CurrentAmmo"))
            {
                currentAmmo = text;
            }
            else if(text.name.Equals("MaxAmmo"))
            {
                maxAmmo = text;
            }
        }
    }

    public void SetMaxAmmo(int ammo)
    {
        maxAmmo.text = ammo.ToString();
    }

    public void SetCurrentAmmo(int ammo)
    {
        currentAmmo.text = ammo.ToString();
    }
}
