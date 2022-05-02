using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            LVLManager.instance.AddCoin();
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
