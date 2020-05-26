using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private GameObject anchor;

    private Rigidbody rigidBody;

    [SerializeField] float damage = 10f;

    private bool hit = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!anchor)
        {
            if(!hit)
            {
                transform.rotation = Quaternion.LookRotation(rigidBody.velocity);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = anchor.transform.position;
            transform.rotation = anchor.transform.rotation;
        }
    }

    //The shuriken will do damage when it collides with the target.
    //The shuriken does not collide with itself, and it gets destroyed after a few seconds.
    void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.transform.GetComponent<Target>();

        if(target)
        {
            target.TakeDamage(damage);
        }

        if (collision.gameObject.layer != LayerMask.NameToLayer("Player") && !collision.gameObject.tag.Equals("Ammo"))
        {
            hit = true;
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<AudioManager>().Play("ShurikenHit", GetComponent<AudioSource>());
            rigidBody.velocity = Vector3.zero;
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rigidBody.isKinematic = true;
            
            GameObject anchor = new GameObject("Shuriken Anchor");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor;

            Destroy(anchor, 10f);
            Destroy(gameObject, 10f);
        }
    }
}
