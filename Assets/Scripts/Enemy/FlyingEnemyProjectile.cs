using UnityEngine;

public class FlyingEnemyProjectile : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] float damage = 20f;

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * speed;
    }

    void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.tag.Equals("FlyingEnemy") && !other.gameObject.tag.Equals("GroundEnemy") && !other.gameObject.tag.Equals("Ammo"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
