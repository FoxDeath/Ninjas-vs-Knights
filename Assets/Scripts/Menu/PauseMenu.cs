using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//TO DO: Add to Menu Manager
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private GameObject pauseMenuUI;
    private GameObject gameUI;
    private GameObject optionsUI;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        pauseMenuUI = GameObject.Find("UI").transform.Find("PauseMenu").gameObject;
        optionsUI = GameObject.Find("UI").transform.Find("OptionsMenu").gameObject;

        if (GameObject.Find("UI").transform.Find("NinjaUI") != null)
        {
            gameUI = GameObject.Find("UI").transform.Find("NinjaUI").gameObject;
        }
        else
        {
            gameUI = GameObject.Find("UI").transform.Find("KnightUI").gameObject;
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
        audioManager.StopAll();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

    public void MainMenu()
    {
        Loader.Load(Loader.Scene.MainMenu);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
