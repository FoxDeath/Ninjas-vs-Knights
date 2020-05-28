using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class NinjaUI : MonoBehaviour
{
    private GameObject ninjaUI;
    public GameObject arrowSelect;

    
    public TextMeshProUGUI grenadeCount;
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI maxAmmo;
    public TextMeshProUGUI[] texts;

    
    public Slider healthSlider;
    public List<Image> fills;
    public MouseLook mouseLook;
    public Image stimpackFill;

    
    public Bow bow;
    [SerializeField] static Color normalColor = new Color(0f / 255f, 0f / 255f, 0f / 255f, 188f / 255f);
    [SerializeField] static Color highlightedColor = new Color(224f / 255f, 114f / 255f, 0f / 255f, 255f / 255f);
    public static Image[] radialOptions;

    
    public string selectedOption;

    
    public Vector2 moveInput;

    public bool inArrowMenu = false;

    private void Awake() 
    {
        if(transform.Find("NinjaUI") != null)
        {
            //if current player is a ninja, it gets the ninja assets
            fills = new List<Image>();
            texts = transform.Find("NinjaUI").Find("AmmoCounter").GetComponentsInChildren<TextMeshProUGUI>();

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
            ninjaUI = transform.Find("NinjaUI").gameObject;
            grenadeCount = ninjaUI.transform.Find("Grenade").Find("GrenadeCount").GetComponent<TextMeshProUGUI>();
            healthSlider = ninjaUI.transform.Find("HealthBar").GetComponent<Slider>();
            arrowSelect = transform.Find("NinjaUI").Find("ArrowSelect").gameObject;
            arrowSelect = arrowSelect.transform.Find("Wheel").gameObject;
            mouseLook = transform.GetComponentInParent<MouseLook>();
            stimpackFill = ninjaUI.transform.Find("Stimpack").Find("StimpackFill").GetComponent<Image>();
            fills.Add(stimpackFill);

            radialOptions = new Image[arrowSelect.transform.childCount];
            for(int i = 0; i < arrowSelect.transform.childCount; i++)
            {
                radialOptions[i] = arrowSelect.transform.GetChild(i).GetComponent<Image>();
            }
        }
    }

        public void ArrowMenuInput(InputAction.CallbackContext context)
    {
        if (inArrowMenu)
        {
            moveInput.x = context.ReadValue<Vector2>().x - (Screen.width / 2f);
            moveInput.y = context.ReadValue<Vector2>().y - (Screen.height / 2f);
            moveInput.Normalize();

            if (moveInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveInput.y, -moveInput.x) / Mathf.PI;
                angle *= 180f;
                angle += 90f;

                if (angle < 0f)
                {
                    angle += 360f;
                }

                for(int i = 0; i < radialOptions.Length; i++)
                {
                    if(angle > i * 90 && angle < (i + 1) * 90)
                    {
                        radialOptions[i].color = highlightedColor;
                        selectedOption = radialOptions[i].name;
                    }
                    else
                    {
                        radialOptions[i].color = normalColor;
                    }
                }
            }
        }
    }

    void Update()
    {
        FillingAbility(stimpackFill, 10f);
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
