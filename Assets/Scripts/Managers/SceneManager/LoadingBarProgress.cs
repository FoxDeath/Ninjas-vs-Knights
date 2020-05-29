using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarProgress : MonoBehaviour
{
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (image.fillAmount < Loader.GetLoadingProgress())
        {
            image.fillAmount += Loader.GetLoadingProgress();
        }
        else if(image.fillAmount == 1f)
        {
            Loader.loadingFinished = true;
        }
    }
}
