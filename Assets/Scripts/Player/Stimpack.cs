using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stimpack : MonoBehaviour
{
    private Health health;

    private bool stimpacking = false;

    void Awake()
    {
        health = FindObjectOfType<Health>();
    }

    [SerializeField] float healPercentage = 75f;
    public void StimpackInput(InputAction.CallbackContext context)
    {   
        if(context.action.phase == InputActionPhase.Performed && !stimpacking)
        {
            StartCoroutine(StimpackBehaviour());
        }
    }

    //Casting the abilty
    IEnumerator StimpackBehaviour()
    {
        if(health.GetMaxHealt() == health.GetCurrentHealth())
        {
            yield return null;
        }
        else
        {
            stimpacking = true;

            health.Heal(GetComponent<Health>().GetMaxHealt() * (healPercentage / 100));
            FindObjectOfType<UIManager>().ResetFill("StimpackFill");
            yield return new WaitForSeconds(10f);

            stimpacking = false;
        }
    }
}
