using UnityEngine;
using Mirror;
using Mirror.Discovery;
using TMPro;

//TO DO: Add to Menu Manager
public class MainMenu : NetworkBehaviour
{
    private NetworkManagerLobby networkManager;
    private TextMeshProUGUI highScoreText;

    [SerializeField] GameObject landingPagePanel;
    [SerializeField] GameObject knightPrefab;
    [SerializeField] GameObject ninjaPrefab;

    private int highScore;

    void Awake() 
    {
        networkManager = FindObjectOfType<NetworkManagerLobby>();
        highScoreText = transform.Find("StartMenu").Find("HighScore").GetComponent<TextMeshProUGUI>();
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    //Creates a new server host.
    public void HostLobby()
    {
        networkManager.StartHost();
        FindObjectOfType<NetworkDiscovery>().AdvertiseServer();
        landingPagePanel.SetActive(false);
    }

    //Creates a single player sesion
    public void SoloPlay()
    {
        networkManager.SoloHost();
        landingPagePanel.SetActive(false);
    }

    //Quits the application.
    public void Quit()
    {
        Application.Quit();
    }
}
