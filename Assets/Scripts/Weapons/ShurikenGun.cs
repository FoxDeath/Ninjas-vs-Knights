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
    private int currentAmmo;

    private bool reloading;
    private bool scoping;

    //The Gun starts with maximum ammo.
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
        currentAmmo = maxAmmo;
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
        uiManager.SetMaxAmmo(maxAmmo);
        uiManager.SetCurrentAmmo(currentAmmo);
        muzzleFlash = GetComponentInChildren<ParticleSystem>();

        bulletEmiter = GameObject.Find("ShurikenEmitter");
    }

    void Update()
    {
        uiManager.SetCurrentAmmo(currentAmmo);
        uiManager.SetMaxAmmo(maxAmmo);

        if(playerMovement.GetMoving())
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
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
                RaycastHit hit ;

                Vector3 targetPoint ;
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

                temporaryRigidbody.velocity = (targetPoint - bulletEmiter.transform.position).normalized * speed;

                Destroy(instantiateBullet, 10f);
            }
            else
            {
                StartCoroutine(ReloadingBehaviour());

                return;
            }
            
            animator.SetTrigger("Firing");
        }
    }

    public void Scope(bool state)
    {
        scoping = state;
        animator.SetBool("Scoped", state);
        //playerMovement.SetScoping(state);
        //playerMovement.Sprint(false);
    }

    public void Reload()
    {
        if(reloading)
        {
            return;
        }

        if(currentAmmo < maxAmmo)
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

    IEnumerator ReloadingBehaviour()
    {
        reloading = true;

        audioManager.Play("Reload");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        reloading = false;
    }
}
