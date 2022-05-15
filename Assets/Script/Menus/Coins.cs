using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(coinSound);
            LVLManager.instance.AddCoin();
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
