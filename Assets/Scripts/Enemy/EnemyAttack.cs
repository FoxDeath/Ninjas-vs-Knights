using System.Collections;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    private float nextTimeToFire;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float shootRadius = 20f;
    [SerializeField] float spread = 0.65f;

    [SerializeField] GameObject bullet;
    public GameObject bulletEmitter;
    private Transform player;
    public bool flashed;


    void Start()

    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //When a Player enters the shooting radius, the Enemy will start shooting at him.
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= shootRadius)
        {
            if (nextTimeToFire <= 0)
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
        Vector3 shootDirection = bulletEmitter.transform.forward;
        shootDirection.x += Random.Range(-spread, spread);
        shootDirection.y += Random.Range(-spread, spread);

        GameObject instantiateBullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
        Rigidbody temporaryRigidbody = instantiateBullet.GetComponentInChildren<Rigidbody>();

        if (flashed)
        {                     
            temporaryRigidbody.AddForce(shootDirection * 200);          
            Invoke("SetFlashedBack", 5);
        }
        Destroy(instantiateBullet, 2f);
    }
    public void SetFlashed(bool flashed)
    {
        this.flashed = flashed;
    }
     void SetFlashedBack()
    {
        flashed = false;
    }
}
