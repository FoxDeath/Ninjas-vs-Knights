using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    private bool hasObject = false;

    [SerializeField] float timeUntilSpawn = 5f;
    private float timer = 0f;

    //After the Instantiated object is destroyed, it calls Spawn() to create a new object.
    void Update()
    {
        if(!hasObject)
        {
            Spawn();
        }

        hasObject = Physics.Raycast(transform.position, Vector3.up, 5f);
    }

    //Instantiates the object after some time and resets the timer.
    void Spawn()
    {
        timer +=Time.deltaTime;

        if(timer >= timeUntilSpawn)
        {
            FindObjectOfType<NetworkController>().NetworkSpawn(objectPrefab.name, transform.position + Vector3.up * 2, Quaternion.identity, Vector3.zero);
            timer = 0f;
        }
    }
}
