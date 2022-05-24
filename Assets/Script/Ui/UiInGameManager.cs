using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiInGameManager : MonoBehaviour
{

    public static UiInGameManager instance;

    public List<GameObject> toadsUiInGame;

    public TextMeshProUGUI textCoins;
    void Start()
    {
        instance = this;
    }

    public void UpdateCoinsTxt()
    {
        textCoins.text = "x " + LVLManager.instance.currentCoins;
    }

    public void UpdateCurrentToadImg(int color)
    {
        toadsUiInGame[color].SetActive(true);
    }
}
