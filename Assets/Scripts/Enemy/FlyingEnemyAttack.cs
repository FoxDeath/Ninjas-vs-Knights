﻿using System.Collections;
using UnityEngine;

public class FlyingEnemyAttack : MonoBehaviour
{
    private Transform target;

    [SerializeField] GameObject projectile;

    private bool coolingDown;
    private bool flashed;

    [SerializeField] float range = 20f;

    public void SetFlashed(bool flashed)
    {
        this.flashed = flashed;
        Invoke("SetFlashedBack", 5f);
    }
    void SetFlashedBack()
    {
        flashed = false;
    }

    void Update()
    {
        target = GetComponent<FlyingEnemyMovement>().GetTarget();

        if(LineOfSight() && InRange() && !coolingDown && !flashed)
        {
            StartCoroutine(AttackBehaviour());   
        }
    }

    //Fires the Projectile towards the player
    IEnumerator AttackBehaviour()
    {
        coolingDown = true;
        FindObjectOfType<NetworkController>().NetworkSpawn(projectile.name, transform.position,
        Quaternion.LookRotation(target.position - transform.position), Vector3.zero, 2f);

        yield return new WaitForSeconds(2f);

        coolingDown = false;
    }

    //Checks if the enemy has line of sight to the player
    bool LineOfSight()
    {
        if(!target)
        {
            return false;
        }
        RaycastHit hit;

        Vector3 dir = target.position - transform.position; 

        Debug.DrawRay(transform.position, dir, Color.red);

        if(Physics.Raycast(transform.position, dir, out hit, range))
        {
            if(hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    //Checks if the player is in range of th enemy
    bool InRange()
    {
        if(!target)
        {
            return false;
        }
        return Vector3.Distance(transform.position, target.position) <= range;
    }
}