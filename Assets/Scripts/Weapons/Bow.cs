using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Bow : MonoBehaviour, IWeapon
{
    private UIManager uiManager;

    [SerializeField] GameObject regularArrowObj;
    [SerializeField] GameObject fireArrowObj;
    [SerializeField] GameObject slowArrowObj;
    [SerializeField] GameObject explosiveArrowObj;
    [SerializeField] GameObject emmiter;

    public enum arrowTypes
    {
        Regular,
        Fire,
        Slow,
        Explosion
    }

    private arrowTypes currentType;

    private float charge;
    [SerializeField] float chargeMax;
    [SerializeField] float chargeRate;

    [SerializeField] int maxArrows = 10;
    private int currentRegularArrows;
    private int currentFireArrows;
    private int currentSlowArrows;
    private int currentExplosiveArrows;

    private bool charging;

    void Start()
    {
        charging = false;
        charge = 0f;
        currentType = arrowTypes.Regular;
        currentRegularArrows = maxArrows;
        currentFireArrows = maxArrows;
        currentSlowArrows = maxArrows;
        currentExplosiveArrows = maxArrows;
        UIManager.GetInstance().SetMaxAmmo(maxArrows);
        UIManager.GetInstance().SetCurrentAmmo(currentRegularArrows);
    }

    void Update()
    {
        if (charging && charge < chargeMax)
        {
            charge += Time.deltaTime * chargeRate;
        }
    }

    public void FireInput(InputAction.CallbackContext context)
    {
        if ((currentType == arrowTypes.Explosion && currentExplosiveArrows > 0) || (currentType == arrowTypes.Fire && currentFireArrows > 0) ||
            (currentType == arrowTypes.Regular && currentRegularArrows > 0) || (currentType == arrowTypes.Slow && currentSlowArrows > 0))
        {
            if (!UIManager.GetInstance().GetArrowMenuState())
            {
                if (context.interaction is HoldInteraction)
                {
                    if (context.action.phase == InputActionPhase.Started)
                    {
                        charging = true;
                    }
                    else if (context.action.phase == InputActionPhase.Canceled)
                    {
                        Rigidbody arrow = null;

                        switch (currentType)
                        {
                            case arrowTypes.Regular:
                                currentRegularArrows--;
                                UIManager.GetInstance().SetCurrentAmmo(currentRegularArrows);
                                arrow = Instantiate(regularArrowObj, emmiter.transform.position, emmiter.transform.rotation).GetComponent<Rigidbody>();
                                arrow.AddForce(emmiter.transform.forward * charge, ForceMode.Impulse);
                                break;

                            case arrowTypes.Fire:
                                currentFireArrows--;
                                UIManager.GetInstance().SetCurrentAmmo(currentFireArrows);
                                arrow = Instantiate(fireArrowObj, emmiter.transform.position, emmiter.transform.rotation).GetComponent<Rigidbody>();
                                arrow.AddForce(emmiter.transform.forward * charge, ForceMode.Impulse);
                                break;

                            case arrowTypes.Explosion:
                                currentExplosiveArrows--;
                                UIManager.GetInstance().SetCurrentAmmo(currentExplosiveArrows);
                                arrow = Instantiate(explosiveArrowObj, emmiter.transform.position, emmiter.transform.rotation).GetComponent<Rigidbody>();
                                arrow.AddForce(emmiter.transform.forward * charge, ForceMode.Impulse);
                                break;

                            case arrowTypes.Slow:
                                currentSlowArrows--;
                                UIManager.GetInstance().SetCurrentAmmo(currentSlowArrows);
                                arrow = Instantiate(slowArrowObj, emmiter.transform.position, emmiter.transform.rotation).GetComponent<Rigidbody>();
                                arrow.AddForce(emmiter.transform.forward * charge, ForceMode.Impulse);
                                break;
                        }

                        charging = false;
                        charge = 0f;
                    }
                }
            }
        }
    }

    public void SwitchArrowInput(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            UIManager.GetInstance().SetArrowMenuState(true);
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            UIManager.GetInstance().SetArrowMenuState(false);
        }
    }

    public void SetCurrentArrow(string name)
    {
        switch(name)
        {
            case "Fire":
                currentType = arrowTypes.Fire;
                UIManager.GetInstance().SetCurrentAmmo(currentFireArrows);
                break;

            case "Regular":
                currentType = arrowTypes.Regular;
                UIManager.GetInstance().SetCurrentAmmo(currentRegularArrows);
                break;

            case "Slow":
                currentType = arrowTypes.Slow;
                UIManager.GetInstance().SetCurrentAmmo(currentSlowArrows);
                break;
                
            case "Explosive":
                currentType = arrowTypes.Explosion;
                UIManager.GetInstance().SetCurrentAmmo(currentExplosiveArrows);
                break;
        }
    }
}
