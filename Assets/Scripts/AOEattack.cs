using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class AOEAttack : MonoBehaviour
{
    [SerializeField] float range = 20f;
    [SerializeField] float damage = 20f;
    [SerializeField] float upwardsForce = 50f;
    [SerializeField] float pushForce = 500f;

    private bool attacking;

    public void AOEInput(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed && !attacking)
        {
            StartCoroutine(AOEAttackBehaviour());
        }
    }

    //Casting the abilty
    IEnumerator AOEAttackBehaviour() 
    {
        //Start cooldown
        attacking = true;

        //Get all object hit by the ability
        var colliders = Physics.OverlapSphere(transform.position, range, Physics.AllLayers);

        //Foreach object check is they have a Target Component, if they do they take damage and knockback/up
        foreach(var item in colliders)
        {
            Target target = item.transform.GetComponent<Target>();

            //Adding damage if the object has a Target Component
            if(target)
            {
                target.TakeDamage(damage);
            }

            //Adding force for knockback/up if the object has a RigidBody Component
            if(item.attachedRigidbody)
            {
                Vector3 force = (transform.position - item.transform.position) * -1;
                force.Normalize();
                force *= pushForce;
                force.y = upwardsForce;
                item.attachedRigidbody.AddForce(force);
            }
        }

        //15 second cooldown
        yield return new WaitForSeconds(15f);

        //End cooldown
        attacking = false;
    }
}