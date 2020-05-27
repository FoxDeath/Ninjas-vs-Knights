using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class AOEattack : MonoBehaviour
{
    [SerializeField] float range = 20f;
    [SerializeField] float damage = 20f;
    [SerializeField] float upwardsForce = 50f;
    [SerializeField] float pushForce = 500f;

    private bool attacking;
    public ParticleSystem aoeParticles;

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

        //If we are on the ground cast it
        //If we are not, slam the ground and then cast it with multiplyers
        if(GetComponent<KnightPlayerMovement>().GetGrounded())
        {
            Attack(1f, 1f);
        }
        else
        {
            GetComponent<KnightPlayerMovement>().SetJetpackOn(false);
            GetComponent<KnightPlayerMovement>().Slam();
            yield return new WaitWhile(() => !GetComponent<KnightPlayerMovement>().GetGrounded());
            Attack(2f, 2f);
        }

        //15 second cooldown
        FindObjectOfType<UIManager>().ResetFill("AOEFill");
        yield return new WaitForSeconds(15f);

        //End cooldown
        attacking = false;
    }

    private void Attack(float damageMutliplyer, float forceMultiplyer)
    {
        aoeParticles.Play();

        //Get all object hit by the ability
        var colliders = Physics.OverlapSphere(transform.position, range, Physics.AllLayers);

        //Foreach object check is they have a Target Component, if they do they take damage and knockback/up
        foreach (var item in colliders)
        {
            Target target = item.transform.GetComponent<Target>();

            //Adding damage if the object has a Target Component
            if (target)
            {
                target.TakeDamage(damage * damageMutliplyer);
            }

            //Adding force for knockback/up if the object has a RigidBody Component
            if (item.attachedRigidbody)
            {
                Vector3 force = (transform.position - item.transform.position) * -1;
                force.Normalize();
                force *= pushForce;
                force.y = upwardsForce;
                item.attachedRigidbody.AddForce(force * forceMultiplyer);
            }
        }
    }
}