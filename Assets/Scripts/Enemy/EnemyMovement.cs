using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float lookRadius = 20f;
    [SerializeField] float stoppingDistance = 10f;
    [SerializeField] float retreatDistance = 7f;
    private float retreatSpeed;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        retreatSpeed = speed * -1.5f;
    }

    //The Enemy will approach the Player only when the Player enters his line of sight.
    //The Enemy will stop once he is close enough to shoot the Player.
    //The Enemy will start retreating when the Player gets too close to him.
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= lookRadius)
        {
            FaceTarget();
        }

        if(distance <= lookRadius && distance >= stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if(distance <= stoppingDistance)
        {
            transform.position = this.transform.position;
        }

        if(distance < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, retreatSpeed * Time.deltaTime);
        }  
    }

    //Makes the Enemy face the Player.
    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
