using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class GroundEnemyMovement : NetworkBehaviour
{
    private Transform objective;
    private GameObject[] players;
    private Transform player;
    private Target target;
    private GroundEnemyAttack groungEnemyAttack;
    private Rigidbody myRigidbody;
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;

    [SerializeField] float lookRadius = 20f;

    #region Getters and Setters

    public float GetAgentSpeed()
    {
        return agent.speed;
    }

    public void SetAgentSpeed(float speed)
    {
        agent.speed = speed;
    }

    public float GetLookRadius()
    {
        return lookRadius;
    }

    #endregion

    void Awake()
    {
        groungEnemyAttack = GetComponent<GroundEnemyAttack>();
        obstacle = GetComponent<NavMeshObstacle>();
        agent = GetComponent<NavMeshAgent>();
        objective = GameObject.FindGameObjectWithTag("EnemyObjective").transform;
        myRigidbody = GetComponent<Rigidbody>();
        players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0].transform;
        target = GetComponent<Target>();

        transform.SetParent(GameObject.Find("EnemyContainer").transform);
    }

    public override void OnStartServer()
    {
        agent.enabled = true;
        base.OnStartServer();
    }

    void Start()
    {
        if(!isServer)
        {
            return;
        }
        
        agent.SetDestination(GetObjective());
        agent.updateRotation = false;
        InvokeRepeating("SearchForNearestPlayer", 0f, 1f);
    }

    void FixedUpdate()
    {
        if(!isServer)
        {
            return;
        }

        foreach(GameObject player in players)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < Vector3.Distance(this.player.position, transform.position))
            {
                this.player = player.transform;
            }
        }

        CheckProgress();
        Facing();

        if(!groungEnemyAttack.GetFlashed() && agent.isStopped)
        {
            agent.isStopped = false;
        }
        else if(groungEnemyAttack.GetFlashed() && !agent.isStopped)
        {
            agent.isStopped = true;
        }
    }

    //Searches for the nearest player to target
    void SearchForNearestPlayer()
    {
        float closestdistance = -1f;

        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            float distance = Vector3.Distance(this.transform.position, item.transform.position);

            if (distance < closestdistance || closestdistance == -1)
            {
                player = item.transform;
                closestdistance = distance;
            }
        }
    }

    private void CheckProgress()
    {
        if(agent.transform.position == agent.destination)
        {
            agent.enabled = false;
            obstacle.enabled = true;
        }
        else if(!target.GetExploding() && !target.GetDead())
        {
            agent.enabled = true;
            obstacle.enabled = false;
        }
    }

    //Faces its target
    private void Facing()
    {
        if(!target.GetDead() && player)
        {
            float playerDistance = Vector3.Distance(transform.position, player.position);
            float objectiveDistance = Vector3.Distance(transform.position, objective.position);

            if(objectiveDistance <= lookRadius)
            {
                FaceTarget(objective.position);
            }
            else if(playerDistance <= lookRadius)
            {
                FaceTarget(player.position);
            }
            else
            {
                FaceTarget(agent.steeringTarget);
            }
        }
    }

    public Vector3 GetObjective()
    {
        float angle = Random.Range(0f, 360f);
        float dist = Random.Range(8f, 15f);
        var x = dist * Mathf.Cos(angle * Mathf.Deg2Rad);
        var z = dist * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 objPos = objective.position;
        objPos.x += x;
        objPos.z += z;

        return objPos;
    }

    //Makes the Enemy face the target position.
    void FaceTarget(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
