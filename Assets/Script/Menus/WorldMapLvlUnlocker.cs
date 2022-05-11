using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapLvlUnlocker : MonoBehaviour
{
    private Button button;

    [SerializeField] int previouslvlNo;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        if (PlayerPrefs.GetInt("LVL"+(previouslvlNo)+"Clear") == 1)
        {
            button.interactable = true;
        }
    }

  
}
