using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slingshot : MonoBehaviour
{
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] GameObject grenadeLauncher;
    private AudioManager audioManager;
    [SerializeField] GameObject bulletEmitter;
    [SerializeField] GameObject muzzleFlash;

    [SerializeField] float cooldown = 0.5f;
    [SerializeField] float force = 20f;
    [SerializeField] int maxGrenades = 5;
    private int currentGrenades = 2;

    private bool grenading = false;

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
    }

    private void Update() 
    {
        UIManager.GetInstance().SetGrenadeCount(currentGrenades, null, GetComponentInChildren<KnightUI>());    
    }

    public void Grenade()
    {
        if(!GetComponent<KnightPlayerMovement>().isLocalPlayer)
        {
            return;
        }

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

        GetComponent<NetworkController>().NetworkSpawn(muzzleFlash.name, bulletEmitter.transform.position, bulletEmitter.transform.rotation, Vector3.zero);
        audioManager.NetworkPlay("GrenadeShoot");

        Camera mainCamera = gameObject.transform.Find("Main Camera").GetComponent<Camera>();

        GetComponent<NetworkController>().NetworkSpawn(grenadePrefab.name, bulletEmitter.transform.position, bulletEmitter.transform.rotation, mainCamera.transform.forward * force);

        SlingshotAnim.DoAnimation(false);

        yield return new WaitForSeconds(0.5f);

        weaponSwitch.SetCurrentWeapon(true);
        grenadeLauncher.SetActive(false);

        yield return new WaitForSeconds(cooldown);

        grenading = false;
    }

    public void AddGrenade(int n)
    {
        if(currentGrenades < maxGrenades)
        {
            currentGrenades += n;
        }
    }

    public bool CanAddGrenade()
    {
        if (currentGrenades < maxGrenades)
        {
            return true;
        }

        return false;
    }
}
