using UnityEngine;
using UnityEngine.InputSystem;

public class CrossBow : MonoBehaviour, IWeapon
{
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 50f;
    [SerializeField] float pushForce = 10f;

    [SerializeField] Camera fpsCam;
    [SerializeField] Camera weaponCam;
    [SerializeField] GameObject player;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject scopeOverlay;
    [SerializeField] GameObject gameUI;
    [SerializeField] Animator shieldAnimator;
    private Animator crossbowAnimator;

    private AmmoCounter ammoCounter;

    private float nextTimeToFire = 0f;
    private float scopedFOV;
    private float[] scopedFOVs = {5f, 10f, 15f, 20f};
    private int currentScopedFOV;
    private float maxMouseSensitivity;

    private bool isScoped;

    void Start()
    {
        ammoCounter = (AmmoCounter)FindObjectOfType(typeof(AmmoCounter));
        ammoCounter.SetMaxAmmo(1);
        ammoCounter.SetCurrentAmmo(1);

        crossbowAnimator = GetComponent<Animator>();
        currentScopedFOV = scopedFOVs.Length - 1;
        scopedFOV = scopedFOVs[currentScopedFOV];
        maxMouseSensitivity = fpsCam.GetComponent<MouseLook>().mouseSensitivity;
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
        }

        if(hit.rigidbody)
        {
            hit.rigidbody.AddForce(-hit.normal * pushForce);
        }

        
        if(hit.transform && hit.transform != player.transform)
        {
            GameObject arrow = Instantiate(arrowPrefab, hit.point, fpsCam.transform.rotation);
            arrow.transform.parent = hit.transform;
            Destroy(arrow, 2.5f);
        }
    }

    public void ScopeInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            isScoped = !isScoped;
            crossbowAnimator.SetBool("Scoped", isScoped);
            shieldAnimator.SetBool("Scoped", isScoped);
            Invoke("Scope", 0.15f);
        }
    }

    public void ScopeZoomInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if(currentScopedFOV <= 0)
            {
                currentScopedFOV = scopedFOVs.Length;
                fpsCam.GetComponent<MouseLook>().mouseSensitivity = 10f;
            }
            currentScopedFOV --;
            scopedFOV = scopedFOVs[currentScopedFOV];
            Scope();
        }
    }

    private void Scope()
    {
        gameUI.SetActive(!isScoped);
        scopeOverlay.SetActive(isScoped);
        weaponCam.gameObject.SetActive(!isScoped);
        if(isScoped)
        {
            fpsCam.fieldOfView = scopedFOV; 
            fpsCam.GetComponent<MouseLook>().mouseSensitivity = maxMouseSensitivity / (1.3f * scopedFOVs.Length - currentScopedFOV);
        }
        else
        {
            fpsCam.fieldOfView = weaponCam.fieldOfView;
            fpsCam.GetComponent<MouseLook>().mouseSensitivity = maxMouseSensitivity;
        }
    }
}
