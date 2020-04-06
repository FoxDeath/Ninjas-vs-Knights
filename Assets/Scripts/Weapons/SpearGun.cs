using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearGun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
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

    [SerializeField] int maxAmmo = 10;
    private int currentAmmo;

    private bool reloading;

    void Start()
    {
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        bulletEmiter = GameObject.Find("SpearGunEmitter");
        currentAmmo = maxAmmo;
        uiManager.SetMaxAmmo(maxAmmo);
        uiManager.SetCurrentAmmo(currentAmmo);
    }

    void Update()
    {
        uiManager.SetCurrentAmmo(currentAmmo);
    }

    //Fires the gun when the input is performed.
    public void FireInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Fire();
        }
    }

    //Reloads the gun.
    public void ReloadInput(InputAction.CallbackContext context)
    {
        if(reloading)
        {
            return;
        }
        
        if(context.phase == InputActionPhase.Performed || currentAmmo <= 0)
        {
            if(currentAmmo < maxAmmo)
            {
                StartCoroutine(Reloading());

                return;
            }
        }
    }

    //Makes the Gun fire a temporary bullet and destroyes that temporary bullet after a few seconds.
    void Fire()
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
                StartCoroutine(Reloading());

                return;
            }

            animator.SetTrigger("Firing");
        }
    }

    IEnumerator Reloading()
    {
        reloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        reloading = false;
    }
}
