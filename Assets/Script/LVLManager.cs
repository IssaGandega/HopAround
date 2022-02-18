using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVLManager : MonoBehaviour
{
    public static LVLManager instance;
    public int currentToad;
    public GameObject winCanvas;

    public void Start()
    {
        instance = this;
    }

    public void AddToad()
    {
        currentToad++;
    }

    public void Win()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"Clear",1);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"Toad",currentToad);
        winCanvas.SetActive(true);
    }
}
