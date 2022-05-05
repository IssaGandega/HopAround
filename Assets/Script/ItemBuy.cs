using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemBuy : MonoBehaviour
{
    [SerializeField] private int itemNo;
    [SerializeField] private int price;
    [SerializeField] private GameObject canvasConfirmBuy;
    [SerializeField] private ItemLoader loader;
    [SerializeField] private Sprite itemBoughtSprite;


    public void OpenConfirmWindow()
    {
        canvasConfirmBuy.SetActive(true);
        loader.SetItem(price,itemNo);
        canvasConfirmBuy.transform.localScale = Vector3.zero;
        canvasConfirmBuy.transform.DOScale(1, 0.5f);
    }   
}
