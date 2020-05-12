using System.Collections;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject flashEffect;
    private GameObject anchor;

    public EnemyAttack enemyFlash;
    private Rigidbody rigidBody;
    private Animator anim;

    [SerializeField] float radius = 20f;
    [SerializeField] float delay = 3f;
    [SerializeField] float damage = 7f;
    private float countdown;

    private bool exploaded = false;

    void Start()
    {
        countdown = delay;
        anim = GameObject.Find("Flash").GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !exploaded)
        {
            Flash();
            exploaded = true;            
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
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.isKinematic = true;

            GameObject anchor = new GameObject("Kunai Anchor");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor;

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
        foreach (Collider nearObjects in effected)
        {
            if (nearObjects.gameObject.layer == LayerMask.NameToLayer("Player") || nearObjects.gameObject.layer == LayerMask.NameToLayer("Enemy") && !exploaded)
            {
                anim.SetTrigger("Flash");                
            }
            Destroy(gameObject);           
        }
        EnemyAttack[] enemies = (EnemyAttack[])GameObject.FindObjectsOfType(typeof(EnemyAttack));
        foreach (EnemyAttack enemy in enemies)
        {
            enemy.SetFlashed(true);           
        }
    }      
}

