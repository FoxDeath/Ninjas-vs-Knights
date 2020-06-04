using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using Mirror.Discovery;


//Modified NetworkManages used for the lobby
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

    //Uppon players joining the server if the player is the first one then it is the 
    // leader of the lobby and it's instantiated and added to a list of lobby players
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        bool isLeader = RoomPlayers.Count == 0;

        NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

        roomPlayerInstance.IsLeader = isLeader;

        NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
    }
    
    //When a player disconects it is removed from the list and it notifies the other lobby players
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
    
    //Returns true of the player chose a class and pressed the ready button, false otherwise
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

    //Called by the start button in the lobby
    public void StartGame()
    {
        if(!IsReadyToStart())
        {
            return;
        }

        if(FindObjectOfType<NetworkDiscovery>())
        {
            FindObjectOfType<NetworkDiscovery>().StopDiscovery();
        }
        
        ServerChangeScene(Loader.Scene.Map.ToString());
    }

    //Called on the server to change the scene for all connected players
    //Is modified to create a NetworkGamePlayerLobby that passes the selectedPlayerPrefab (class)
    // trough to the player spawner
    public override void ServerChangeScene(string newSceneName)
    {        
        for(int i = RoomPlayers.Count - 1; i >= 0; i--)
        {
            var conn = RoomPlayers[i].connectionToClient;
            var gamePlayerInstance = Instantiate(gamePlayerPrefab);
            gamePlayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);
            gamePlayerInstance.SetPlayerPrefab(RoomPlayers[i].selectedPlayerPrefab);

            NetworkServer.Destroy(conn.identity.gameObject);

            NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject);
        }

        //RoomPlayers.Clear();

        base.ServerChangeScene(newSceneName);
    }

    public bool RestartGame()
    {
        NetworkManager networkManager = FindObjectOfType<NetworkManager>();
        NetworkManagerLobby networkManagerlobby = FindObjectOfType<NetworkManagerLobby>();

        networkManagerlobby.GamePlayers.Clear();
        int nextIndex = 0;

        foreach(NetworkConnectionToClient conn in NetworkServer.connections.Values)
        {
            GameObject currentPlayer = conn.identity.gameObject.GetComponent<NetworkController>().playerPrefab;
            GameObject prefab = null;

            if(currentPlayer.GetComponent<NinjaPlayerMovement>())
            {
                foreach(GameObject obj in networkManager.spawnPrefabs)
                {
                    if(obj.tag.Equals("Player"))
                    {
                        if(obj.GetComponent<NinjaPlayerInput>())
                        {
                            prefab = obj;
                        }
                    }
                }
            }
            else if(currentPlayer.GetComponent<KnightPlayerMovement>())
            {
                foreach(GameObject obj in networkManager.spawnPrefabs)
                {
                    if(obj.tag.Equals("Player"))
                    {
                        if(obj.GetComponent<KnightPlayerMovement>())
                        {
                            prefab = obj;
                        }
                    }
                }
            }

            NetworkServer.Destroy(conn.identity.gameObject);

            GameObject playerInstance = Instantiate(prefab, PlayerSpawnSystem.GetSpawnPoints()[nextIndex].position, PlayerSpawnSystem.GetSpawnPoints()[nextIndex].rotation);
            nextIndex++;

            NetworkServer.Spawn(playerInstance, conn);
            NetworkServer.ReplacePlayerForConnection(conn, playerInstance);

            networkManagerlobby.GamePlayers.Add(playerInstance);
        }
        return true;
    }

    //When the scene fully changed it creates the player spanw system
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

    //After a client stops it clears the lists of players for that client
    public override void OnStopClient()
    {
        RoomPlayers.Clear();
        GamePlayers.Clear();

        Loader.Load(Loader.Scene.MainMenu);

        OnClientStopped?.Invoke();
    }

    //After the server stops it clears the lists of players for that client
    public override void OnStopServer()
    {
        RoomPlayers.Clear();
        GamePlayers.Clear();

        Loader.Load(Loader.Scene.MainMenu);

        OnServerStopped?.Invoke();
    }
}
