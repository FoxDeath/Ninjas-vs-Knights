using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KnightUI : MonoBehaviour
{
    public GameObject knightScopeOverlay;
    public GameObject knightUI;

    public TextMeshProUGUI grenadeCount;
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI maxAmmo;
    public TextMeshProUGUI[] texts;

    public Slider knightSlider;
    public Slider healthSlider;

    public List<Image> fills;
    public Image dashFill;
    public Image chargeFill;
    public Image AOEFill;
    public Image stimpackFill;
    public MouseLook mouseLook;

    private void Awake() 
    {
        if(transform.Find("KnightUI") != null)
        {
            fills = new List<Image>();
            knightUI = transform.Find("KnightUI").gameObject;
            grenadeCount = knightUI.transform.Find("Grenade").Find("GrenadeCount").GetComponent<TextMeshProUGUI>();
            texts = knightUI.transform.Find("AmmoCounter").GetComponentsInChildren<TextMeshProUGUI>();
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
            knightSlider = knightUI.transform.Find("FuelSlider").GetComponent<Slider>();
            healthSlider = knightUI.transform.Find("HealthBar").GetComponent<Slider>();
            knightScopeOverlay = transform.Find("ScopeOverlay").gameObject;
            stimpackFill = knightUI.transform.Find("Stimpack").Find("StimpackFill").GetComponent<Image>();
            fills.Add(stimpackFill);
            dashFill = knightUI.transform.Find("Dash").Find("DashFill").GetComponent<Image>();
            fills.Add(dashFill);
            chargeFill = knightUI.transform.Find("Charge").Find("ChargeFill").GetComponent<Image>();
            fills.Add(chargeFill);
            AOEFill = knightUI.transform.Find("AOE").Find("AOEFill").GetComponent<Image>();
            fills.Add(AOEFill);
        }
    }

    void Update()
    {
        FillingAbility(dashFill, 2.4f);
        FillingAbility(stimpackFill, 10f);
        FillingAbility(chargeFill, 12.5f);
        FillingAbility(AOEFill, 15f);
    }

    //Refils the fillAmount for cooldown seconds if the fill is not null or full
    private void FillingAbility(Image fill, float cooldown)
    {
        if(fill == null || (fill != null && fill.fillAmount == 1f))
        {
            return;
        }

        fill.fillAmount += 1f / cooldown * Time.deltaTime;
    }
}
