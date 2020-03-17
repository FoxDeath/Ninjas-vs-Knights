using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AOEattack : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float upwardsForce;
    [SerializeField] float pushForce;
    private bool canAttack = true;
    public void AOEInput(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed && canAttack)
        {
            StartCoroutine("AOEAttack");
        }
    }

    IEnumerator AOEAttack() {

        canAttack = false;

        var colliders = Physics.OverlapSphere(transform.position, range, Physics.AllLayers);

        foreach (var item in colliders)
        {
            Target target = item.transform.GetComponent<Target>();
            if (target)
            {
                target.TakeDamage(damage);
            }

            if (item.attachedRigidbody)
            {
                Vector3 force = (transform.position - item.transform.position) * -1;
                force.Normalize();
                force *= pushForce;
                force.y = upwardsForce;
                item.attachedRigidbody.AddForce(force);
            }
        }

        yield return new WaitForSeconds(15f);

        canAttack = true;
    }
}