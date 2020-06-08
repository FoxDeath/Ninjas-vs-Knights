using System.Collections;
using UnityEngine;

public class SpearGun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject chargeBullet;
    [SerializeField] Transform bulletEmiter;
    private GameObject model;
    private UIManager uiManager;
    private KnightUI knightUI;
    private AudioManager audioManager;
    [SerializeField] GameObject muzzleFlash;
    private Animator animator;
    private MouseLook mouseLook;
    private WeaponRecoil weaponRecoil;

    [SerializeField] float speed = 100f;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] float fireRate = 15f;
    [SerializeField] float spread = 0.01f;
    private float nextTimeToFire = 0f;
    private float altSpeed;

    [SerializeField] int maxAmmo = 10;
    [SerializeField] int maxMag;
    private int currentAmmo;
    private int currentMag;

    private Quaternion startingRotation;

    private bool reloading;
    private bool equiped;

    public Quaternion GetStartingRotation()
    {
        return startingRotation;
    }

    public void SetReloading(bool reloading)
    {
        this.reloading = reloading;
    }

    public void SetEquiped(bool equiped)
    {
        this.equiped = equiped;
    }

    void Awake()
    {
        uiManager = UIManager.GetInstance();
        knightUI = transform.parent.parent.GetComponentInChildren<KnightUI>();
        audioManager = GetComponentInParent<AudioManager>();
        animator = GetComponent<Animator>();
        model = transform.GetChild(0).gameObject;
        mouseLook = GetComponentInParent<MouseLook>();
        weaponRecoil = GetComponent<WeaponRecoil>();
    }

    void Start()
    {
        equiped = transform.GetChild(0).gameObject.activeSelf;

        maxMag = maxAmmo * 4;
        currentAmmo = maxAmmo;
        currentMag = maxMag;

        if(equiped)
        {
            uiManager.SetMaxAmmo(currentMag, null, knightUI);
            uiManager.SetCurrentAmmo(currentAmmo, null, knightUI);
        }

        altSpeed = speed / 2;
        startingRotation = transform.localRotation;
    }

    void Update()
    {
        if(!equiped)
        {
            return;
        }
        ManageAmmo();

        uiManager.SetMaxAmmo(currentMag, null, knightUI);
        uiManager.SetCurrentAmmo(currentAmmo, null, knightUI);

        if(GetComponentInParent<PlayerMovement>().GetMoving())
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void ManageAmmo()
    {
        if(currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        if(currentMag > maxMag)
        {
            currentMag = maxMag;
        }

        if(currentAmmo < 0)
        {
            currentAmmo = 0;
        }
        
        if(currentMag < 0)
        {
            currentMag = 0;
        }
    }

    //Reloads the gun.
    public void Reload()
    {
        if(reloading || !GetComponentInParent<KnightPlayerMovement>().isLocalPlayer || !equiped)
        {
            return;
        }

        if((currentAmmo < maxAmmo) && (currentMag > 0))
        {
            StartCoroutine(ReloadingBehaviour());
        }
    }

    //Makes the Gun fire a temporary bullet and destroyes that temporary bullet after a few seconds.
    public void Fire()
    {
        if(!GetComponentInParent<KnightPlayerMovement>().isLocalPlayer)
        {
            return;
        }

        if(Time.time >= nextTimeToFire && !reloading)
        {
            if(currentAmmo > 0)
            {
                Ray ray = transform.parent.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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

                currentAmmo--;
                nextTimeToFire = Time.time + 1f / fireRate;
                GetComponentInParent<NetworkController>().NetworkSpawn(muzzleFlash.name, bulletEmiter.transform.position, bulletEmiter.transform.rotation, Vector3.zero);
                audioManager.NetworkPlay("Laser");

                targetPoint.x += Random.Range(-spread, spread);
                targetPoint.y += Random.Range(-spread, spread);

                GetComponentInParent<NetworkController>().NetworkSpawn(bullet.name, bulletEmiter.transform.position, bulletEmiter.transform.rotation,
                (targetPoint - bulletEmiter.position).normalized * speed);
                animator.SetTrigger("Firing");
                mouseLook.Fire(false);
                weaponRecoil.Fire(false);
            }
            else
            {
                Reload();
            }
        }
    }

    public void Charge()
    {
        if(!GetComponentInParent<KnightPlayerMovement>().isLocalPlayer)
        {
            return;
        }

        if(Time.time >= nextTimeToFire && !reloading && currentAmmo >= 3)
        {
            if(currentAmmo >= 3)
            {
                Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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

                currentAmmo -= 3;
                nextTimeToFire = Time.time + 1f / fireRate;
                GetComponentInParent<NetworkController>().NetworkSpawn(muzzleFlash.name, bulletEmiter.transform.position, bulletEmiter.transform.rotation, Vector3.zero);
                audioManager.NetworkPlay("Laser");

                targetPoint.x += Random.Range(-spread, spread);
                targetPoint.y += Random.Range(-spread, spread);

                GetComponentInParent<NetworkController>().NetworkSpawn(chargeBullet.name, bulletEmiter.transform.position, bulletEmiter.transform.rotation,
                (targetPoint - bulletEmiter.transform.position).normalized * altSpeed);
                animator.SetTrigger("Firing");
            }
            else
            {
                StartCoroutine(ReloadingBehaviour());

                return;
            }
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

    IEnumerator ReloadingBehaviour()
    {
        reloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        if(currentMag >= maxAmmo - currentAmmo)
        {
            currentMag -= maxAmmo - currentAmmo;
            currentAmmo = maxAmmo;
        }

        if(currentMag < maxAmmo - currentAmmo)
        {
            currentAmmo += currentMag;
            currentMag = 0;
        }

        reloading = false;
    }
}
