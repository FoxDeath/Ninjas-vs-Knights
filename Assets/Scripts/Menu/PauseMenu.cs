using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

//TO DO: Add to Menu Manager
public class PauseMenu : NetworkBehaviour
{
    private InputActionAsset inputActions;
    private AudioManager audioManager;
    private Health playerHealth;

    private GameObject pauseMenuUI;
    private GameObject gameUI;
    private GameObject optionsUI;
    private GameObject keyBindUI;
    private GameObject gameOverScreen;
    private GameObject deathScreen;

    public bool GameIsPaused = false;


    void Awake()
    {
        playerHealth = GetComponentInParent<Health>();
        audioManager = FindObjectOfType<AudioManager>();
        pauseMenuUI = transform.Find("PauseMenu").gameObject;
        optionsUI = transform.Find("OptionsMenu").gameObject;
        keyBindUI = transform.Find("RebindMenu").gameObject;
        gameOverScreen = transform.Find("GameOverScreen").gameObject;
        deathScreen = transform.Find("DeathScreen").gameObject;
        inputActions = GetComponentInParent<UnityEngine.InputSystem.PlayerInput>().actions;

        if(transform.Find("NinjaInGameUI") != null)
        {
            gameUI = transform.Find("NinjaInGameUI").gameObject;
        }
        else
        {
            gameUI = transform.Find("KnightInGameUI").gameObject;
        }
    }

    void Update()
    {
        if(gameOverScreen.activeInHierarchy || deathScreen.activeInHierarchy)
        {
            pauseMenuUI.SetActive(false);
            optionsUI.SetActive(false);
            keyBindUI.SetActive(false);
        }

        if(GameIsPaused && inputActions.enabled)
        {
            inputActions.Disable();
        }
        else if(!GameIsPaused && !inputActions.enabled && !playerHealth.GetDead() && FindObjectOfType<Objective>().GetHealth() > 0f)
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

    //Pauses the game.
    private void Pause()
    {
        gameUI.SetActive(false);
        pauseMenuUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }

    //Resumes the game.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(false);
        keyBindUI.SetActive(false);
        gameUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

    //Stops the host.
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
