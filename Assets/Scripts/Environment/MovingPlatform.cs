using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] points;
    private Transform target;
    private CharacterController playerCC;
    private PlayerMovement playerPM;
    private Rigidbody myRigidbody;

    private Vector3 prevPos;
    private Vector3 newPos;
    private Vector3 objVelocity;

    [SerializeField] float speed = 5f;

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

        target = points[1];
        prevPos = transform.position;
        newPos = transform.position;

        forward = true;
        currentPoint = 0;
    }

    void FixedUpdate()
    {
        newPos = transform.position;
        objVelocity = (newPos - prevPos) / Time.fixedDeltaTime;
        prevPos = newPos;

        if (points.Length != 0)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, target.position)) < 0.5f)
            {
                switch (currentType)
                {
                    case types.Circle:
                        CircleBehaviour();
                        break;

                    case types.Linear:
                        if (forward)
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

            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.fixedDeltaTime * speed);
        }
    }

    private void CircleBehaviour()
    {
        if (currentPoint + 1 < points.Length)
        {
            currentPoint += 1;
        }
        else
        {
            currentPoint = 0;
        }

        target = points[currentPoint];
    }

    private void LinearForwardBehaviour()
    {
        if (!forward)
        {
            forward = !forward;
        }

        if (currentPoint + 1 < points.Length)
        {
            currentPoint += 1;
        }
        else
        {
            LinearBackwardsBehaviour();
            return;
        }

        target = points[currentPoint];
    }

    private void LinearBackwardsBehaviour()
    {
        if (forward)
        {
            forward = !forward;
        }

        if (currentPoint - 1 >= 0)
        {
            currentPoint -= 1;
        }
        else
        {
            LinearForwardBehaviour();
            return;
        }

        target = points[currentPoint];
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.GetType() == typeof(MeshCollider))
        {
            other.gameObject.transform.parent.SetParent(transform.parent);
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
                playerCC.Move(objVelocity * Time.deltaTime);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.GetType() == typeof(MeshCollider))
        {
            other.gameObject.transform.parent.SetParent(null);
        }
    }
}