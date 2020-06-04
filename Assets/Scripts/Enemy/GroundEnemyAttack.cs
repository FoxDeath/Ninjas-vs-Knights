using System;
using System.Collections;
using UnityEngine;


public class GroundEnemyAttack : EnemyAttack
{
    protected Transform objective;

    protected override void Awake()
    {
      //  objective = GameObject.FindGameObjectWithTag("EnemyObjective").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = FindObjectOfType<AudioManager>();
        target = GetComponent<Target>();
    }

    void Start()
    {
        shootRadius = GetComponent<GroundEnemyMovement>().GetLookRadius();
    }
    
    protected override void FixedUpdate()
    {
        AquireTarget();

        if(!target.GetDead() && nextTimeToFire <= 0)
        {
            if(Vector3.Distance(transform.position, objective.position) <= shootRadius)
            {
                Shoot();
            }
            if(Vector3.Distance(transform.position, player.position) <= shootRadius)
            {
                Shoot();
            }
        }

        nextTimeToFire -= Time.fixedDeltaTime;
    }
}
