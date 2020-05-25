using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform objective;
    private Transform player;
    private Target target;
    private Rigidbody myRigidbody;
    private NavMeshAgent agent;

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

    public Transform GetObjective()
    {
        return objective;
    }

    #endregion

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        objective = GameObject.FindGameObjectWithTag("EnemyObjective").transform;
        myRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GetComponent<Target>();
    }

    void Start()
    {
        agent.SetDestination(objective.position);
        agent.updateRotation = false;
    }

    void FixedUpdate()
    {
        if(!target.GetDead())
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

    //Makes the Enemy face the target position.
    void FaceTarget(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
