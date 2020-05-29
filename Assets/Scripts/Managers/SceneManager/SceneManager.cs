using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private class LoadingMonoBehaviour : MonoBehaviour {}

    private static AsyncOperation loadingAsyncOperation;

    public enum Scene 
    {
        MainMenu,
        Loading,
        KnightDemoLevel,
        NinjaDemoLevel,
        Map
    }

    private static Action onLoaderCallback;

    public static bool loadingFinished;

    public static void Load(Scene scene)
    {
        //Set the loader callback action to load target scene
        onLoaderCallback = () => {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        };

        //Load loading scene
        if(scene != Scene.Loading)
        {
            SceneManager.LoadScene(Scene.Loading.ToString());
        }
    }

    //Loads the scene asynchronously inside a coroutine 
    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;
        loadingFinished = false;

        while(!loadingFinished)
        {
            yield return null;
        }

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while(!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    //Returns the progress for loading the scene
    public static float GetLoadingProgress()
    {
        if(loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1f;  
        }
    }

    public static void LoaderCallback()
    {
        //Triggered after the first update which lets the scene refresh
        //Execute the loader callback action which will load the target scene
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
