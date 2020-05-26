using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyProjectile : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] float damage = 20f;

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
