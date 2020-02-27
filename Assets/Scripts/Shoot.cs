using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Animator animator;

    public float speed = 100f;
    public float reloadTime = 2f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;
    public float spread = 0.1f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public int strayFactor;

    private bool isReloading = false;
    private bool isScoped = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) || currentAmmo <= 0)
        {
            if (currentAmmo < maxAmmo)
            {
                StartCoroutine(Reload());
                return;
            }
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);
        }
    }

    IEnumerator Reload()
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
        currentAmmo--;

        Vector3 shootDirection = bullet.transform.forward;
        shootDirection.x += Random.Range(-spread, spread);
        shootDirection.y += Random.Range(-spread, spread);

        GameObject instantiateBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        Rigidbody temporaryRigidbody = instantiateBullet.GetComponent<Rigidbody>();
        temporaryRigidbody.AddForce(shootDirection * speed); //Vector3.forward
        
        Destroy(instantiateBullet, 3f);
    }
}
