using Mirror;
using UnityEngine;
using System.Collections;

public class NetworkController : NetworkBehaviour
{
    private GameObject[] spawnableObjects;

    private void Awake() 
    {
        spawnableObjects = FindObjectOfType<NetworkManager>().spawnPrefabs.ToArray();
    }

    public void NetworkSpawn(string name, Vector3 possition, Quaternion rotation, Vector3 velocity, float time = 0)
    {
        if(this.isLocalPlayer)
        {
            CmdSpawn(name, possition, rotation, velocity, time);
        }
    }

    [Command]
    private void CmdSpawn(string name, Vector3 possition, Quaternion rotation, Vector3 velocity, float time)
    {
        Spawn(name, possition, rotation, velocity, time);
    }

    private void Spawn(string name, Vector3 possition, Quaternion rotation, Vector3 velocity, float time)
    {
        GameObject temporaryObject = null;

        foreach (GameObject gameObject in spawnableObjects)
        {
            if (gameObject.name.Equals(name))
            {
                temporaryObject = gameObject;
            }
        }

        GameObject instantiateObject = Instantiate(temporaryObject, possition, rotation);

        Rigidbody rigidbody = instantiateObject.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.velocity = velocity;
        }

        NetworkServer.Spawn(instantiateObject);

        if (time > 0f)
        {
            StartCoroutine(NetworkDestroy(instantiateObject, time));
        }
    }

    public IEnumerator NetworkDestroy(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);

        if(this.isLocalPlayer)
        {
            CmdDestroy(gameObject);
        }
    }

    [Command]
    private void CmdDestroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private static void Destroy(GameObject gameObject)
    {
        NetworkServer.Destroy(gameObject);
    }
}
