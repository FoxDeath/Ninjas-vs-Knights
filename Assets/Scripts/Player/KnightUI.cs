using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KnightUI : MonoBehaviour
{
    [HideInInspector] public GameObject knightScopeOverlay;
    [HideInInspector] public GameObject knightUI;

    [HideInInspector] public TextMeshProUGUI waveCounter;
    [HideInInspector] public TextMeshProUGUI grenadeCount;
    [HideInInspector] public TextMeshProUGUI currentAmmo;
    [HideInInspector] public TextMeshProUGUI maxAmmo;
    [HideInInspector] public TextMeshProUGUI[] texts;

    [HideInInspector] public Color ogWaveNrColor;
    [HideInInspector] public Slider knightSlider;
    [HideInInspector] public Slider healthSlider;

    [HideInInspector] public List<Image> fills;
    [HideInInspector] public Image dashFill;
    [HideInInspector] public Image chargeFill;
    [HideInInspector] public Image AOEFill;
    [HideInInspector] public Image stimpackFill;
    [HideInInspector] public MouseLook mouseLook;

    private void Awake() 
    {
        if(transform.Find("KnightUI") != null)
        {
            fills = new List<Image>();
            knightUI = transform.Find("KnightUI").gameObject;
            waveCounter = knightUI.transform.Find("WaveCounter").GetComponent<TextMeshProUGUI>();
            ogWaveNrColor = waveCounter.color;
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
            
            FindObjectOfType<WaveManager>().AddKnightUI(GetComponent<KnightUI>());
            FindObjectOfType<UIManager>().SetWaveCounter(0.ToString(), false, null, GetComponent<KnightUI>());
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
