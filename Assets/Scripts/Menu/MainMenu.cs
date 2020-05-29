using System.Collections;
using System.Collections.Generic;
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

    public void HostLobby()
    {
        networkManager.StartHost();
        FindObjectOfType<NetworkDiscovery>().AdvertiseServer();
        landingPagePanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
