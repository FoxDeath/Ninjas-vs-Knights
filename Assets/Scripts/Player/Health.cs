using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    private float health;

    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

        health = maxHealth;
        uiManager.SetMaxHealth(maxHealth);
    }

    //Updates the Health Bar.
    void Update()
    {
        uiManager.SetHealth(health);
    }

    //The Players health will go down when he takes damage.
    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Future.
    }
}
