using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLauncher : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            LVLManager.instance.Win();
        }
    }
}
