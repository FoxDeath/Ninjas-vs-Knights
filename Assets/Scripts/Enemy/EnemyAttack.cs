using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float nextTimeToFire;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float shootRadius = 20f;

    [SerializeField] GameObject bullet;
    public GameObject bulletEmitter;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //When a Player enters the shooting radius, the Enemy will start shooting at him.
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) <= shootRadius)
        {
            if(nextTimeToFire <= 0)
            {
                Shoot();
                nextTimeToFire = 1f / fireRate;
            }

            nextTimeToFire -= Time.deltaTime;
        }
    }

    //Makes the Shooting happen. It creates a clone of the bullet, and it is destroyed after a few seconds.
    void Shoot()
    {
        GameObject instantiateBullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
        Destroy(instantiateBullet, 2f);
    }
}
