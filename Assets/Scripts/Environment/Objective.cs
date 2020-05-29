using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    private Slider healthBar;

    private Transform player;
    
    [SerializeField] float maxHealth = 1000f;
    private float health;

    void Awake()
    {
        healthBar = GetComponentInChildren<Slider>();
        
        foreach(var player in FindObjectsOfType<PlayerMovement>())
        {
            if(player.isLocalPlayer)
            {
                this.player = player.transform;
            }
        }
    }

    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    void FixedUpdate()
    {
        if(player)
        {
            healthBar.transform.parent.LookAt(player);
        }
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0f, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;

        if(health <= 0f)
        {
            //game ends here
        }
    }
}
