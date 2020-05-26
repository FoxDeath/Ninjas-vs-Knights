using UnityEngine;

public class SpearGunLaser : MonoBehaviour
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

        if(target)
        {
            target.TakeDamage(damage);
        }

        if(collision.gameObject.layer != LayerMask.NameToLayer("Player") && !collision.gameObject.tag.Equals("Ammo"))
        {
            GetComponent<Collider>().enabled = false;
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rigidBody.isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;
            rigidBody.velocity = Vector3.zero;
            Destroy(gameObject, 0.1f);
        }
    }
}
