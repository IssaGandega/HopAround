using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLauncher : MonoBehaviour
{
    [SerializeField] private AudioClip winSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.StopMusic();
            SoundManager.instance.PlaySound(winSound);
            other.GetComponent<Player>().rotateMaxValue = 0;
            
            PlayerAnimatorManager.instance.gameObject.SetActive(false);
            LVLManager.instance.Win();
        }
    }
}
