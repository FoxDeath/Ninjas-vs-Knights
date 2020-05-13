using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] points;
    private Transform target;
    private Transform previousTarget;
    private CharacterController playerCC;
    private PlayerMovement playerPM;
    private Rigidbody myRigidbody;

    [SerializeField] float travelTime = 8f;
    private float time = 0f;

    private int currentPoint;

    private bool forward;

    private enum types
    {
        Circle,
        Linear
    }

    [SerializeField] types currentType;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        if (points.Length != 0)
        {
            target = points[1];
            previousTarget = points[0];
        }

        forward = true;

        currentPoint = 0;
    }

    void FixedUpdate()
    {
        if(points.Length != 0)
        {
            if(Mathf.Abs(Vector3.Distance(transform.position, target.position)) < 0.1f)
            {
                switch(currentType)
                {
                    case types.Circle:
                        CircleBehaviour();
                        break;

                    case types.Linear:
                        if(forward)
                        {
                            LinearForwardBehaviour();
                        }
                        else
                        {
                            LinearBackwardsBehaviour();
                        }
                        break;
                }
            }
            
            Vector3 currentPos = Vector3.Lerp(previousTarget.position, target.position, Mathf.Cos(time / travelTime * Mathf.PI * 2) * -0.5f + 0.5f);
            time += Time.fixedDeltaTime;
            myRigidbody.MovePosition(currentPos);
        }
    }

    private void CircleBehaviour()
    {
        if(currentPoint + 1 < points.Length)
        {
            currentPoint += 1;
        }
        else
        {
            currentPoint = 0;
        }

        previousTarget = target;
        target = points[currentPoint];
        time = 0;
    }

    private void LinearForwardBehaviour()
    {
        if(!forward)
        {
            forward = !forward;
        }

        if(currentPoint + 1 < points.Length)
        {
            currentPoint += 1;
        }
        else
        {
            LinearBackwardsBehaviour();
            return;
        }

        previousTarget = target;
        target = points[currentPoint];
        time = 0;
    }

    private void LinearBackwardsBehaviour()
    {
        if(forward)
        {
            forward = !forward;
        }

        if(currentPoint - 1 >= 0)
        {
            currentPoint -= 1;
        }
        else
        {
            LinearForwardBehaviour();
            return;
        }

        previousTarget = target;
        target = points[currentPoint];
        time = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.GetType() == typeof(MeshCollider))
        {
            other.transform.parent.SetParent(transform.parent);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.transform.SetParent(transform.parent);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.GetType() == typeof(MeshCollider))
        {
            if(playerCC != other.transform.parent.GetComponent<CharacterController>())
            {
                playerCC = other.transform.parent.GetComponent<CharacterController>();
                playerPM = other.transform.parent.GetComponent<PlayerMovement>();
            }

            if(playerPM.GetGrounded() || playerPM.GetEdgeClimbing() || playerPM.GetEdgeHanging())
            {
                playerCC.Move(myRigidbody.velocity * Time.fixedDeltaTime);
            }
        }
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Rigidbody>().velocity = myRigidbody.velocity;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.GetType() == typeof(MeshCollider))
        {
            other.gameObject.transform.parent.SetParent(null);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.transform.parent.SetParent(transform.parent);
        }
    }
}