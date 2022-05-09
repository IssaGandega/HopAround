using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinDisplayerTxt : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "x " +LVLManager.instance.currentCoins;
    }
}
