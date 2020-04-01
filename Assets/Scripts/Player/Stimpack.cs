using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stimpack : MonoBehaviour
{
    private bool stimpacking;

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
        stimpacking = true;

        GetComponent<Health>().Heal(GetComponent<Health>().GetMaxHealt() * (healPercentage / 100));

        yield return new WaitForSeconds(10f);

        stimpacking = false;
    }
}
