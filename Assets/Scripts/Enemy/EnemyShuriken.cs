using UnityEngine;

public class EnemyShuriken : MonoBehaviour
{
    private GameObject anchor;
    private Rigidbody myRigidbody;

    [SerializeField] float damage = 10f;

    private bool hit = false;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    //The EnemyShuriken will move towards the targets last known position.
    void FixedUpdate()
    {
        if(!anchor)
        {
            if(!hit)
            {
                transform.rotation = Quaternion.LookRotation(myRigidbody.velocity);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
        else
        {
            transform.position = anchor.transform.position;
            transform.rotation = anchor.transform.rotation;
        }
    }

    //When the EnemyShuriken hits the Player, the Player will take damage.
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            FindObjectOfType<AudioManager>().NetworkPlay("ShurikenHit", GetComponent<AudioSource>());
            other.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
            Destroy(transform.parent.gameObject, 0.1f);
        }

        if(other.gameObject.tag.Equals("EnemyObjective"))
        {
            // FindObjectOfType<AudioManager>().Play("ShurikenHit", GetComponent<AudioSource>());
            other.gameObject.GetComponentInParent<Objective>().TakeDamage(damage);
        }
        
        if(other.gameObject.layer != LayerMask.NameToLayer("Player") && other.gameObject.layer != LayerMask.NameToLayer("Enemy") && !other.gameObject.tag.Equals("Ammo"))
        {
            hit = true;
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<AudioManager>().NetworkPlay("ShurikenHit", GetComponent<AudioSource>());
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            myRigidbody.isKinematic = true;

            GameObject anchor = new GameObject("Shuriken Anchor");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = myRigidbody.transform;
            this.anchor = anchor;

            Destroy(anchor, 10f);
            Destroy(transform.parent.gameObject, 10f);
        }
    }
}
