using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [HideInInspector] public List<NinjaUI> ninjaUIs = new List<NinjaUI>();
    [HideInInspector] public List<KnightUI> knightUIs = new List<KnightUI>();

    //Returns the singleton instance of the class.
    public static UIManager GetInstance()
    {
        return instance;
    }

    //Makes sure there is only one instance at a time.
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

    public void GameOver()
    {
        foreach (NinjaUI ui in ninjaUIs)
        {
            if(!ui.GetComponentInParent<PlayerMovement>().isServer)
            {
                return;
            }

            if(ui.deathScreen.activeInHierarchy)
            {
                ui.deathScreen.SetActive(false);
            }

            ui.ninjaInGameUI.SetActive(false);
            ui.gameOverScreen.SetActive(true);
        }

        foreach (KnightUI ui in knightUIs)
        {
            if(!ui.GetComponentInParent<PlayerMovement>().isServer)
            {
                return;
            }

            if(ui.deathScreen.activeInHierarchy)
            {
                ui.deathScreen.SetActive(false);
            }

            ui.knightInGameUI.SetActive(false);
            ui.gameOverScreen.SetActive(true);
        }
    }

    public void PlayerDeath(NinjaUI ninjaUI = null, KnightUI knightUI = null)
    {
        if(ninjaUI)
        {
            ninjaUI.ninjaInGameUI.SetActive(false);
            ninjaUI.deathScreen.SetActive(true);
        }
        
        if(knightUI)
        {
            knightUI.knightInGameUI.SetActive(false);
            knightUI.deathScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        foreach(NinjaUI ui in ninjaUIs)
        {
            ui.ninjaInGameUI.SetActive(false);
            ui.gameOverScreen.SetActive(true);
            ui.Awake();
        }

        foreach(KnightUI ui in knightUIs)
        {
            ui.knightInGameUI.SetActive(false);
            ui.gameOverScreen.SetActive(true);
            ui.Awake();
        }
    }

    public void AddNinjaUI(NinjaUI ui)
    {
        ninjaUIs.Add(ui);
    }

    public void RemoveNinjaUI(NinjaUI ui)
    {
        if(ninjaUIs.Contains(ui))
        {
            ninjaUIs.Remove(ui);
        }
    }

    public void AddKnightUI(KnightUI ui)
    {
        knightUIs.Add(ui);
    }

    public void RemoveKnightUI(KnightUI ui)
    {
        if (knightUIs.Contains(ui))
        {
            knightUIs.Remove(ui);
        }
    }

    public void SetWaveCounter(string waveNr, bool countDown, NinjaUI ninjaUI = null, KnightUI knightUI = null)
    {
        if (ninjaUI != null)
        {
            if(countDown)
            {
                ninjaUI.waveCounter.color = Color.red;
            }
            else
            {
                ninjaUI.waveCounter.color = ninjaUI.ogWaveNrColor;
            }

            ninjaUI.waveCounter.text = waveNr;
        }
        else if (knightUI != null)
        {
            if (countDown)
            {
                knightUI.waveCounter.color = Color.red;
            }
            else
            {
                knightUI.waveCounter.color = knightUI.ogWaveNrColor;
            }

            knightUI.waveCounter.text = waveNr;
        }
        else
        {
            return;
        }
    }

    //Turns the radial menu and the Ninja UI on\off accordingly.
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

    //Checks if the radial menu is open or not.
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

    //Sets the maximum ammo UI.
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

    //Updates the current ammo UI.
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

    //Updates the grenade UI
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

    //Sets the jetpack UI.
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

    //Sets the knights scoping overlay.
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

    //Turns knight UI on/off.
    public void SetKnightUIActive(bool state, KnightUI knightUI = null)
    {
        if(knightUI != null)
        {
            knightUI.knightInGameUI.SetActive(state);
        }
        else
        {
            return;
        }
    }
 
    //Updates the max health UI.
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

    //Updates current health UI.
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
