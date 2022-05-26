using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenChecker : MonoBehaviour
{
    [SerializeField] private GameObject lvlSelection;
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("loadFromLVL") == 1)
        {
            lvlSelection.SetActive(true);
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("loadFromLVL", 0);

        }
    }
}
