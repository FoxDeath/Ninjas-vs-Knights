using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletEmiter;
    public Animator animator;
    
    public float speed = 100f;
    public float reloadTime = 2f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;
    public float spread = 0.1f;

    public int maxAmmo = 10;
    private int currentAmmo;

    private bool isReloading = false;
    private bool isScoped = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && Time.time >= nextTimeToFire && !isReloading)
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                nextTimeToFire = Time.time + 1f / fireRate;
                TryShoot();
            }
            else
            {
                StartCoroutine(Reloading());
                return;
            }
        }
    }

    public void Scope(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);
        }
    }

    public void Reload(InputAction.CallbackContext context)
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

        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void TryShoot()
    {
        Vector3 shootDirection = bulletEmiter.transform.forward;
        shootDirection.x += Random.Range(-spread, spread);
        shootDirection.y += Random.Range(-spread, spread);

        GameObject instantiateBullet = Instantiate(bullet, bulletEmiter.transform.position, bulletEmiter.transform.rotation);
        Rigidbody temporaryRigidbody = instantiateBullet.GetComponent<Rigidbody>();

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
}
