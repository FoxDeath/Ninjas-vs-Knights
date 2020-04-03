using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float regenPerSec = 5f;
    [SerializeField] float damageReduPercentage = 50f;
    private float health;

    private bool regenerating;

    private UIManager uiManager;

    public float GetMaxHealt() 
    {
        return maxHealth;
    }

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

    private void FixedUpdate()
    {
        health = Mathf.Clamp(health, 0f, maxHealth);
        Regenerate();
    }

    //The Players health will go down when he takes damage.
    public void TakeDamage(float damage)
    {
        PlayerMovement movement = GetComponent<KnightPlayerMovement>();
        if(movement != null && movement.GetScoping())
        {
            health -= damage * (damageReduPercentage / 100);
        }
        else
        {
            health -= damage;
        }

        regenerating = false;

        if(health <= 0)
        {
            Die();
        }

        StopAllCoroutines();
        StartCoroutine(RegenCooldown());
    }

    IEnumerator RegenCooldown() 
    {
        yield return new WaitForSeconds(3f);

        regenerating = true;
    }

    public void Heal(float ammount) 
    {
        health += ammount;
    }

    void Die()
    {
        //Future.
    }

    private void Regenerate() 
    {
        if(regenerating && health < maxHealth)
        {
            health += regenPerSec * Time.deltaTime;
        }
        else if(health == maxHealth)
        {
            regenerating = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("heal bic");
        Medkit medkit = other.GetComponent<Medkit>();
        if (medkit && health < maxHealth)
        {
            Heal(medkit.GetHealAmmount());
            GameObject.Destroy(other.gameObject);
        }
    }
}
