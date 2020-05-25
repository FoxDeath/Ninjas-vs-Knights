using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bow;

public class Arrow : MonoBehaviour
{
    private Rigidbody myRigidbody;

    private GameObject anchor;
    [SerializeField] GameObject explosion;

    [SerializeField] float damage = 10f;
    [SerializeField] float duration = 5f;

    [SerializeField] arrowTypes type = arrowTypes.Regular;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!anchor)
        {
            if(myRigidbody.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(myRigidbody.velocity);
            }
        }
        else
        {
            transform.position = anchor.transform.position;
            transform.rotation = anchor.transform.rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            switch(type)
            {
                case arrowTypes.Regular:
                    collision.gameObject.GetComponent<Target>().TakeDamage(damage);
                    break;

                case arrowTypes.Fire:
                    collision.gameObject.GetComponent<Target>().SetOnFire(duration, damage);
                    break;

                case arrowTypes.Slow:
                    collision.gameObject.GetComponent<Target>().SlowDown(duration);
                    break;
            }
        }

        if(type == arrowTypes.Explosion)
        {
            Instantiate(explosion, collision.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer != LayerMask.NameToLayer("Player") && !collision.gameObject.tag.Equals("Ammo"))
        {
            GetComponentInChildren<Collider>().enabled = false;
            //FindObjectOfType<AudioManager>().Play("ShurikenHit", GetComponent<AudioSource>());
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            myRigidbody.isKinematic = true;

            GameObject anchor = new GameObject("Arrow Anchor");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor;

            Destroy(anchor, 10f);
            Destroy(gameObject, 10f);
        }
    }
}
