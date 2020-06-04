using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//TO DO: Add to UI Manager
public class EndLevel : MonoBehaviour
{
    private InputActionAsset inputActions;
    private NetworkManagerLobby networkManager;
    
    private void Awake()
    {
        inputActions = GetComponentInParent<UnityEngine.InputSystem.PlayerInput>().actions;
        networkManager = FindObjectOfType<NetworkManagerLobby>();
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
        //restart the game somehow
    }
}
