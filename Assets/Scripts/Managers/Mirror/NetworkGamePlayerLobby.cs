using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

//Only used for passing the playerPrefab trough the NetworkRoomPlayerLobby to the palyer spawner
public class NetworkGamePlayerLobby : NetworkBehaviour
{
    [SyncVar]
    private string displayName = "Loading...";

    private GameObject playerPrefab;

    #region Getters and Setters

    public void SetPlayerPrefab(GameObject playerPrefab)
    {
        this.playerPrefab = playerPrefab;
    }

    public GameObject GetPlayerPrefab()
    {
        return playerPrefab;
    }

    public string GetDisplayName()
    {
        return displayName;
    }

    #endregion

    private NetworkManagerLobby room;

    private NetworkManagerLobby Room
    {
        get
        {
            if (room != null) { return room; }

            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }
}
