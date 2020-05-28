using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using Mirror.Discovery;

public class NetworkManagerLobby : NetworkManager
{
    private int minPlayers = 1;

    [SerializeField] NetworkRoomPlayerLobby roomPlayerPrefab;
    [SerializeField] NetworkGamePlayerLobby gamePlayerPrefab;
    [SerializeField] GameObject playerSpawnSystem;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnection, GameObject> OnServerReadied;
    public static event Action OnServerStopped;
    public static event Action OnClientStopped;
    
    public List<NetworkRoomPlayerLobby> RoomPlayers {get;} = new List<NetworkRoomPlayerLobby>();
    public List<GameObject> GamePlayers {get;} = new List<GameObject>();

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        bool isLeader = RoomPlayers.Count == 0;

        NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

        roomPlayerInstance.IsLeader = isLeader;

        NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if(conn.identity != null)
        {
            NetworkRoomPlayerLobby player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach(NetworkRoomPlayerLobby player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
            player.UpdateDisplay();
        }
    }

    private bool IsReadyToStart()
    {
        if(numPlayers < minPlayers)
        {
            return false;
        }

        foreach(NetworkRoomPlayerLobby player in RoomPlayers)
        {
            if(!player.IsReady || player.selectedPlayerPrefab == null)
            {
                return false;
            }
        }

        return true;
    }

    public void StartGame()
    {
        if(!IsReadyToStart())
        {
            return;
        }

        FindObjectOfType<NetworkDiscovery>().StopDiscovery();
        ServerChangeScene(Loader.Scene.Map.ToString());
    }

    public override void ServerChangeScene(string newSceneName)
    {        
        for (int i = RoomPlayers.Count - 1; i >= 0; i--)
        {
            var conn = RoomPlayers[i].connectionToClient;
            var gamePlayerInstance = Instantiate(gamePlayerPrefab);
            gamePlayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);
            gamePlayerInstance.SetPlayerPrefab(RoomPlayers[i].selectedPlayerPrefab);

            NetworkServer.Destroy(conn.identity.gameObject);

            NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject);
        }

        RoomPlayers.Clear();

        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string newSceneName)
    {
        GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);

        NetworkServer.Spawn(playerSpawnSystemInstance);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        if(conn.identity != null)
        {
            OnServerReadied?.Invoke(conn, conn.identity.gameObject.GetComponent<NetworkGamePlayerLobby>().GetPlayerPrefab());
        }
    }

    public override void OnStopClient()
    {
        RoomPlayers.Clear();
        GamePlayers.Clear();

        Loader.Load(Loader.Scene.MainMenu);

        OnClientStopped?.Invoke();
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
        GamePlayers.Clear();

        Loader.Load(Loader.Scene.MainMenu);

        OnServerStopped?.Invoke();
    }
}
