using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyMovement : MonoBehaviour
{
    private Transform objective;
    private Transform player;
    private Mirror.NetworkTransformChild networkTransformChild;
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

    #endregion

    void Awake()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        agent = GetComponent<NavMeshAgent>();
        objective = GameObject.FindGameObjectWithTag("EnemyObjective").transform;
        myRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GetComponent<Target>();

        transform.SetParent(GameObject.Find("EnemyContainer").transform);
        networkTransformChild = transform.parent.gameObject.AddComponent<Mirror.NetworkTransformChild>();
        networkTransformChild.target = transform;
    }

    void OnDestroy() 
    {
        Destroy(networkTransformChild);
    }

    void Start()
    {
        agent.SetDestination(GetObjective());
        agent.updateRotation = false;
    }

    void FixedUpdate()
    {
        CheckProgress();
        Facing();
    }

    private void CheckProgress()
    {
        if(agent.transform.position == agent.destination)
        {
            agent.enabled = false;
            obstacle.enabled = true;
        }
        else if(!target.GetExploding())
        {
            agent.enabled = true;
            obstacle.enabled = false;
        }
    }

    //Faces its target
    private void Facing()
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
