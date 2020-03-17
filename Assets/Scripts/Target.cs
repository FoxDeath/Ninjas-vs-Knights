using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health = 50f;
    private AudioManager audioManager;
    public bool charged;
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
            if (charged)
            {
                StartCoroutine("ResetCharged");
            }
        }
    }

    IEnumerator ResetCharged()
    {
        yield return new WaitForSeconds(2.5f);
        charged = false;
    }

    void Die()
    {
        dead = true;
        EndLevel.killedEnemies++;
        audioManager.Play("EnemyDying", GetComponent<AudioSource>());
        Destroy(gameObject, 1f);
    }
}
