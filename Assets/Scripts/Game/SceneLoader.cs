﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : BaseManager<SceneLoader>
{
    public void LoadScene(string sceneName, bool isPopup = false)
    {
        LoadSceneMode mode;
        if (isPopup) mode = LoadSceneMode.Additive;
        else mode = LoadSceneMode.Single;

        SceneManager.LoadScene(sceneName, mode);
        //GameManager.Instance.IsPaused = false;     
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ClosePopup(string popupName)
    {
        SceneManager.UnloadSceneAsync(popupName);
    }
}
