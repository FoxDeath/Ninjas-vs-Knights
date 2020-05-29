using Mirror;
using Mirror.Discovery;
using UnityEngine;
using System.Collections.Generic;

//Displays a list of LAN servers
public class ServerList : MonoBehaviour
{
    private NetworkDiscovery networkDiscovery;

    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();

    void Awake()
    {
        networkDiscovery = FindObjectOfType<NetworkDiscovery>();
    }

    void OnEnable()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(transform.position.x, transform.position.y, 215, 9999));
        DrawGUI();
        GUILayout.EndArea();
    }

    void DrawGUI()
    {
        GUILayout.BeginScrollView(transform.position);

        foreach (ServerResponse info in discoveredServers.Values)
            if (GUILayout.Button(info.EndPoint.Address.ToString()))
                Connect(info);

        GUILayout.EndScrollView();
    }

    //After clicking one of the servers on the list you are connected to it
    void Connect(ServerResponse info)
    {
        transform.parent.gameObject.SetActive(false);
        NetworkManager.singleton.StartClient(info.uri);
    }

    public void OnDiscoveredServer(ServerResponse info)
    {
        discoveredServers[info.serverId] = info;
    }

    public void RefreshList()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
    }
}
