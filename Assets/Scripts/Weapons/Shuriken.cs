using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private GameObject anchor;

    private Rigidbody rigidBody;

    [SerializeField] float damage = 10f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!anchor)
        {
            transform.rotation = Quaternion.LookRotation(rigidBody.velocity);
        }
        else
        {
            transform.position = anchor.transform.position;
            transform.rotation = anchor.transform.rotation;
        }
    }

    //The shuriken will do damage when it collides with the target.
    //The shuriken does not collide with itself, and it gets destroyed after a few seconds.
    private void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.transform.GetComponent<Target>();

        if(target)
        {
            target.TakeDamage(damage);
        }

        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<AudioManager>().Play("ShurikenHit", GetComponent<AudioSource>());
            rigidBody.velocity = Vector3.zero;
            rigidBody.isKinematic = true;

            GameObject anchor = new GameObject("Shuriken Anchor");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor;

            Destroy(gameObject, 10f);
        }
    }
}
