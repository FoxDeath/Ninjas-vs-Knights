using System.Collections;
using UnityEngine;

public class ShurikenGun :  MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletEmiter;
    [SerializeField] GameObject muzzleFlash;
    private AudioManager audioManager;
    private UIManager uiManager;
    private NinjaUI ninjaUI;
    private Animator animator;
    private PlayerMovement playerMovement;
    
    [SerializeField] float speed = 100f;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] float fireRate = 15f;
    [SerializeField] float spread = 0.01f;
    private float nextTimeToFire = 0f;

    [SerializeField] int maxAmmo = 10;
    [SerializeField] int maxMag = 5;
    private int currentAmmo;
    private int currentMag;

    private bool reloading;
    private bool scoping;
    private bool equiped;

    public void SetReloading(bool reloading)
    {
        this.reloading = reloading;
    }

    public bool GetScoping()
    {
        return scoping;
    }
    
    public void SetEquiped(bool equiped)
    {
        this.equiped = equiped;
    }

    //The Gun starts with maximum ammo.
    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        audioManager = GetComponentInParent<AudioManager>();
        animator = GetComponent<Animator>();
        ninjaUI = transform.parent.parent.GetComponentInChildren<NinjaUI>();
        uiManager = UIManager.GetInstance();
    }

    void Start()
    {
        equiped = transform.GetChild(0).gameObject.activeSelf;

        maxMag = maxAmmo * 4;
        currentAmmo = maxAmmo;
        currentMag = maxMag;

        if(equiped)
        {
            uiManager.SetMaxAmmo(currentMag, ninjaUI);
            uiManager.SetCurrentAmmo(currentAmmo, ninjaUI);
        }
    }

    void Update()
    {
        if(!equiped)
        {
            return;
        }
        ManageAmmo();

        uiManager.SetCurrentAmmo(currentAmmo, ninjaUI);
        uiManager.SetMaxAmmo(currentMag, ninjaUI);

        if(playerMovement.GetMoving())
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

    //Makes the Gun fire a temporary shuriken, and destroyes that temporary shuriken after a few seconds.
    //The shuriken has spread, but only when the Gun isn't scoped.
    public void Fire()
    {
        if(!GetComponentInParent<NinjaPlayerMovement>().isLocalPlayer)
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
                if (Physics.Raycast(ray, out hit))
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
                audioManager.NetworkPlay("ShurikenShoot");

                targetPoint.x += Random.Range(-spread, spread);
                targetPoint.y += Random.Range(-spread, spread);

                GetComponentInParent<NetworkController>().NetworkSpawn(bullet.name, bulletEmiter.transform.position, bulletEmiter.transform.rotation,
                (targetPoint - bulletEmiter.transform.position).normalized * speed);
                animator.SetTrigger("Firing");
            }
            else
            {
                Reload();
            }
        }
    }

    public void Scope(bool state)
    {
        if(!GetComponentInParent<NinjaPlayerMovement>().isLocalPlayer)
        {
            return;
        }

        scoping = state;
        animator.SetBool("Scoped", state);
        playerMovement.SetScoping(state);
        playerMovement.Sprint(false);
    }

    public void Reload()
    {
        if(reloading || !GetComponentInParent<NinjaPlayerMovement>().isLocalPlayer)
        {
            return;
        }

        if((currentAmmo < maxAmmo) && (currentMag > 0))
        {
            Scope(false);
            StartCoroutine(ReloadingBehaviour());
        }
    }

    public void RestockAmmo()
    {
        currentMag = maxMag;
    }

    IEnumerator ReloadingBehaviour()
    {
        reloading = true;

        audioManager.NetworkPlay("Reload");

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
