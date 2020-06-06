using UnityEngine;
using Mirror;

public class FlyingEnemyMovement : NetworkBehaviour
{
    private Transform target;
    private Target targetScript;

    private Vector3 avoidDir;

    private bool hitSomething;

    [SerializeField] float minDistanceFromPlayer = 15f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rayCastOffset = 1f;
    [SerializeField] float detectionRange = 1f;
    [SerializeField] float avoidSpeedMultiplyer = 5f;

    void Awake()
    {
        targetScript = GetComponent<Target>();
        transform.SetParent(GameObject.Find("EnemyContainer").transform);
    }

    public Transform GetTarget()
    {
        return target;
    }

    void Start()
    {
        InvokeRepeating("SearchForNearestPlayer", 0f, 1f);
    }

    void FixedUpdate()
    {
        if(!isServer)
        {
            return;
        }

        if(!targetScript.GetDead())
        {
            Pathfind();
        }
    }

    //Searches for the nearest player to target
    void SearchForNearestPlayer()
    {
        float closestdistance = -1f;

        foreach(var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            float distance = Vector3.Distance(this.transform.position, item.transform.position);
            
            if(distance < closestdistance || closestdistance == -1)
            {
                target = item.transform;
                closestdistance = distance;
            }
        } 
    }

    //Moves forwards or moves awy from the player depending on its distance
    void Move()
    {
        if(Vector3.Distance(transform.position, target.position) > minDistanceFromPlayer)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }

        if(Vector3.Distance(transform.position, target.position) < minDistanceFromPlayer)
        {
            Vector3 retreat = transform.forward;
            retreat.y *= -1;
            transform.position -= retreat * movementSpeed * Time.deltaTime;
        }
    }

    //Slowly turns towards the player
    void Turn()
    {
        Vector3 pos = target.position + new Vector3(0f, 10f, 0f) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    //Avoids stuff that are in its path
    void Pathfind()
    {
        RaycastHit hit;

        hitSomething = false;

        avoidDir = Vector3.zero;

        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        Debug.DrawRay(left, transform.forward * detectionRange, Color.cyan);
        Debug.DrawRay(right, transform.forward * detectionRange, Color.cyan);
        Debug.DrawRay(up, transform.forward * detectionRange, Color.cyan);
        Debug.DrawRay(down, transform.forward * detectionRange, Color.cyan);

        if (Physics.Raycast(left, transform.forward, out hit, detectionRange, 12))
        {
            avoidDir += Vector3.right;
            hitSomething = true;
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionRange, 12))
        {
            avoidDir -= Vector3.right;
            hitSomething = true;
        }

        if (Physics.Raycast(up, transform.forward, out hit, detectionRange, 12))
        {
            avoidDir -= Vector3.up;
            hitSomething = true;
        }
        else if (Physics.Raycast(down, transform.forward, out hit, detectionRange, 12))
        {
            avoidDir += Vector3.up;
            hitSomething = true;
        }

        if (hitSomething)
        {
            Quaternion rotation = Quaternion.LookRotation(avoidDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * avoidSpeedMultiplyer * Time.deltaTime);
        }
        else
        {
            if(target)
            {
                Turn();
            }
        }

        if(target)
        {
            Move(); 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            transform.position -= (other.transform.position - transform.position).normalized * Time.deltaTime;
        }
    }
}