using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletEmitter;

    protected Transform player;
    protected AudioManager audioManager;
    protected Target target;

    [SerializeField] float shootSpeed = 100f;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float spread = 0.65f;
    [SerializeField] float flashedSpread = 4f;
    [SerializeField] protected float shootRadius = 20f;
    protected float nextTimeToFire;

    protected bool flashed;

    protected virtual void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        target = GetComponent<Target>();
    }

    protected virtual void FixedUpdate()
    {
        AquireTarget();

        if(!target.GetDead() && nextTimeToFire <= 0)
        {
            if(Vector3.Distance(transform.position, player.position) <= shootRadius)
            {
                bulletEmitter.transform.rotation = Quaternion.LookRotation(player.position - bulletEmitter.transform.position);
                Shoot();
            }
        }

        nextTimeToFire -= Time.fixedDeltaTime;
    }

    //Searches for the nearest player to target
    protected void AquireTarget()
    {
        float closestdistance = -1f;

        foreach(var gO in GameObject.FindGameObjectsWithTag("Player"))
        {
            float distance = Vector3.Distance(this.transform.position, gO.transform.position);
            
            if(distance < closestdistance || closestdistance == -1)
            {
                player = gO.transform;
                closestdistance = distance;
            }
        }
    }

    //Makes the Shooting happen. It creates a clone of the bullet, and it is destroyed after a few seconds.
    protected void Shoot()
    {
        nextTimeToFire = fireRate + UnityEngine.Random.Range(-fireRate / 2, fireRate / 2);

        Ray ray = new Ray(bulletEmitter.transform.position, bulletEmitter.transform.forward);

        RaycastHit hit;
        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000f);
        }

        targetPoint.x += UnityEngine.Random.Range(-spread, spread);
        targetPoint.y += UnityEngine.Random.Range(-spread, spread);

        if(flashed)
        {
            int chanceX = UnityEngine.Random.Range(0, 2);
            int chanceY = UnityEngine.Random.Range(0, 2);

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

        audioManager.NetworkPlay("ShurikenShoot", GetComponent<AudioSource>());

        FindObjectOfType<NetworkController>().NetworkSpawn(bullet.name, bulletEmitter.transform.position, bulletEmitter.transform.rotation,
        (targetPoint - bulletEmitter.transform.position).normalized * shootSpeed, 10f);
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