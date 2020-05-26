using System.Collections;
using UnityEngine;

public class ShurikenGun :  MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private GameObject bulletEmiter;
    private AudioManager audioManager;
    private UIManager uiManager;
    private Animator animator;
    private PlayerMovement playerMovement;
    private ParticleSystem muzzleFlash;
    
    [SerializeField] float speed = 100f;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] float fireRate = 15f;
    [SerializeField] float spread = 0.01f;
    private float nextTimeToFire = 0f;

    [SerializeField] int maxAmmo = 10;
    [SerializeField] int maxMag;
    private int currentAmmo;
    private int currentMag;

    private bool reloading;
    private bool scoping;

    //The Gun starts with maximum ammo.
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
        maxMag = maxAmmo * 4;
        currentAmmo = maxAmmo;
        currentMag = maxMag;
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
        uiManager.SetMaxAmmo(currentMag);
        uiManager.SetCurrentAmmo(currentAmmo);
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        bulletEmiter = GameObject.Find("ShurikenEmitter");
    }

    void Update()
    {
        uiManager.SetCurrentAmmo(currentAmmo);
        uiManager.SetMaxAmmo(currentMag);
        ManageAmmo();

        if(playerMovement.GetMoving())
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void ManageAmmo()
    {
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

    //Makes the Gun fire a temporary shuriken, and destroyes that temporary shuriken after a few seconds.
    //The shuriken has spread, but only when the Gun isn't scoped.
    public void Fire()
    {
        if(Time.time >= nextTimeToFire && !reloading)
        {
            if(currentAmmo > 0)
            {
                Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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

                currentAmmo--;
                nextTimeToFire = Time.time + 1f / fireRate;
                muzzleFlash.Play();
                audioManager.Play("ShurikenShoot");

                targetPoint.x += Random.Range(-spread, spread);
                targetPoint.y += Random.Range(-spread, spread);

                GameObject instantiateBullet = Instantiate(bullet, bulletEmiter.transform.position, bulletEmiter.transform.rotation);
                Rigidbody temporaryRigidbody = instantiateBullet.GetComponentInChildren<Rigidbody>();
                animator.SetTrigger("Firing");

                temporaryRigidbody.velocity = (targetPoint - bulletEmiter.transform.position).normalized * speed;

                Destroy(instantiateBullet, 10f);
            }
            else
            {
                Reload();
            }
        }
    }

    public void Scope(bool state)
    {
        scoping = state;
        animator.SetBool("Scoped", state);
        playerMovement.SetScoping(state);
        playerMovement.Sprint(false);
    }

    public void Reload()
    {
        if(reloading)
        {
            return;
        }

        if((currentAmmo < maxAmmo) && (currentMag > 0))
        {
            Scope(false);
            StartCoroutine(ReloadingBehaviour());
        }
    }

    public void SetInactive()
    {
        reloading = false;
        audioManager.Stop("Reload");
        audioManager.Stop("ShurikenShoot");

        if(scoping)
        {
            Scope(false);
        }

        gameObject.SetActive(false);
    }

    public void RestockAmmo()
    {
        currentMag = maxMag;
    }

    IEnumerator ReloadingBehaviour()
    {
        reloading = true;

        audioManager.Play("Reload");

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
