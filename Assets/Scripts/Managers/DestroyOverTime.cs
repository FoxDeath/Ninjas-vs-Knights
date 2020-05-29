using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float timeToDestroy;

    //Destroys the object after the timeToDestroy runs out.
    void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
