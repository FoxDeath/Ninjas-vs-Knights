using UnityEngine;

public class CrossBow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    private GameObject anchor;
    [SerializeField] GameObject player;
    private GameObject model;
    [SerializeField] LayerMask layerMask;
    private KnightUI knightUI;
    private AudioManager audioManager;
    private Animator crossbowAnimator;
    private Animator shieldAnimator;
    private Camera fpsCam;
    private Camera weaponCam;
    private UIManager uiManager;
    private MouseLook mouseLook;
    [SerializeField] GameObject muzzleFlash;
    private Transform bulletEmiter;
    private Quaternion startingRotation;

    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float spread = 10f;
    [SerializeField] float pushForce = 1000f;
    private float nextTimeToFire = 0f;
    private float scopedFOV;
    private float[] scopedFOVs = { 5f, 10f, 15f, 20f };
    private float scopedSensitivity = 1f;
    private float lookSensitivity = 0.75f;
    [SerializeField] int maxMag = 15;
    private int currentMag;
    private int currentScopedFOV;

    private bool scoping;
    private bool ourBoiIsInTheProcessOfScoping;
    private bool equiped;

    #region Getters and Setters

    public float GetScopeSensitivity()
    {
        return scopedSensitivity;
    }

    public void SetScopedSensitivity(float sens)
    {
        scopedSensitivity = sens;
    }

    public float GetSensitivity()
    {
        return lookSensitivity;
    }

    public void SetSensitivity(float sens)
    {
        lookSensitivity = sens;
    }

    public Quaternion GetStartingRotation()
    {
        return startingRotation;
    }

    public bool GetScoping()
    {
        return scoping;
    }

    public void SetEquiped(bool equiped)
    {
        this.equiped = equiped;
    }

    #endregion

    void Awake()
    {
        uiManager = UIManager.GetInstance();
        knightUI = transform.parent.parent.GetComponentInChildren<KnightUI>();
        audioManager = GetComponentInParent<AudioManager>();
        crossbowAnimator = GetComponent<Animator>();
        shieldAnimator = transform.parent.Find("Shield").GetComponent<Animator>();
        fpsCam = transform.parent.GetComponent<Camera>();
        weaponCam = transform.parent.Find("WeaponCamera").GetComponent<Camera>();
        bulletEmiter = transform.GetChild(0).GetChild(0).GetChild(0).Find("BulletEmitter");
        lookSensitivity = GetComponentInParent<MouseLook>().GetSensitivity();
        model = transform.GetChild(0).gameObject;
        mouseLook = GetComponentInParent<MouseLook>();
    }

    void Start()
    {
        equiped = transform.GetChild(0).gameObject.activeSelf;

        currentScopedFOV = scopedFOVs.Length - 1;
        scopedFOV = scopedFOVs[currentScopedFOV];

        currentMag = maxMag;

        if (equiped)
        {
            uiManager.SetCurrentAmmo(1, null, knightUI);
            uiManager.SetMaxAmmo(currentMag, null, knightUI);
        }

        startingRotation = transform.localRotation;
    }

    void Update()
    {
        if(!equiped)
        {
            return;
        }

        ManageAmmo();

        SetAmmo();
        
        if(GetComponentInParent<PlayerMovement>().GetMoving())
        {
            crossbowAnimator.SetBool("Moving", true);
            shieldAnimator.SetBool("Moving", true);
        }
        else
        {
            crossbowAnimator.SetBool("Moving", false);
            shieldAnimator.SetBool("Moving", false);
        }
    }

    private void ManageAmmo()
    {
        if (currentMag > maxMag)
        {
            currentMag = maxMag;
        }

        if (currentMag < 0)
        {
            currentMag = 0;
        }
    }

    private void SetAmmo()
    {
        if(Time.time >= nextTimeToFire - 0.1f)
        {
            uiManager.SetCurrentAmmo(1, null, knightUI);
            uiManager.SetMaxAmmo(currentMag, null, knightUI);
        }
        else
        {
            uiManager.SetCurrentAmmo(0, null, knightUI);
        }
    }

    public void RestockAmmo()
    {
        currentMag = maxMag;
    }

    public bool CanAddAmmo()
    {
        if (currentMag < maxMag)
        {
            return true;
        }

        return false;
    }

    public void Fire()
    {
        if(!GetComponentInParent<KnightPlayerMovement>().isLocalPlayer)
        {
            return;
        }

        if(Time.time >= nextTimeToFire && currentMag > 0)
        {
            currentMag--;

            nextTimeToFire = Time.time + 1 / fireRate;
            crossbowAnimator.SetTrigger("Reloading");

            Vector3 forwardVector = CalculatingSpread();

            if(scoping)
            {
                forwardVector = fpsCam.transform.forward;
            }

            Debug.DrawRay(fpsCam.transform.position, (fpsCam.transform.forward * range) + new Vector3(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f)));
            RaycastHit hit;

            audioManager.NetworkPlay("Laser");
            player.GetComponent<NetworkController>().NetworkSpawn(muzzleFlash.name, bulletEmiter.transform.position, bulletEmiter.transform.rotation, Vector3.zero);

            //Checks if the raycast hits anything and if it is a target then it takes damage
            if(Physics.Raycast(fpsCam.transform.position, forwardVector, out hit, range, layerMask))
            {
                Target target = hit.transform.GetComponent<Target>();

                if(target)
                {
                    target.TakeDamage(damage);
                }
            }

            //If the target has a transform that isn't the player then it puts an arrow in it
            if(hit.transform && hit.transform != player.transform)
            {
                GetComponentInParent<NetworkController>().NetworkSpawn(arrowPrefab.name, hit.point, fpsCam.transform.rotation, Vector3.zero);
            }
            
            mouseLook.Fire(scoping);
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

    public void Scope()
    {
        if(!GetComponentInParent<KnightPlayerMovement>().isLocalPlayer)
        {
            return;
        }

        if (!ourBoiIsInTheProcessOfScoping)
        {
            ourBoiIsInTheProcessOfScoping = true;
            scoping = !scoping;
            player.GetComponent<KnightPlayerMovement>().SetScoping(scoping);
            player.GetComponent<KnightPlayerMovement>().Sprint(false);
            crossbowAnimator.SetBool("Scoped", scoping);
            shieldAnimator.SetBool("Scoped", scoping);
            Invoke("ScopeBehaviour", 0.15f);
        }
    }

    public void ScopeZoom()
    {
        if(!GetComponentInParent<KnightPlayerMovement>().isLocalPlayer)
        {
            return;
        }
        
        if (currentScopedFOV <= 0)
        {
            currentScopedFOV = scopedFOVs.Length;
            GetComponentInParent<MouseLook>().mouseSensitivity = 10f;
        }

        currentScopedFOV--;
        scopedFOV = scopedFOVs[currentScopedFOV];
        ScopeBehaviour();
    }

    private void ScopeBehaviour()
    {
        uiManager.SetKnightUIActive(!scoping, knightUI);
        uiManager.SetKnightScopeOverlayActive(scoping, knightUI);
        weaponCam.gameObject.SetActive(!scoping);
        
        //Changes the mouse sensitivity depentding on the level of zoom
        if(scoping)
        {
            fpsCam.fieldOfView = scopedFOV; 
            GetComponentInParent<MouseLook>().SetSensitivity(scopedSensitivity);
        }
        else
        {
            fpsCam.fieldOfView = weaponCam.fieldOfView;
            GetComponentInParent<MouseLook>().SetSensitivity(lookSensitivity);
        }

        ourBoiIsInTheProcessOfScoping = false;
    }
}
