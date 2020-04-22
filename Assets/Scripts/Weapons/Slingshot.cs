using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slingshot : MonoBehaviour
{
    [SerializeField] GameObject grenadePrefab;
    private AudioManager audioManager;
    private GameObject bulletEmitter;
    private ParticleSystem muzzleFlash;

    [SerializeField] float cooldown = 0.5f;
    [SerializeField] float force = 20f;
    [SerializeField] float maxGrenades = 5f;
    private float currentGrenades = 0f;

    private bool grenading;

    public float GetMaxGrenades() 
    {
        return maxGrenades;
    }

    public float GetCurrentGrenades()
    {
        return currentGrenades;
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        bulletEmitter = GameObject.Find("SlingshotGrenadeEmitter");
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    public void GrenadeInput(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Performed && !grenading && currentGrenades > 0f)
        {
            StartCoroutine(GrenadeBehaviour());
        }
    }

    //Shoots the grenades.
    IEnumerator GrenadeBehaviour() 
    {
        grenading = true;

        currentGrenades--;

        SlingshotAnim.DoAnimationTrue();

        yield return new WaitForSeconds(0.5f);

        muzzleFlash.Play();
        audioManager.Play("GrenadeShoot", GetComponent<AudioSource>());

        Camera mainCamera = gameObject.transform.Find("Main Camera").GetComponent<Camera>();
        GameObject grenade = Instantiate(grenadePrefab, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        rb.AddForce(mainCamera.transform.forward * force, ForceMode.VelocityChange);

        SlingshotAnim.DoAnimationFalse();

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
