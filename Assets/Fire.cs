using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bullet;
    public float speed = 100f;
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 2f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    private bool isReloading = false;
    private bool isScoped = false;

    public Animator animator;

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
        if(isReloading)
        {
            return;
        }

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse1))
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

    void Shoot()
    {
        currentAmmo--;

        GameObject instantiateBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        Rigidbody temporaryRigidbody = instantiateBullet.GetComponent<Rigidbody>();
        temporaryRigidbody.AddForce(Vector3.right * -speed);

        Destroy(instantiateBullet, 3f);
    }
}
