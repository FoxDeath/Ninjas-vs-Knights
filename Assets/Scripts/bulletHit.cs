using UnityEngine;

public class bulletHit : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<bulletHit>())
        {
            return;
        }
        Target target = collision.gameObject.transform.GetComponent<Target>();
        if (target)
        {
            target.TakeDamage(damage);
        }
        if(collision.gameObject.layer != 9)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }
}
