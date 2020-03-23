using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShurikenGun :  MonoBehaviour, IWeapon
{
    [SerializeField] GameObject bullet;
    private GameObject bulletEmiter;
    private AudioManager audioManager;
    private UIManager uiManager;
    private Animator animator;
    private NinjaPlayerMovement ninjaMovement;
    
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
        ninjaMovement = GameObject.Find("NinjaPlayer").GetComponent<NinjaPlayerMovement>();
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
        currentAmmo = maxAmmo;
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
        uiManager.SetMaxAmmo(maxAmmo);
        uiManager.SetCurrentAmmo(currentAmmo);

        bulletEmiter = GameObject.Find("ShurikenEmitter");
    }

    //Fires the Gun when the input is performed, lowers the current ammo, and automatically starts reloading when running out of ammo.
    public void FireInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && Time.time >= nextTimeToFire && !reloading)
        {
            if(currentAmmo > 0)
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
        uiManager.SetCurrentAmmo(currentAmmo);
    }

    //Makes the Gun fire a temporary shuriken, and destroyes that temporary shuriken after a few seconds.
    //The shuriken has spread, but only when the Gun isn't scoped.
    void Fire()
    {
        audioManager.Play("ShurikenShoot");

        Vector3 shootDirection = bulletEmiter.transform.forward;

        shootDirection.x += Random.Range(-spread, spread);
        shootDirection.y += Random.Range(-spread, spread);

        GameObject instantiateBullet = Instantiate(bullet, bulletEmiter.transform.position, bulletEmiter.transform.rotation);
        Rigidbody temporaryRigidbody = instantiateBullet.GetComponentInChildren<Rigidbody>();

        if(!scoping)
        {
            temporaryRigidbody.AddForce(shootDirection * speed);
        }
        else
        {
            temporaryRigidbody.AddForce(bulletEmiter.transform.forward * speed);
        }

        Destroy(instantiateBullet, 2f);
    }

    //Scopes the Gun.
    public void ScopeInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            scoping = !scoping;
            animator.SetBool("Scoped", scoping);
            ninjaMovement.SetScoping(scoping);
            ninjaMovement.Sprint(false);
        }
    }

    //Reloads the Gun.
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

    IEnumerator Reloading()
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
