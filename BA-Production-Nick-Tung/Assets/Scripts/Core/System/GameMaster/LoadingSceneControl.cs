using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneControl : SingletonMonobehavior<LoadingSceneControl>
{
    [SerializeField]
    Text loadingProgress = null;
    [SerializeField]
    Text currentLoading = null;
    [SerializeField]
    Image loadingUnfillImage = null;
    [SerializeField]
    float minLoadTime = 1f;
    [SerializeField]
    [ReadOnly]
    float curTime = 0.0f;
    private void Start()
    {
        if (currentLoading)
        {
            currentLoading.text = "Loading...";
            var unfillColor = loadingUnfillImage.color;
            unfillColor.a = 1.0f;
            loadingUnfillImage.color = unfillColor;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (curTime < minLoadTime)
        {
            if (loadingUnfillImage)
            {
                var unfillColor = loadingUnfillImage.color;
                unfillColor.a = curTime / minLoadTime;
                loadingUnfillImage.color = unfillColor;

            }
            if (loadingProgress)
            {
                loadingProgress.text = ((curTime / minLoadTime) * 100).ToString("F1") + "%";
            }
            curTime += Time.deltaTime;
        }
        else
        {
            if (loadingProgress)
            {
                loadingProgress.text = (SceneLoadingManager.GetInstance().GetLoadingProgress() * 100).ToString() + "%";
            }
            if (SceneLoadingManager.GetInstance().GetLoadingProgress() >= 1.0f)
            {
                SetLoadedText("Game");
                SceneLoadingManager.GetInstance().FinishedLoading();
            }
        }
    }

    public void SetLoadedText(string name)
    {
        if (currentLoading)
        {
            currentLoading.text = name + " loaded!!";
        }
    }
}
