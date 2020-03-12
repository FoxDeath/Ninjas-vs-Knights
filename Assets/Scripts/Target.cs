using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health = 50f;
    private AudioManager audioManager;
    private bool dead;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void TakeDamage(float damage)
    {
        if(!dead)
        {
            audioManager.Play("Hit", GetComponent<AudioSource>());
            health -= damage;
            if(health <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        dead = true;
        audioManager.Play("EnemyDying", GetComponent<AudioSource>());
        Destroy(gameObject, 1f);
    }
}
