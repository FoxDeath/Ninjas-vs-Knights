using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetArrowMenuState(bool state, NinjaUI ninjaUI = null)
    {
        if(ninjaUI != null)
        {
            if (ninjaUI.bow == null)
            {
                ninjaUI.bow = GameObject.Find("NinjaPlayer").transform.Find("Main Camera").Find("Bow").GetComponent<Bow>();
            }

            ninjaUI.arrowSelect.SetActive(state);
            ninjaUI.inArrowMenu = state;

            if (state)
            {
                Cursor.lockState = CursorLockMode.None;
                ninjaUI.mouseLook.SetCanLook(false);

                foreach (TextMeshProUGUI text in ninjaUI.texts)
                {
                    text.gameObject.SetActive(false);
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                ninjaUI.mouseLook.SetCanLook(true);

                foreach (TextMeshProUGUI text in ninjaUI.texts)
                {
                    text.gameObject.SetActive(true);
                }
                ninjaUI.bow.SetCurrentArrow(ninjaUI.selectedOption);
            }
        }
        else
        {
            return;
        }
    }

    public bool GetArrowMenuState(NinjaUI ninjaUI = null)
    {
        if(ninjaUI != null)
        {
            return ninjaUI.inArrowMenu;
        }
        else
        {
            return false;
        }
    }

    public void SetMaxAmmo(int ammo, NinjaUI ninjaUI = null, KnightUI knightUI = null)
    {
        if(ninjaUI != null)
        {
            ninjaUI.maxAmmo.text = ammo.ToString();
        }
        else if(knightUI != null)
        {
            knightUI.maxAmmo.text = ammo.ToString();
        }
        else
        {
            return;
        }
    }

    public void SetCurrentAmmo(int ammo, NinjaUI ninjaUI = null, KnightUI knightUI = null)
    {
        if(ninjaUI != null)
        {
            ninjaUI.currentAmmo.text = ammo.ToString();
        }
        else if(knightUI != null)
        {
            knightUI.currentAmmo.text = ammo.ToString();
        }
        else
        {
            return;
        }
    }

    public void SetGrenadeCount(int count, NinjaUI ninjaUI = null, KnightUI knightUI = null)
    {
        if(ninjaUI != null)
        {
            ninjaUI.grenadeCount.text = count.ToString();
        }
        else if(knightUI != null)
        {
            knightUI.grenadeCount.text = count.ToString();
        }
        else
        {
            return;
        }
    }

    public void SetKnightSliderValue(float value, KnightUI knightUI = null)
    {
        if(knightUI != null)
        {
            knightUI.knightSlider.value = value;
        }
        else
        {
            return;
        }
    }

    public void SetKnightScopeOverlayActive(bool state, KnightUI knightUI = null)
    {
        if(knightUI != null)
        {
            knightUI.knightScopeOverlay.SetActive(state);
        }
        else
        {
            return;
        }
    }

    public void SetKnightUIActive(bool state, KnightUI knightUI = null)
    {
        if(knightUI != null)
        {
            knightUI.knightUI.SetActive(state);
        }
        else
        {
            return;
        }
    }
 
    public void SetMaxHealth(float health, NinjaUI ninjaUI = null, KnightUI knightUI = null)
    {
        if(ninjaUI != null)
        {   
            ninjaUI.healthSlider.maxValue = health;
            ninjaUI.healthSlider.value = health;
        }
        else if(knightUI != null)
        {  
            knightUI.healthSlider.maxValue = health;
            knightUI.healthSlider.value = health;
        }
        else
        {
            return;
        }
    }

    public void SetHealth(float health, NinjaUI ninjaUI = null, KnightUI knightUI = null)
    {
        if(ninjaUI != null)
        {   
            ninjaUI.healthSlider.value = health;
        }
        else if(knightUI != null)
        {  
            knightUI.healthSlider.value = health;
        }
        else
        {
            return;
        }
    }

    //Resets the fill value to 0 and starts refiling it
    public void ResetFill(string name, NinjaUI ninjaUI = null, KnightUI knightUI = null)
    {
        if(ninjaUI != null)
        { 
            foreach(Image fill in ninjaUI.fills)
            {
                if(fill.name.Equals(name))
                {
                    fill.fillAmount = 0f;
                }
            }
        }
        else if(knightUI != null)
        {
            foreach(Image fill in knightUI.fills)
            {
                if(fill.name.Equals(name))
                {
                    fill.fillAmount = 0f;
                }
            }
        }
        else
        {
            return;
        }
    }
}
