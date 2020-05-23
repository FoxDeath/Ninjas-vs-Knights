using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttack : MonoBehaviour
{
    private Transform target;

    private bool coolingDown;

    [SerializeField] float range = 20f;
    [SerializeField] float damage = 20f;

    void Update()
    {
        target = GetComponent<FlyingEnemyMovement>().GetTarget();
        if (LineOfSight() && InRange() && !coolingDown)
        {
            StartCoroutine(AttackBehaviour());   
        }
    }

    IEnumerator AttackBehaviour()
    {
        coolingDown = true;
        target.GetComponent<Health>().TakeDamage(damage);
        yield return new WaitForSeconds(2f);
        coolingDown = false;
    }

    bool LineOfSight()
    {
        RaycastHit hit;

        Vector3 dir = target.position - transform.position; 

        Debug.DrawRay(transform.position, dir, Color.red);

        if (Physics.Raycast(transform.position, dir, out hit, range))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    bool InRange()
    {
        return Vector3.Distance(transform.position, target.position) <= range;
    }
}