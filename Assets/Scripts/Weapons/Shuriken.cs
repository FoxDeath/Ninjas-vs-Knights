using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField] float damage = 10f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
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

        if(collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<AudioManager>().Play("ShurikenHit", GetComponent<AudioSource>());
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rigidBody.isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;
            Destroy(gameObject, 2f);
        }
    }
}
