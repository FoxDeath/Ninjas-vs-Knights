using UnityEngine;
using UnityEngine.InputSystem;

public class CrossBow : MonoBehaviour, IWeapon
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

    private AmmoCounter ammoCounter;

    private float nextTimeToFire = 0f;

    void Start()
    {
        ammoCounter = (AmmoCounter)FindObjectOfType(typeof(AmmoCounter));
        ammoCounter.SetMaxAmmo(1);
        ammoCounter.SetCurrentAmmo(1);
    }

    void Update()
    {
        SetAmmo();
    }

    private void SetAmmo()
    {
        if (Time.time >= nextTimeToFire - 0.1f)
        {
            ammoCounter.SetCurrentAmmo(1);
        }
        else
        {
            ammoCounter.SetCurrentAmmo(0);
        }
    }

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
        else
        {
            trailEffect.transform.rotation = new Quaternion();
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
