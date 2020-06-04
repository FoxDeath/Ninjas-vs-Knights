using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private NetworkController networkController;

    [SerializeField] GameObject objectPrefab;

    private bool hasObject = false;

    [SerializeField] float timeUntilSpawn = 5f;
    private float timer = 0f;

    //After the Instantiated object is destroyed, it calls Spawn() to create a new object.
    void Update()
    {
        if(transform.childCount == 0)
        {
            Spawn();
        }
    }

    //Instantiates the object after some time and resets the timer.
    void Spawn()
    {
        timer += Time.deltaTime;

        if(timer >= timeUntilSpawn)
        {
            if(!networkController)
            {
                networkController = FindObjectOfType<NetworkController>();
            }
            
            networkController.NetworkSpawn(objectPrefab.name, transform.position + Vector3.up * 2, Quaternion.identity, Vector3.zero);
            timer = 0f;
        }
    }

    public void Restart()
    {
        if(transform.childCount != 0)
        {
            networkController.NetworkDestroy(transform.GetChild(0).gameObject, 0f);
        }
    }
}
