using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private SphereCollider collider;

    [SerializeField] float damage = 15;
    [SerializeField] float explosionForce = 1000;
    [SerializeField] float upwardsModifier = 150;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<Target>().TakeDamage(damage);
            other.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, collider.radius, upwardsModifier);

        }
    }
}
