using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour
{
    private UIManager uiManager;
    private NinjaUI ninjaUI;

    [SerializeField] GameObject regularArrowObj;
    [SerializeField] GameObject fireArrowObj;
    [SerializeField] GameObject slowArrowObj;
    [SerializeField] GameObject explosiveArrowObj;
    [SerializeField] GameObject emmiter;
    
    private Animator animator;
    private PlayerMovement playerMovement;
    private MouseLook mouseLook;
    private WeaponRecoil weaponRecoil;

    private AudioManager audioManager;

    private Quaternion startingRotation;

    public enum arrowTypes
    {
        Regular,
        Fire,
        Slow,
        Explosion
    }

    private arrowTypes currentType;

    private float charge;
    [SerializeField] float chargeMax;
    [SerializeField] float chargeRate;

    [SerializeField] int maxArrows = 10;
    private int currentRegularArrows;
    private int currentFireArrows;
    private int currentSlowArrows;
    private int currentExplosiveArrows;

    private bool charging;
    private bool canShoot = true;
    private bool equiped;

    public void SetCharging(bool charging)
    {
        this.charging = charging;
    }

    public Quaternion GetStartingRotation()
    {
        return startingRotation;
    }

    public void SetEquiped(bool equiped)
    {
        this.equiped = equiped;
    }

    void Awake()
    {
        ninjaUI = transform.parent.parent.GetComponentInChildren<NinjaUI>();
        uiManager = UIManager.GetInstance();
        mouseLook = GetComponentInParent<MouseLook>();
        weaponRecoil = GetComponent<WeaponRecoil>();
    }

    void Start()
    {
        equiped = transform.GetChild(0).gameObject.activeSelf;

        currentType = arrowTypes.Regular;
        currentRegularArrows = maxArrows;
        currentFireArrows = maxArrows;
        currentSlowArrows = maxArrows;
        currentExplosiveArrows = maxArrows;

        if(equiped)
        {
            uiManager.SetMaxAmmo(maxArrows, ninjaUI);
            uiManager.SetCurrentAmmo(currentRegularArrows, ninjaUI);
        }

        charging = false;
        charge = 0f;

        startingRotation = transform.localRotation;
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if(!GetComponentInParent<NinjaPlayerMovement>().isLocalPlayer || !equiped)
        {
            return;
        }

        uiManager.SetMaxAmmo(maxArrows, ninjaUI);

        if (charging && charge < chargeMax)
        {
            playerMovement.SetScoping(true);
            playerMovement.Sprint(false);

            if(charge < chargeMax)
            {
                charge += Time.deltaTime * chargeRate;
            }
        }
        else
        {
            playerMovement.SetScoping(false);
        }

        switch(currentType)
        {
            case arrowTypes.Fire:
                uiManager.SetCurrentAmmo(currentFireArrows, ninjaUI);
                break;

            case arrowTypes.Regular:
                uiManager.SetCurrentAmmo(currentRegularArrows, ninjaUI);
                break;

            case arrowTypes.Slow:
                uiManager.SetCurrentAmmo(currentSlowArrows, ninjaUI);
                break;
                
            case arrowTypes.Explosion:
                uiManager.SetCurrentAmmo(currentExplosiveArrows, ninjaUI);
                break;
        }

        if(GetComponentInParent<PlayerMovement>().GetMoving())
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    public bool CanShoot()
    {
        if((currentType == arrowTypes.Explosion && currentExplosiveArrows > 0) || (currentType == arrowTypes.Fire && currentFireArrows > 0) ||
                    (currentType == arrowTypes.Regular && currentRegularArrows > 0) || (currentType == arrowTypes.Slow && currentSlowArrows > 0))
        {
            if (!uiManager.GetArrowMenuState(ninjaUI))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void Fire()
    {
        if (!CanShoot() || !GetComponentInParent<NinjaPlayerMovement>().isLocalPlayer)
        {
            return;
        }
        else
        {
            StartCoroutine(FireBehaviour());
        }
    }

    private IEnumerator FireBehaviour()
    {
        if(canShoot)
        {
            switch(currentType)
            {
                case arrowTypes.Regular:
                    currentRegularArrows--;
                    uiManager.SetCurrentAmmo(currentRegularArrows, ninjaUI);
                    InstantiateArow(regularArrowObj);
                    break;

                case arrowTypes.Fire:
                    currentFireArrows--;
                    uiManager.SetCurrentAmmo(currentFireArrows, ninjaUI);
                    InstantiateArow(fireArrowObj);
                    break;

                case arrowTypes.Explosion:
                    currentExplosiveArrows--;
                    uiManager.SetCurrentAmmo(currentExplosiveArrows, ninjaUI);
                    InstantiateArow(explosiveArrowObj);
                    break;

                case arrowTypes.Slow:
                    currentSlowArrows--;
                    uiManager.SetCurrentAmmo(currentSlowArrows, ninjaUI);
                    InstantiateArow(slowArrowObj);
                    break;
            }
            
            charging = false;
            charge = 0f;
            canShoot = false;

            mouseLook.Fire(false);
            weaponRecoil.Fire(false);
            animator.SetTrigger("Fire");
            audioManager.NetworkPlay("ShurikenShoot");

            yield return new WaitForSeconds(1f);

            canShoot = true;
        }
    }

    private void InstantiateArow(GameObject arrowType)
    {
        Ray ray = transform.parent.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000f);
        }

        GetComponentInParent<NetworkController>().NetworkSpawn(arrowType.name, emmiter.transform.position, emmiter.transform.rotation, (targetPoint - emmiter.transform.position).normalized * charge);
    }

    public void SetArrowMenuState(bool state)
    {
        uiManager.SetArrowMenuState(state, ninjaUI);
    }

    public void SetCurrentArrow(string name)
    {
        switch(name)
        {
            case "Fire":
                currentType = arrowTypes.Fire;
                uiManager.SetCurrentAmmo(currentFireArrows, ninjaUI);
                break;

            case "Regular":
                currentType = arrowTypes.Regular;
                uiManager.SetCurrentAmmo(currentRegularArrows, ninjaUI);
                break;

            case "Slow":
                currentType = arrowTypes.Slow;
                uiManager.SetCurrentAmmo(currentSlowArrows, ninjaUI);
                break;
                
            case "Explosive":
                currentType = arrowTypes.Explosion;
                uiManager.SetCurrentAmmo(currentExplosiveArrows, ninjaUI);
                break;
        }
    }

    public void RestockAmmo()
    {
        currentFireArrows = maxArrows;
        currentRegularArrows = maxArrows;
        currentSlowArrows = maxArrows;
        currentExplosiveArrows = maxArrows;
    }

    public bool CanAddAmmo()
    {
        if (currentFireArrows < maxArrows)
        {
            return true;
        }
        else if (currentRegularArrows < maxArrows)
        {
            return true;
        }
        else if (currentSlowArrows < maxArrows)
        {
            return true;
        }
        else if (currentExplosiveArrows < maxArrows)
        {
            return true;
        }

        return false;
    }
}
