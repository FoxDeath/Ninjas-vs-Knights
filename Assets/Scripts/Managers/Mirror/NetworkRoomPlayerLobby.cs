using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Mirror.Discovery;
using TMPro;

//The player instance when in the lobby. It holds all the lobby UI as well
public class NetworkRoomPlayerLobby : NetworkBehaviour
{
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[4];
    [SerializeField] private Button startGameButton = null;

    [SerializeField] GameObject ninjaPlayerPrefab;
    [SerializeField] GameObject knightPlayerPrefab;

    public GameObject selectedPlayerPrefab;

    [SerializeField] TMP_ColorGradient activeGradien;
    [SerializeField] TMP_ColorGradient inactiveGradien;



    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";

    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    private bool isLeader;
    public bool IsLeader
    {
        set
        {
            isLeader = value;
            startGameButton.gameObject.SetActive(value);
        }
    }

    private NetworkManagerLobby room;
    private NetworkManagerLobby Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }
    
    //Sets the name it should display on the server
    public override void OnStartAuthority()
    {
        CmdSetDisplayName(PlayerNameInput.DisplayName);
        lobbyUI.SetActive(true);
    }

    //Adds the player to the lobby player list and updates the UI
    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);
        UpdateDisplay();
    }

    public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();
    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();

    //Updates the UI of the local player
    public void UpdateDisplay()
    {
        if(!hasAuthority)
        {
            foreach(var player in Room.RoomPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }
            return;
        }

        for(int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting For Player...";
            playerReadyTexts[i].text = string.Empty;
        }

        for(int i = 0; i < Room.RoomPlayers.Count; i++)
        {
            playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName;
            playerReadyTexts[i].text = Room.RoomPlayers[i].IsReady ?
            "<color=green>Ready</color>" :
            "<color=red>Not Ready</color>";
        }
    }
    public void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader) { return; }
        startGameButton.interactable = readyToStart;

        if(readyToStart)
        {
            startGameButton.GetComponentInChildren<TextMeshProUGUI>().colorGradientPreset = activeGradien;
        }
        else
        {
            startGameButton.GetComponentInChildren<TextMeshProUGUI>().colorGradientPreset = inactiveGradien;
        }
    }

    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }

    [Command]
    public void CmdReadyUp()
    {
        IsReady = !IsReady;

        Room.NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdStartGame()
    {
        if (Room.RoomPlayers[0].connectionToClient != connectionToClient) { return; }
        Room.StartGame();
    }

    [Command]
    public void CmdSetPlayerNinja()
    {
        selectedPlayerPrefab = ninjaPlayerPrefab;

        Room.NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdSetPlayerKnight()
    {
        selectedPlayerPrefab = knightPlayerPrefab;

        Room.NotifyPlayersOfReadyState();
    }

    //If the disconect button is pressed then the client disconects or if the palyer is the host it disconects
    // all the clients, stops the network discovery from showing the server and then it stops the server
    public void Disconnect()
    {
        if (isLeader)
        {
            FindObjectOfType<NetworkDiscovery>().StopDiscovery();
            room.StopHost();
        }
        else
        {
            room.StopClient();
        }
    }
}
