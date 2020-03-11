using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Bullet>())
        {
            return;
        }
        Target target = collision.gameObject.transform.GetComponent<Target>();
        if (target)
        {
            target.TakeDamage(damage);
        }
        if(collision.gameObject.layer != 10)
        {
            gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }
}
