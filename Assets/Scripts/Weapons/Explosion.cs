using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Explosion : MonoBehaviour
{
    private SphereCollider myCollider;

    [SerializeField] float damage = 15;
    [SerializeField] float explosionForce = 450;

    void Awake()
    {
        FindObjectOfType<AudioManager>().NetworkPlay("GrenadeExplode", GetComponent<AudioSource>());
        myCollider = GetComponent<SphereCollider>();
    }

    void Start()
    {
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Target>().StartExploding(damage, explosionForce, transform.position, myCollider.radius, other.gameObject);
        }
    }
}
