using System.Collections;
using UnityEngine;

public class SlingshotGrenade : MonoBehaviour
{
    [SerializeField] GameObject expEffect;
    private AudioManager audioManager;

    [SerializeField] float delay = 3f;
    [SerializeField] float radius = 15f;
    [SerializeField] float force = 500f;
    [SerializeField] float damage = 20f;
    private float countdown;

    private bool exploded;

    void Awake()
    {
        foreach(AudioManager audioManager in FindObjectsOfType<AudioManager>())
        {
            if(audioManager.isLocalPlayer)
            {
                this.audioManager = audioManager;
            }
        }
    }

    void Start()
    {
        countdown = delay;
    }

    void FixedUpdate()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0f && !exploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        exploded = true;
        FindObjectOfType<NetworkController>().NetworkSpawn(expEffect.name, transform.position, transform.rotation, Vector3.zero);
        GameObject emptyWithAudioSource = new GameObject();
        emptyWithAudioSource.AddComponent<AudioSource>();
        GameObject expSound = Instantiate(emptyWithAudioSource, transform.position, transform.rotation);
        audioManager.NetworkPlay("GrenadeExplode", expSound.GetComponent<AudioSource>());
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider collider in colliders)
        {
            Target target = collider.GetComponent<Target>();

            if(target != null)
            {
                target.StartExploding(damage, force, transform.position, radius);
            }
        }

        Destroy(expSound, 2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!exploded)
        {
            audioManager.NetworkPlay("GrenadeBounce", GetComponent<AudioSource>());
        }
    }
}
