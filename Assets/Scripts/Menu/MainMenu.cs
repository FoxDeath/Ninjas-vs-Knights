using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//TO DO: Add to Menu Manager
public class MainMenu : MonoBehaviour
{
    public void PlayKnightGame()
    {
        Loader.Load(Loader.Scene.KnightDemoLevel);
    }

    public void PlayNinjaGame()
    {
        Loader.Load(Loader.Scene.NinjaDemoLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
