using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyMovement : MonoBehaviour
{
    private Transform objective;
    private Transform player;
    private Target target;
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

    public Vector3 GetObjective()
    {
        return objective.position + (Vector3)Random.insideUnitCircle * 15f;
    }

    #endregion

    void Awake()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        agent = GetComponent<NavMeshAgent>();
        objective = GameObject.FindGameObjectWithTag("EnemyObjective").transform;
        myRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GetComponent<Target>();
    }

    void Start()
    {
        agent.SetDestination(GetObjective());
        agent.updateRotation = false;
    }

    void FixedUpdate()
    {
        // if(agent.pathStatus == NavMeshPathStatus.PathComplete)
        // {
        //     print(1);
        //     obstacle.carving = true;
        // }
        // else
        // {
        //     print(2);
        //     obstacle.carving = false;
        // }

        if(!target.GetDead())
        {
            float playerDistance = Vector3.Distance(transform.position, player.position);
            float objectiveDistance = Vector3.Distance(transform.position, objective.position);

            if(objectiveDistance <= lookRadius)
            {
                FaceTarget(objective.position);

                // if (agent.desiredVelocity != agent.velocity)
                // {
                //     agent.SetDestination(objective.position + (Vector3)Random.insideUnitCircle * 15);
                // }
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

    //Makes the Enemy face the target position.
    void FaceTarget(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
