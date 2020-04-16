using System.Collections;
using UnityEngine;
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

    [SerializeField] float maxHealth = 50f;
    private float health;
    
    private bool dead;
    private bool onFire;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        myRigidbody = GetComponent<Rigidbody>();
        movement = GetComponent<EnemyMovement>();
        health = maxHealth;
        healthBar.value = health;
        dead = false;
        onFire = false;
    }

    void Die()
    {
        dead = true;
        EndLevel.killedEnemies++;
        audioManager.Play("EnemyDying", GetComponent<AudioSource>());
        Destroy(gameObject, 1f);
    }

    //Calculates the health for the health bar slider.
    float CalculateHealth()
    {
        return health / maxHealth;
    }

    //Makes the Enemy take damage and updates its health bar.
    public void TakeDamage(float damage)
    {
        if (!dead)
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
        float ogSpeed = movement.GetSpeed();
        movement.SetSpeed(ogSpeed * 0.5f);

        yield return new WaitForSeconds(duration);

        movement.SetSpeed(ogSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name.Equals("ShurikenModel") || other.gameObject.name.Equals("SpearGunLaserModel"))
        {
            Vector3 force = transform.localPosition - other.transform.localPosition;
            myRigidbody.velocity = new Vector3(other.transform.right.x * force.x, other.transform.up.y * force.y, other.transform.forward.z * force.z);
        }
    }
}
