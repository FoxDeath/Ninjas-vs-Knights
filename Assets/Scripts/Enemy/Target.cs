using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private AudioManager audioManager;
    public GameObject healthBarUI;
    public Slider healthBar;

    [SerializeField] float maxHealth = 50f;
    private float health;
    
    private bool dead;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        myRigidbody = GetComponent<Rigidbody>();
        health = maxHealth;
        dead = false;
        healthBar.value = health;
    }

    //Makes the Enemy take damage and updates its health bar.
    public void TakeDamage(float damage)
    {
        if(!dead)
        {
            audioManager.Play("Hit", GetComponent<AudioSource>());
            health -= damage;
            healthBar.value = health;

            if(health <= 0f)
            {
                Die();
            }
        }
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

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name.Equals("ShurikenModel") || other.gameObject.name.Equals("SpearGunLaserModel"))
        {
            Vector3 force = transform.localPosition - other.transform.localPosition;
            myRigidbody.velocity = new Vector3(other.transform.right.x * force.x, other.transform.up.y * force.y, other.transform.forward.z * force.z);
        }
    }
}
