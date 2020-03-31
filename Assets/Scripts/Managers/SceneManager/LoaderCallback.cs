using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdated = true;

    private void Update()
    {
        if(isFirstUpdated)
        {
            isFirstUpdated = false;
            Loader.LoaderCallback();
        }
    }
}
