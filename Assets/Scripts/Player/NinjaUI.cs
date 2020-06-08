using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class NinjaUI : MonoBehaviour
{
    [HideInInspector] public GameObject ninjaInGameUI;
    [HideInInspector] public GameObject arrowSelect;
    [HideInInspector] public GameObject gameOverScreen;
    [HideInInspector] public GameObject deathScreen;

    [HideInInspector] public TextMeshProUGUI waveCounter;
    [HideInInspector] public TextMeshProUGUI grenadeCount;
    [HideInInspector] public TextMeshProUGUI currentAmmo;
    [HideInInspector] public TextMeshProUGUI maxAmmo;
    [HideInInspector] public TextMeshProUGUI[] texts;


    [HideInInspector] public Color ogWaveNrColor;
    [HideInInspector] public Slider healthSlider;
    [HideInInspector] public List<Image> fills;
    [HideInInspector] public MouseLook mouseLook;
    [HideInInspector] public Image stimpackFill;


    [HideInInspector] public Bow bow;
    [SerializeField] static Color normalColor = new Color(0f / 255f, 0f / 255f, 0f / 255f, 188f / 255f);
    [SerializeField] static Color highlightedColor = new Color(224f / 255f, 114f / 255f, 0f / 255f, 255f / 255f);
    public static Image[] radialOptions;

    [HideInInspector] public Vector2 moveInput;

    [HideInInspector] public string selectedOption;

    [HideInInspector] public bool inArrowMenu = false;

    public void Awake() 
    {
        if(transform.Find("NinjaInGameUI") != null)
        {
            //if current player is a ninja, it gets the ninja assets
            fills = new List<Image>();
            ninjaInGameUI = transform.Find("NinjaInGameUI").gameObject;
            gameOverScreen = transform.Find("GameOverScreen").gameObject;
            deathScreen = transform.Find("DeathScreen").gameObject;
            texts = ninjaInGameUI.transform.Find("AmmoCounter").GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI text in texts)
            {
                if (text.name.Equals("CurrentAmmo"))
                {
                    currentAmmo = text;
                }
                else if (text.name.Equals("MaxAmmo"))
                {
                    maxAmmo = text;
                }
            }

            waveCounter = ninjaInGameUI.transform.Find("WaveCounter").GetComponent<TextMeshProUGUI>();
            ogWaveNrColor = waveCounter.color;
            grenadeCount = ninjaInGameUI.transform.Find("Grenade").Find("GrenadeCount").GetComponent<TextMeshProUGUI>();
            healthSlider = ninjaInGameUI.transform.Find("HealthBar").GetComponent<Slider>();
            arrowSelect = ninjaInGameUI.transform.Find("ArrowSelect").gameObject;
            arrowSelect = arrowSelect.transform.Find("Wheel").gameObject;
            mouseLook = transform.GetComponentInParent<MouseLook>();
            stimpackFill = ninjaInGameUI.transform.Find("Stimpack").Find("StimpackFill").GetComponent<Image>();
            fills.Add(stimpackFill);

            radialOptions = new Image[arrowSelect.transform.childCount];

            for(int i = 0; i < arrowSelect.transform.childCount; i++)
            {
                radialOptions[i] = arrowSelect.transform.GetChild(i).GetComponent<Image>();
            }
        }
        FindObjectOfType<UIManager>().AddNinjaUI(this);
    }

    public void SetWaveUI() 
    {
        FindObjectOfType<UIManager>().SetWaveCounter(0.ToString(), false, this, null);
    }

    void OnDestroy()
    {
        if(FindObjectOfType<UIManager>())
        {
            FindObjectOfType<UIManager>().RemoveNinjaUI(GetComponent<NinjaUI>());
        }
    }

    public void ArrowMenuInput(InputAction.CallbackContext context)
    {
        if (inArrowMenu)
        {
            moveInput.x = context.ReadValue<Vector2>().x - (Screen.width / 2f);
            moveInput.y = context.ReadValue<Vector2>().y - (Screen.height / 2f);
            moveInput.Normalize();

            if(moveInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveInput.y, -moveInput.x) / Mathf.PI;
                angle *= 180f;
                angle += 90f;

                if(angle < 0f)
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
