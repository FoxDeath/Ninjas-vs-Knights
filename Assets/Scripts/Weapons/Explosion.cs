using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Collider collider;

    [SerializeField] float damage;

    void Start()
    {
        collider = GetComponent<Collider>();
        Destroy(gameObject, 0.3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Target>().TakeDamage(damage);
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") && collision.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Wall"))
        {
            Physics.IgnoreCollision(collider, collision.collider, true);
        }
    }
}
