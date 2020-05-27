using System.Collections;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject flashEffect;
    private GameObject anchor;

    private Rigidbody rigidBody;
    private Animator anim;

    [SerializeField] float radius = 20f;
    [SerializeField] float delay = 3f;
    [SerializeField] float damage = 5f;
    [SerializeField] float effectAngle = 100f;
    private float countdown;

    private bool exploaded = false;

    void Start()
    {
        countdown = delay;
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !exploaded)
        {
            exploaded = true;
            Flash();
        }

        if (!anchor)
        {
            if(rigidBody.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(rigidBody.velocity);
            }
        }
        else
        {
            transform.position = anchor.transform.position;
            transform.rotation = anchor.transform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player") && !collision.gameObject.tag.Equals("Ammo"))
        {
            GetComponent<Collider>().enabled = false;
            rigidBody.velocity = Vector3.zero;
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rigidBody.isKinematic = true;

            GameObject anchor = new GameObject("Kunai Anchor");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor;

            Destroy(anchor, 5f);

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !exploaded)
        {
            collision.gameObject.GetComponent<Target>().TakeDamage(damage);
        }
    }
    
    public void Flash()
    {
        Instantiate(flashEffect, transform.position, transform.rotation);

        //creates a bubble, which detects everything inside of its radius
        Collider[] effected = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearObject in effected)
        {
            if(Vector3.Angle(nearObject.transform.forward, transform.position - nearObject.transform.position) < effectAngle)
            {
                RaycastHit hit;
                
                if(Physics.Raycast(transform.position, nearObject.transform.position - transform.position, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    //change this if mirror fucks it up
                    GameObject.Find("UI").transform.Find("Flash").GetComponent<Animator>().SetTrigger("Flash");
                }

                if(Physics.Raycast(transform.position, nearObject.transform.position - transform.position, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if(nearObject.GetComponent<EnemyAttack>())
                    {
                        nearObject.GetComponent<EnemyAttack>().SetFlashed(true);
                    }
                }
            }
        }

        Destroy(gameObject);
    }
}

