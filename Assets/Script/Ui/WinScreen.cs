using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject[] stars;

    private void OnEnable()
    {
        for (int y = 0; y < PlayerPrefs.GetInt(SceneManager.GetActiveScene().name+"Toad"); y++)
        {
            stars[y].SetActive(true);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
