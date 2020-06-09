using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Mirror;

public class Target : NetworkBehaviour
{
    private Rigidbody myRigidbody;
    private AudioManager audioManager;
    public Slider healthBar;
    private GroundEnemyMovement movement;

    private Coroutine fireEffectTimer;
    private Coroutine fireEffectBehaviour;
    private Coroutine slowDownEffectTimer;
    private Coroutine explodingBehaviour;

    private Quaternion ogRotation;

    private Vector3 lastHit;

    [SerializeField] float maxHealth = 50f;

    [SyncVar] private float health;
    
    [SyncVar] private bool dead;
    private bool onFire;
    private bool exploding;

    #region Getters and Setters

    public bool GetDead()
    {
        return dead;
    }

    public bool GetExploding()
    {
        return exploding;
    }

    #endregion

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        myRigidbody = GetComponent<Rigidbody>();
        movement = GetComponent<GroundEnemyMovement>();
    }

    void Start()
    {
        ogRotation = myRigidbody.rotation;
        health = maxHealth;
        healthBar.value = health;
        dead = false;
        onFire = false;
        exploding = false;
    }

    void Update()
    {
        if(audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        healthBar.value = health;
    }

    //Makes the target ragdoll
    [Server]
    void Die()
    {
        dead = true;
        audioManager.NetworkPlay("EnemyDying", GetComponent<AudioSource>());
        myRigidbody.isKinematic = false;
        myRigidbody.constraints = RigidbodyConstraints.None;

        if(GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        
        myRigidbody.AddForce(lastHit * 10f, ForceMode.Impulse);

        NetworkServer.Destroy(gameObject);

        //StartCoroutine(FindObjectOfType<NetworkController>().NetworkDestroy(gameObject, 5f));
    }

    //Calculates the health for the health bar slider.
    float CalculateHealth()
    {
        return health / maxHealth;
    }

    //Makes the Enemy take damage and updates its health bar.
    [Server]
    public void TakeDamage(float damage)
    {
        if (!dead)
        {
            audioManager.NetworkPlay("Hit", GetComponent<AudioSource>());
            health -= damage;

            if (health <= 0f)
            {
                Die();
            }
        }
    }

    public void SetOnFire(float duration, float damage)
    {
        if(fireEffectTimer != null && fireEffectBehaviour != null)
        {
            StopCoroutine(fireEffectTimer);
            StopCoroutine(fireEffectBehaviour);
        }
        
        fireEffectTimer = StartCoroutine(FireEffectTimer(duration));
        fireEffectBehaviour = StartCoroutine(OnFireBehaviour(damage));
    }

    public void SlowDown(float duration)
    {
        if(slowDownEffectTimer != null)
        {
            StopCoroutine(slowDownEffectTimer);
        }

        slowDownEffectTimer = StartCoroutine(SlowDownEffectTimer(duration));
    }

    IEnumerator OnFireBehaviour(float damage)
    {
        while(onFire)
        {
            yield return new WaitForSeconds(1f);

            TakeDamage(damage);
        }
    }

    IEnumerator FireEffectTimer(float duration)
    {
        onFire = true;

        yield return new WaitForSeconds(duration);

        onFire = false;
    }

    IEnumerator SlowDownEffectTimer(float duration)
    {
        float ogSpeed = movement.GetAgentSpeed();
        movement.SetAgentSpeed(ogSpeed * 0.5f);

        yield return new WaitForSeconds(duration);

        movement.SetAgentSpeed(ogSpeed);
    }

    public void StartExploding(float damage, float explosionForce, Vector3 position, float radius, GameObject obj)
    {
        if(explodingBehaviour == null)
        {
            explodingBehaviour = StartCoroutine(ExplodingBehaviour(damage, explosionForce, position, radius, obj));
        }
        else
        {
            StopCoroutine(explodingBehaviour);
            explodingBehaviour = StartCoroutine(ExplodingBehaviour(damage, explosionForce, position, radius, obj));
        }
    }

    IEnumerator ExplodingBehaviour(float damage, float explosionForce, Vector3 position, float radius, GameObject obj)
    {
        exploding = true;
        myRigidbody.isKinematic = false;
        myRigidbody.constraints = RigidbodyConstraints.None;

        if(GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }

        TakeDamage(damage);

        if(obj.tag.Equals("GroundEnemy"))
        {
            myRigidbody.AddExplosionForce(explosionForce, position, radius, 1f);    
        }
        else if(obj.tag.Equals("FlyingEnemy"))
        {
            myRigidbody.AddExplosionForce(explosionForce * 0.1f, position, radius, 1f);
        }

        if(!obj.GetComponent<Target>().GetDead())
        {
            yield return new WaitForSeconds(2f);

            myRigidbody.rotation = ogRotation;
            myRigidbody.isKinematic = true;
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            if (GetComponent<NavMeshAgent>() != null && !dead)
            {
                GetComponent<NavMeshAgent>().enabled = true;
                GetComponent<NavMeshAgent>().SetDestination(movement.GetObjective());
            }

            exploding = false;
        }
        else
        {
            yield return null;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Ammo"))
        {
            lastHit = transform.localPosition - other.transform.localPosition;
        }
    }
}
