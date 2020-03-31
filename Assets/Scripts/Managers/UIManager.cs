using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameObject ui;
    private GameObject knightScopeOverlay;
    private GameObject knightUI;
    private GameObject ninjaUI;

    private TextMeshProUGUI currentAmmo;
    private TextMeshProUGUI maxAmmo;
    private TextMeshProUGUI[] texts;
    private Slider knightSlider;
    private Slider healthSlider;

    void Update()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
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
            texts = ui.transform.Find("NinjaUI").Find("AmmoCounter").GetComponentsInChildren<TextMeshProUGUI>();
            ninjaUI = ui.transform.Find("NinjaUI").gameObject;
            healthSlider = ninjaUI.transform.Find("HealthBar").GetComponent<Slider>();
        }
        else
        {
            //if current player is a knight, it gets the ninja assets
            knightUI = ui.transform.Find("KnightUI").gameObject;
            texts = knightUI.transform.Find("AmmoCounter").GetComponentsInChildren<TextMeshProUGUI>();
            knightSlider = knightUI.transform.Find("FuelSlider").GetComponent<Slider>();
            healthSlider = knightUI.transform.Find("HealthBar").GetComponent<Slider>();
            knightScopeOverlay = ui.transform.Find("ScopeOverlay").gameObject;
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

    public void SetMaxAmmo(int ammo)
    {
        maxAmmo.text = ammo.ToString();
    }

    public void SetCurrentAmmo(int ammo)
    {
        currentAmmo.text = ammo.ToString();
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
}
