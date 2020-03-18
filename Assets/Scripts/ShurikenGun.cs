using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShurikenGun :  MonoBehaviour, IWeapon
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletEmiter;
    [SerializeField] Animator animator;
    private AudioManager audioManager;

    private AmmoCounter ammoCounter;
    
    [SerializeField] float speed = 100f;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] float fireRate = 15f;
    private float nextTimeToFire = 0f;
    [SerializeField] float spread = 0.1f;

    [SerializeField] int maxAmmo = 10;
    private int currentAmmo;

    private bool isReloading;
    private bool isScoped;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        currentAmmo = maxAmmo;
        ammoCounter = (AmmoCounter)FindObjectOfType(typeof(AmmoCounter));
        ammoCounter.SetMaxAmmo(maxAmmo);
        ammoCounter.SetCurrentAmmo(currentAmmo);
    }

    public void FireInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && Time.time >= nextTimeToFire && !isReloading)
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                nextTimeToFire = Time.time + 1f / fireRate;
                Fire();
            }
            else
            {
                StartCoroutine(Reloading());
                return;
            }
        }
    }

    void Update()
    {
        ammoCounter.SetCurrentAmmo(currentAmmo);
    }

    void Fire()
    {
        audioManager.Play("ShurikenShoot");

        Vector3 shootDirection = bulletEmiter.transform.forward;
        shootDirection.x += Random.Range(-spread, spread);
        shootDirection.y += Random.Range(-spread, spread);

        GameObject instantiateBullet = Instantiate(bullet, bulletEmiter.transform.position, bulletEmiter.transform.rotation);
        Rigidbody temporaryRigidbody = instantiateBullet.GetComponentInChildren<Rigidbody>();

        if (!isScoped)
        {
            temporaryRigidbody.AddForce(shootDirection * speed);
        }
        else
        {
            temporaryRigidbody.AddForce(bulletEmiter.transform.forward * speed);
        }

        Destroy(instantiateBullet, 2f);
    }

    public void ScopeInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);
        }
    }

    public void ReloadInput(InputAction.CallbackContext context)
    {
        if (isReloading)
        {
            return;
        }
        
        if (context.phase == InputActionPhase.Performed || currentAmmo <= 0)
        {
            if (currentAmmo < maxAmmo)
            {
                StartCoroutine(Reloading());
                return;
            }
        }
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        audioManager.Play("Reload");


        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
