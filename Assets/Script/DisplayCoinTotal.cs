using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCoinTotal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    private void OnEnable()
    {
        UpdateCoinTotalTxt();
    }

    public void UpdateCoinTotalTxt()
    {
        txt.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
