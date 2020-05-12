using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearGun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject chargeBullet;
    private GameObject bulletEmiter;
    private UIManager uiManager;
    private AudioManager audioManager;
    private ParticleSystem muzzleFlash;
    private Animator animator;

    [SerializeField] float speed = 100f;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] float fireRate = 15f;
    [SerializeField] float spread = 0.01f;
    private float nextTimeToFire = 0f;
    private float altSpeed;

    [SerializeField] int maxAmmo = 10;
    [SerializeField] int maxMag;
    private int currentAmmo;
    private int currentMag;

    private Quaternion startingRotation;

    private bool reloading;

    void Start()
    {
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        bulletEmiter = GameObject.Find("SpearGunEmitter");
        maxMag = maxAmmo * 4;
        currentAmmo = maxAmmo;
        currentMag = maxMag;
        uiManager.SetMaxAmmo(currentMag);
        uiManager.SetCurrentAmmo(currentAmmo);
        altSpeed = speed / 2;
        startingRotation = transform.localRotation;
    }

    void Update()
    {
        uiManager.SetCurrentAmmo(currentAmmo);
        uiManager.SetMaxAmmo(currentMag);

        if(currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        if(currentMag > maxMag)
        {
            currentMag = maxMag;
        }

        if(currentAmmo < 0)
        {
            currentAmmo = 0;
        }
        
        if(currentMag < 0)
        {
            currentMag = 0;
        }
    }

    //Reloads the gun.
    public void Reload()
    {
        if(reloading)
        {
            return;
        }

        if((currentAmmo < maxAmmo) && (currentMag > 0))
        {
            StartCoroutine(ReloadingBehaviour());
        }
    }

    //Makes the Gun fire a temporary bullet and destroyes that temporary bullet after a few seconds.
    public void Fire()
    {
        if(Time.time >= nextTimeToFire && !reloading && currentAmmo > 0)
        {
            if(currentAmmo > 0)
            {
                Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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

                currentAmmo--;
                nextTimeToFire = Time.time + 1f / fireRate;
                muzzleFlash.Play();
                audioManager.Play("Laser");

                targetPoint.x += Random.Range(-spread, spread);
                targetPoint.y += Random.Range(-spread, spread);
        
                GameObject instantiateBullet = Instantiate(bullet, bulletEmiter.transform.position, bulletEmiter.transform.rotation);
                Rigidbody temporaryRigidbody = instantiateBullet.GetComponentInChildren<Rigidbody>();

                temporaryRigidbody.velocity = (targetPoint - bulletEmiter.transform.position).normalized * speed;

                Destroy(instantiateBullet, 2f);
            }
            else
            {
                StartCoroutine(ReloadingBehaviour());

                return;
            }

            animator.SetTrigger("Firing");
        }
    }

    public void Charge()
    {
        if(currentAmmo >= 3)
        {
            Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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

            currentAmmo -= 3;
            muzzleFlash.Play();
            audioManager.Play("Laser");

            targetPoint.x += Random.Range(-spread, spread);
            targetPoint.y += Random.Range(-spread, spread);
            
            GameObject instantiateBullet = Instantiate(chargeBullet, bulletEmiter.transform.position, bulletEmiter.transform.rotation);
            Rigidbody temporaryRigidbody = instantiateBullet.GetComponentInChildren<Rigidbody>();
            temporaryRigidbody.velocity = (targetPoint - bulletEmiter.transform.position).normalized * altSpeed;
            Destroy(instantiateBullet, 2f);
        }
        else
        {
            StartCoroutine(ReloadingBehaviour());

            return;
        }

        animator.SetTrigger("Firing");
    }

    public void SetInactive()
    {
        reloading = false;
        audioManager.Stop("Laser");
        transform.localRotation = startingRotation;
        gameObject.SetActive(false);
    }

    IEnumerator ReloadingBehaviour()
    {
        reloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        if(currentMag >= maxAmmo - currentAmmo)
        {
            currentMag -= maxAmmo - currentAmmo;
            currentAmmo = maxAmmo;
        }

        if(currentMag < maxAmmo - currentAmmo)
        {
            currentAmmo += currentMag;
            currentMag = 0;
        }

        reloading = false;
    }
}
