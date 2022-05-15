using UnityEngine;
using UnityEngine.UI;

public class ItemBuy : MonoBehaviour
{
    [SerializeField] private int itemNo;
    [SerializeField] private int price;
    [SerializeField] private Sprite itemBoughtSprite;
    [SerializeField] private Image bgSpriteRenderer;
    [SerializeField] private GameObject priceText;

    
    
    private void OnEnable()
    {
        PlayerPrefs.SetInt("Item"+1,1);

        if (PlayerPrefs.GetInt("Item"+itemNo) == 1 )
        {
            ObjectBoughtUIUpdate();
        }
    }

    public void Buy()
    {
        if (price < PlayerPrefs.GetInt("Coins"))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins")-price);
            PlayerPrefs.SetInt("Item"+itemNo, 1);
            ObjectBoughtUIUpdate();
        }
    }

    private void ObjectBoughtUIUpdate()
    {
        bgSpriteRenderer.sprite = itemBoughtSprite;
        priceText.SetActive(false);
    }
}
