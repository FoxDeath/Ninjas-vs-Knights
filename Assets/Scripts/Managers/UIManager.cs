using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    private GameObject ui;
    private GameObject knightScopeOverlay;
    private GameObject knightUI;
    private GameObject ninjaUI;
    private GameObject arrowSelect;

    private TextMeshProUGUI grenadeCount;
    private TextMeshProUGUI currentAmmo;
    private TextMeshProUGUI maxAmmo;
    private TextMeshProUGUI[] texts;
    private Slider knightSlider;
    private Slider healthSlider;
    private List<Image> fills;
    private MouseLook mouseLook;
    private Image stimpackFill;
    private Image dashFill;
    private Image chargeFill;
    private Image AOEFill;
    private Bow bow;
    [SerializeField] static Color normalColor = new Color(0f / 255f, 0f / 255f, 0f / 255f, 188f / 255f);
    [SerializeField] static Color highlightedColor = new Color(224f / 255f, 114f / 255f, 0f / 255f, 255f / 255f);
    private static Image[] radialOptions;

    private static string selectedOption;
    private bool stimpackFilling;
    private bool dashFilling;
    private bool chargeFilling;

    private Vector2 moveInput;

    private static bool inArrowMenu = false;

    public static UIManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }


    void Update()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;

        FillingAbility(dashFill, 2.4f);
        FillingAbility(stimpackFill, 10f);
        FillingAbility(chargeFill, 12.5f);
        FillingAbility(AOEFill, 15f);
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        ui = GameObject.Find("UI");

        if(ui == null)
        {
            return;
        }

        if(ui.transform.Find("NinjaUI") != null)
        {
            //if current player is a ninja, it gets the ninja assets
            fills = new List<Image>();
            texts = ui.transform.Find("NinjaUI").Find("AmmoCounter").GetComponentsInChildren<TextMeshProUGUI>();
            ninjaUI = ui.transform.Find("NinjaUI").gameObject;
           // flashUI = ui.transform.Find("FlashUI").gameObject;
            healthSlider = ninjaUI.transform.Find("HealthBar").GetComponent<Slider>();
            arrowSelect = ui.transform.Find("NinjaUI").Find("ArrowSelect").gameObject;
            arrowSelect = arrowSelect.transform.Find("Wheel").gameObject;
            mouseLook = GameObject.Find("NinjaPlayer").transform.Find("Main Camera").GetComponent<MouseLook>();
            stimpackFill = ninjaUI.transform.Find("Stimpack").Find("StimpackFill").GetComponent<Image>();
            fills.Add(stimpackFill);

            radialOptions = new Image[arrowSelect.transform.childCount];
            for(int i = 0; i < arrowSelect.transform.childCount; i++)
            {
                radialOptions[i] = arrowSelect.transform.GetChild(i).GetComponent<Image>();
            }
        }
        else
        {
            //if current player is a knight, it gets the knight assets
            fills = new List<Image>();
            knightUI = ui.transform.Find("KnightUI").gameObject;
            grenadeCount = knightUI.transform.Find("Grenade").Find("GrenadeCount").GetComponent<TextMeshProUGUI>();
            texts = knightUI.transform.Find("AmmoCounter").GetComponentsInChildren<TextMeshProUGUI>();
            knightSlider = knightUI.transform.Find("FuelSlider").GetComponent<Slider>();
            healthSlider = knightUI.transform.Find("HealthBar").GetComponent<Slider>();
            knightScopeOverlay = ui.transform.Find("ScopeOverlay").gameObject;
            stimpackFill = knightUI.transform.Find("Stimpack").Find("StimpackFill").GetComponent<Image>();
            fills.Add(stimpackFill);
            dashFill = knightUI.transform.Find("Dash").Find("DashFill").GetComponent<Image>();
            fills.Add(dashFill);
            chargeFill = knightUI.transform.Find("Charge").Find("ChargeFill").GetComponent<Image>();
            fills.Add(chargeFill);
            AOEFill = knightUI.transform.Find("AOE").Find("AOEFill").GetComponent<Image>();
            fills.Add(AOEFill);
        }

        //gets and sets the number of current and max ammo
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

    public void SetArrowMenuState(bool state)
    {
        if (bow == null)
        {
            bow = GameObject.Find("NinjaPlayer").transform.Find("Main Camera").Find("Bow").GetComponent<Bow>();
        }

        arrowSelect.SetActive(state);
        inArrowMenu = state;

        if (state)
        {
            Cursor.lockState = CursorLockMode.None;
            mouseLook.SetCanLook(false);

            foreach (TextMeshProUGUI text in texts)
            {
                text.gameObject.SetActive(false);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseLook.SetCanLook(true);

            foreach (TextMeshProUGUI text in texts)
            {
                text.gameObject.SetActive(true);
            }

            bow.SetCurrentArrow(selectedOption);
        }
    }
    public bool GetArrowMenuState()
    {
        return inArrowMenu;
    }

    public void SetMaxAmmo(int ammo)
    {
        maxAmmo.text = ammo.ToString();
    }

    public void SetCurrentAmmo(int ammo)
    {
        currentAmmo.text = ammo.ToString();
    }

    public void SetGrenadeCount(int count)
    {
        grenadeCount.text = count.ToString();
    }

    public void SetKnightSliderValue(float value)
    {
        knightSlider.value = value;
    }

    public void SetKnightSliderColour(Color color)
    {
        GameObject handle = knightSlider.transform.GetChild(2).GetChild(0).gameObject;
        Image handleImage = handle.GetComponent<Image>();
        handleImage.color = color;
    }

    public void SetKnightScopeOverlayActive(bool state)
    {
        knightScopeOverlay.SetActive(state);
    }

    public void SetKnightUIActive(bool state)
    {
        knightUI.SetActive(state);
    }
 
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health; 
    }

    //Resets the fill value to 0 and starts refiling it
    public void ResetFill(string name)
    {
        foreach(Image fill in fills)
        {
            if(fill.name.Equals(name))
            {
                fill.fillAmount = 0f;
            }
        }
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
