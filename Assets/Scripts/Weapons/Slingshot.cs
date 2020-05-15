using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slingshot : MonoBehaviour
{
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] GameObject grenadeLauncher;
    private AudioManager audioManager;
    [SerializeField] GameObject bulletEmitter;
    private ParticleSystem muzzleFlash;

    [SerializeField] float cooldown = 0.5f;
    [SerializeField] float force = 20f;
    [SerializeField] int maxGrenades = 5;
    private int currentGrenades = 0;

    private bool grenading = false;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    private void Update() 
    {
        UIManager.GetInstance().SetGrenadeCount(currentGrenades);    
    }

    public void Grenade()
    {
        if(!grenading && currentGrenades > 0)
        {
            StartCoroutine(GrenadeBehaviour());
        }
    }

    //Shoots the grenades.
    IEnumerator GrenadeBehaviour() 
    {
        WeaponSwitch weaponSwitch = FindObjectOfType<WeaponSwitch>();
        grenading = true;

        currentGrenades--;

        grenadeLauncher.SetActive(true);
        weaponSwitch.SetCurrentWeapon(false);

        SlingshotAnim.DoAnimation(true);

        yield return new WaitForSeconds(0.5f);

        muzzleFlash.Play();
        audioManager.Play("GrenadeShoot", GetComponent<AudioSource>());

        Camera mainCamera = gameObject.transform.Find("Main Camera").GetComponent<Camera>();
        GameObject grenade = Instantiate(grenadePrefab, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        rb.AddForce(mainCamera.transform.forward * force, ForceMode.VelocityChange);

        SlingshotAnim.DoAnimation(false);

        yield return new WaitForSeconds(0.5f);

        weaponSwitch.SetCurrentWeapon(true);
        grenadeLauncher.SetActive(false);

        yield return new WaitForSeconds(cooldown);

        grenading = false;
    }

    //Picks up the grenade if you can carry them.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Grenade") && currentGrenades < maxGrenades)
        {
            audioManager.Play("Pickup", GetComponent<AudioSource>());
            currentGrenades++;
            Destroy(other.gameObject);
        }
    }
}
