using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject objectPrefab;
    private GameObject objectInstance;

    private float timer = 0f;
    [SerializeField] float timeUntilSpawn = 5f;

    //After the Instantiated object is destroyed, it calls Spawn() to create a new object.
    void Update()
    {
        if(objectInstance == null)
        {
            Spawn();
        }
    }

    //Instantiates the object after some time.
    void Spawn()
    {
        timer +=Time.deltaTime;

        if(timer >= timeUntilSpawn)
        {
            objectInstance = Instantiate(objectPrefab, transform.position, Quaternion.identity, transform);
            timer = 0f;
        }
    }
}
