using UnityEngine;
using Mirror;
using Mirror.Discovery;

//TO DO: Add to Menu Manager
public class MainMenu : NetworkBehaviour
{
    private NetworkManagerLobby networkManager;
    [SerializeField] GameObject landingPagePanel;
    [SerializeField] GameObject knightPrefab;
    [SerializeField] GameObject ninjaPrefab;

    void Awake() 
    {
        networkManager = FindObjectOfType<NetworkManagerLobby>();
    }

    //Creates a new server host.
    public void HostLobby()
    {
        networkManager.StartHost();
        FindObjectOfType<NetworkDiscovery>().AdvertiseServer();
        landingPagePanel.SetActive(false);
    }

    //Quits the application.
    public void Quit()
    {
        Application.Quit();
    }
}
