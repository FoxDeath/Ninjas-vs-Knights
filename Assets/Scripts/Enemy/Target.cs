using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private AudioManager audioManager;
    public GameObject healthBarUI;
    public Slider healthBar;
    private EnemyMovement movement;

    private Coroutine fireEffectTimer;
    private Coroutine fireEffectBehaviour;
    private Coroutine slowDownEffectTimer;
    private Coroutine explodingBehaviour;

    private Quaternion ogRotation;

    [SerializeField] float maxHealth = 50f;
    private float health;
    
    private bool dead;
    private bool onFire;

    #region Getters and Setters

    public bool GetDead()
    {
        return dead;
    }

    #endregion

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        myRigidbody = GetComponent<Rigidbody>();
        movement = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        ogRotation = myRigidbody.rotation;
        health = maxHealth;
        healthBar.value = health;
        dead = false;
        onFire = false;
    }

    void Die()
    {
        audioManager.Play("EnemyDying", GetComponent<AudioSource>());
        dead = true;
        myRigidbody.isKinematic = false;
        myRigidbody.constraints = RigidbodyConstraints.None;

        if(GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }

        Destroy(gameObject, 5f);
    }

    //Calculates the health for the health bar slider.
    float CalculateHealth()
    {
        return health / maxHealth;
    }

    //Makes the Enemy take damage and updates its health bar.
    public void TakeDamage(float damage)
    {
        if(!dead)
        {
            audioManager.Play("Hit", GetComponent<AudioSource>());
            health -= damage;
            healthBar.value = health;

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
        if (slowDownEffectTimer != null)
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

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Ammo") && dead)
        {
            Vector3 force = transform.localPosition - other.transform.localPosition;
            myRigidbody.AddForce(force, ForceMode.Impulse);
        }
    }

    public void StartExploding(float damage, float explosionForce, Vector3 position, float radius)
    {
        if(explodingBehaviour == null)
        {
            explodingBehaviour = StartCoroutine(ExplodingBehaviour(damage, explosionForce, position, radius));
        }
        else
        {
            StopCoroutine(explodingBehaviour);
            explodingBehaviour = StartCoroutine(ExplodingBehaviour(damage, explosionForce, position, radius));
        }
    }

    IEnumerator ExplodingBehaviour(float damage, float explosionForce, Vector3 position, float radius)
    {
        myRigidbody.isKinematic = false;
        myRigidbody.constraints = RigidbodyConstraints.None;

        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }

        TakeDamage(damage);
        myRigidbody.AddExplosionForce(explosionForce, position, radius, 1f);

        yield return new WaitForSeconds(3f);

        myRigidbody.rotation = ogRotation;
        myRigidbody.isKinematic = true;
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        if (GetComponent<NavMeshAgent>() != null && !dead)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().SetDestination(movement.GetObjective().position);
        }
    }
}
