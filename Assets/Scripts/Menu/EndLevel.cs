using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//TO DO: Add to UI Manager
public class EndLevel : MonoBehaviour
{
    private InputActionAsset inputActions;
    private NetworkManagerLobby networkManager;
    private WaveManager waveManager;
    private Objective objective;
    
    private void Awake()
    {
        inputActions = GetComponentInParent<UnityEngine.InputSystem.PlayerInput>().actions;
        networkManager = FindObjectOfType<NetworkManagerLobby>();
        waveManager = FindObjectOfType<WaveManager>();
        objective = FindObjectOfType<Objective>();
    }

    //Disables player inputs when UI is active
    private void OnEnable()
    {
        inputActions.Disable();
        Cursor.lockState = CursorLockMode.None;

        // if (GetComponentInParent<PlayerMovement>().isServer)
        // {
        //     transform.Find("Restart").gameObject.GetComponent<Button>().enabled = true;
        // }
        // else if (GetComponentInParent<PlayerMovement>().isClientOnly)
        // {
        //     transform.Find("Restart").gameObject.GetComponent<Button>().enabled = false;
        // }
    }

    //Enables player inputs when UI is inactive
    private void OnDisable()
    {
        inputActions.Enable();
    }

    //Stops the client or host and returns to server.
    public void MainMenu()
    {
        if (GetComponentInParent<PlayerMovement>().isServer)
        {
            networkManager.StopHost();
        }
        else if (GetComponentInParent<PlayerMovement>().isClientOnly)
        {
            networkManager.StopClient();
        }
    }

    public void Restart()
    {
        //restart wave spawner
        waveManager.Restart();
        //destroy all enemies
        if(GetComponentInParent<PlayerMovement>().isServer)
        { 
            NetworkController networkController = GetComponentInParent<NetworkController>();

            foreach(Target enemy in FindObjectsOfType<Target>())
            {
                StartCoroutine(networkController.NetworkDestroy(enemy.gameObject, 0f));
            }
        }
        //restart consumables spawners
        foreach(SpawnObject spawner in FindObjectsOfType<SpawnObject>())
        {
            spawner.Restart();
        }
        //restart objective
        objective.Restart();
        //destroy players
        
        //spawn new players
    }

    public void Spectate()
    {
        //spectate somehow and someway
    }
}
