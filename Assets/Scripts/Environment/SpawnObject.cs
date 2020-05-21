using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject objectPrefab;
    private GameObject objectInstance;

    [SerializeField] float timer = 0f;

    void Start()
    {
        if(objectInstance == null)
        {
            objectInstance = Instantiate(objectPrefab, transform.position, Quaternion.identity);
        }
    }

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

        if(timer >= 5f)
        {
            objectInstance = Instantiate(objectPrefab, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
