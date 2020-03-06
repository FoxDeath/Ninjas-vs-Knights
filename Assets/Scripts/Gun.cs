using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 50f;
    [SerializeField] float pushForce = 10f;

    [SerializeField] Camera fpsCam;
    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem trailEffect;
    [SerializeField] GameObject arrowPrefab;

    [SerializeField] LayerMask layerMask;

    private float nextTimeToFire = 0f;

    public void FireInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1/fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Laser");
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
        {
            Target target = hit.transform.GetComponent<Target>();
            if(target)
            {
                target.TakeDamage(damage);
            }

            trailEffect.transform.rotation = Quaternion.LookRotation((hit.point - trailEffect.transform.position).normalized);
        }
        trailEffect.Play();

        if(hit.rigidbody)
        {
            hit.rigidbody.AddForce(-hit.normal * pushForce);
        }

        
        if(hit.transform && hit.transform != player.transform)
        {
            GameObject arrow = Instantiate(arrowPrefab, hit.point, trailEffect.transform.rotation);
            arrow.transform.parent = hit.transform;
            Destroy(arrow, 2.5f);
        }
    }
}
