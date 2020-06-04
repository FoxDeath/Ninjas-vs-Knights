using System.Collections;
using UnityEngine;


public class GroundEnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public GameObject bulletEmitter;

    private Transform objective;
    private Transform player;
    private AudioManager audioManager;
    private Target target;

    [SerializeField] float shootSpeed = 100f;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float spread = 0.65f;
    [SerializeField] float flashedSpread = 4f;
    private float shootRadius;
    private float nextTimeToFire;

    private bool flashed;

    void Awake()
    {
      //  objective = GameObject.FindGameObjectWithTag("EnemyObjective").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = FindObjectOfType<AudioManager>();
        target = GetComponent<Target>();
    }

    void Start()
    {
        shootRadius = GetComponent<GroundEnemyMovement>().GetLookRadius();
    }
    
    void FixedUpdate()
    {
        if(!target.GetDead() && nextTimeToFire <= 0)
        {
            if(Vector3.Distance(transform.position, objective.position) <= shootRadius)
            {
                Shoot();
            }
            if(Vector3.Distance(transform.position, player.position) <= shootRadius)
            {
                Shoot();
            }
        }

        nextTimeToFire -= Time.fixedDeltaTime;
    }

    //Makes the Shooting happen. It creates a clone of the bullet, and it is destroyed after a few seconds.
    void Shoot()
    {
        nextTimeToFire = fireRate + Random.Range(-fireRate / 2, fireRate / 2);
        
        Ray ray = new Ray(bulletEmitter.transform.position, bulletEmitter.transform.forward);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000f);
        }

        targetPoint.x += Random.Range(-spread, spread);
        targetPoint.y += Random.Range(-spread, spread);
        
        if (flashed)
        {
            int chanceX = Random.Range(0, 2);
            int chanceY = Random.Range(0, 2);

            if(chanceX == 0)
            {
                targetPoint.x += flashedSpread;
            }
            else
            {
                targetPoint.x -= flashedSpread;
            }
            
            if(chanceY == 0)
            {
                targetPoint.y += flashedSpread;
            }
            else
            {
                targetPoint.y -= flashedSpread;
            }
            
            Invoke("SetFlashedBack", 5);
        }

        GameObject instantiateBullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
        audioManager.Play("ShurikenShoot", GetComponent<AudioSource>());
        Rigidbody temporaryRigidbody = instantiateBullet.GetComponentInChildren<Rigidbody>();

        temporaryRigidbody.velocity = (targetPoint - bulletEmitter.transform.position).normalized * shootSpeed;

        Destroy(instantiateBullet, 10f);
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
