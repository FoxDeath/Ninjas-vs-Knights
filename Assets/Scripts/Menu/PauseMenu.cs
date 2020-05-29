using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using Mirror;


//TO DO: Add to Menu Manager
public class PauseMenu : NetworkBehaviour
{
    public bool GameIsPaused = false;
    private GameObject pauseMenuUI;
    private GameObject gameUI;
    private GameObject optionsUI;
    private GameObject keyBindUI;

    private InputActionAsset inputActions;
    private PlayerInput playerInput;
    private AudioManager audioManager;

    void Awake()
    {
        playerInput = new PlayerInput();
        audioManager = FindObjectOfType<AudioManager>();
        pauseMenuUI = transform.Find("PauseMenu").gameObject;
        optionsUI = transform.Find("OptionsMenu").gameObject;
        keyBindUI = transform.Find("RebindMenu").gameObject;
        inputActions = GetComponentInParent<UnityEngine.InputSystem.PlayerInput>().actions;


        if (transform.Find("NinjaUI") != null)
        {
            gameUI = transform.Find("NinjaUI").gameObject;
        }
        else
        {
            gameUI = transform.Find("KnightUI").gameObject;
        }
    }

    void Update()
    {
        if (GameIsPaused && inputActions.enabled)
        {
            inputActions.Disable();
        }
        else if(!GameIsPaused && !inputActions.enabled)
        {
            inputActions.Enable();
        }
    }

    public void MenuInput()
    {
        if(GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        gameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(false);
        keyBindUI.SetActive(false);
        gameUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

    public void MainMenu()
    {
        GameIsPaused = false;

        NetworkManagerLobby networkManager = FindObjectOfType<NetworkManagerLobby>();

        if(GetComponentInParent<PlayerMovement>().isServer)
        {
            networkManager.StopHost();
        }
        else if(GetComponentInParent<PlayerMovement>().isClientOnly)
        {
            networkManager.StopClient();
        }
    }
}
