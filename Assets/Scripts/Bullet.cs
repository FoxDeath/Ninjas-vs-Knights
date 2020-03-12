using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigidBody;
    [SerializeField] float damage = 10f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.transform.GetComponent<Target>();
        if (target)
        {
            target.TakeDamage(damage);
        }
        if(collision.gameObject.layer != 10)
        {
            FindObjectOfType<AudioManager>().Play("ShurikenHit");
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rigidBody.isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;
            Destroy(gameObject, 2f);
        }
    }
}
