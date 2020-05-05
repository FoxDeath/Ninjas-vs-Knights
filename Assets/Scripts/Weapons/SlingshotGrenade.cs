﻿using System.Collections;
using UnityEngine;

public class SlingshotGrenade : MonoBehaviour
{
    [SerializeField] GameObject expEffect;
    private AudioManager audioManager;

    [SerializeField] float delay = 3f;
    [SerializeField] float radius = 15f;
    [SerializeField] float force = 500f;
    [SerializeField] float damage = 20f;
    private float countdown;

    private bool exploded;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        countdown = delay;
    }

    private void FixedUpdate()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0f && !exploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        exploded = true;

        Instantiate(expEffect, transform.position, transform.rotation);

        GameObject emptyWithAudioSource = new GameObject();
        emptyWithAudioSource.AddComponent<AudioSource>();

        GameObject expSound = Instantiate(emptyWithAudioSource, transform.position, transform.rotation);

        audioManager.Play("GrenadeExplode", expSound.GetComponent<AudioSource>());

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider collider in colliders)
        {
            Target target = collider.GetComponent<Target>();

            if(target != null)
            {
                target.TakeDamage(damage);
            }

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);
        StartCoroutine(DestroyAfterSeconds(2f, expSound));
    }

    IEnumerator DestroyAfterSeconds(float seconds, GameObject gameObjectToDestroy) 
    {
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!exploded)
        {
            audioManager.Play("GrenadeBounce", GetComponent<AudioSource>());
        }
    }
}