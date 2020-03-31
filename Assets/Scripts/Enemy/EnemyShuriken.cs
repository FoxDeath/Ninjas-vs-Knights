using UnityEngine;

public class EnemyShuriken : MonoBehaviour
{
    [SerializeField] float speed = 200f;
    [SerializeField] float damage = 10f;

    private Transform player;
    private Vector3 target;
    private Vector3 direction;

    //The Target is the players position, when an EnemyShuriken is spawned.
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player.position;
        direction = (target - transform.position).normalized;
    }

    //The EnemyShuriken will move towards the targets last known position.
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    //When the EnemyShuriken hits the Player, the Player will take damage.
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
