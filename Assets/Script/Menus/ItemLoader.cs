using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    private int price;
    private int itemNo;

    public void SetItem(int _price, int _itemNo)
    {
        price = _price;
        itemNo = _itemNo;
    }
    public void Buy()
    {
        if (price < PlayerPrefs.GetInt("Coins"))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins")-price);
            PlayerPrefs.SetInt("Item"+itemNo, 1);
        }
    }
}
