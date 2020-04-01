using UnityEngine;
using UnityEngine.InputSystem;

public class CrossBow : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] LayerMask layerMask;
    private AudioManager audioManager;
    private Animator crossbowAnimator;
    private Animator shieldAnimator;
    private Camera fpsCam;
    private Camera weaponCam;
    private UIManager uiManager;
    private ParticleSystem muzzleFlash;
    private GameObject player;

    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float spread = 10f;
    [SerializeField] float pushForce = 1000f;
    private float nextTimeToFire = 0f;
    private float scopedFOV;
    private float[] scopedFOVs = { 5f, 10f, 15f, 20f };
    private float maxMouseSensitivity;

    private int currentScopedFOV;

    private bool scoping;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        crossbowAnimator = GetComponent<Animator>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        shieldAnimator = GameObject.Find("Shield").GetComponent<Animator>();
        fpsCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        weaponCam = GameObject.Find("WeaponCamera").GetComponent<Camera>();
        player = GameObject.Find("KnightPlayer");

        uiManager = FindObjectOfType<UIManager>();
        uiManager.SetMaxAmmo(1);
        uiManager.SetCurrentAmmo(1);

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
        if(Time.time >= nextTimeToFire - 0.1f)
        {
            uiManager.SetCurrentAmmo(1);
        }
        else
        {
            uiManager.SetCurrentAmmo(0);
        }
    }

    public void FireInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            crossbowAnimator.SetBool("Reloading", true);
            Fire();
        }
        else
        {
            crossbowAnimator.SetBool("Reloading", false);
        }
    }

    private void Fire()
    {
        Vector3 forwardVector = CalculatingSpread();

        if(scoping)
        {
            forwardVector = fpsCam.transform.forward;
        }

        Debug.DrawRay(fpsCam.transform.position, (fpsCam.transform.forward * range) + new Vector3(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f)));
        RaycastHit hit;

        audioManager.Play("Laser");
        muzzleFlash.Play();

        //Checks if the raycast hits anything and if it is a target then it takes damage
        if(Physics.Raycast(fpsCam.transform.position, forwardVector, out hit, range, layerMask))
        {
            Target target = hit.transform.GetComponent<Target>();

            if(target)
            {
                target.TakeDamage(damage);
            }
        }

        //If the target has a rigidbody then it pushes it
        if(hit.rigidbody)
        {
            hit.rigidbody.AddForce(-hit.normal * pushForce);
        }

        //If the target has a transform that isn't the player then it puts an arrow in it
        if(hit.transform && hit.transform != player.transform)
        {
            GameObject arrow = Instantiate(arrowPrefab, hit.point, fpsCam.transform.rotation);
            arrow.transform.parent = hit.transform;
            Destroy(arrow, 2.5f);
        }
    }

    //Calculating the spread of the crossbow
    private Vector3 CalculatingSpread()
    {
        Vector3 forwardVector = Vector3.forward;
        float deviation = Random.Range(0f, spread);
        float angle = Random.Range(0f, 360f);
        forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
        forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
        forwardVector = fpsCam.transform.rotation * forwardVector;
        
        return forwardVector;
    }

    public void ScopeInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            scoping = !scoping;
            player.GetComponent<KnightPlayerMovement>().SetScoping(scoping);
            player.GetComponent<KnightPlayerMovement>().Sprint(false);
            crossbowAnimator.SetBool("Scoped", scoping);
            shieldAnimator.SetBool("Scoped", scoping);
            Invoke("Scope", 0.15f);
        }
    }

    //Iterates trough different levels of zoom
    public void ScopeZoomInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if(currentScopedFOV <= 0)
            {
                currentScopedFOV = scopedFOVs.Length;
                fpsCam.GetComponent<MouseLook>().mouseSensitivity = 10f;
            }

            currentScopedFOV--;
            scopedFOV = scopedFOVs[currentScopedFOV];
            Scope();
        }
    }

    private void Scope()
    {
        uiManager.SetKnightUIActive(!scoping);
        uiManager.SetKnightScopeOverlayActive(scoping);
        weaponCam.gameObject.SetActive(!scoping);
        
        //Changes the mouse sensitivity depentding on the level of zoom
        if(scoping)
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
