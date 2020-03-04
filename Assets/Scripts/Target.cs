using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health = 50f;

    public void TakeDamage(float damage)
    {
        FindObjectOfType<AudioManager>().Play("Hit", GetComponent<AudioSource>());
        health -= damage;
        if(health <= 0f)
        {
            Invoke("Die", 1f);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
