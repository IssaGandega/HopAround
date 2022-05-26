using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuy : MonoBehaviour
{
    [SerializeField] private int itemNo;
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI currentCoinsText;
    [SerializeField] private GameObject priceText;
    [SerializeField] private GameObject fly;
    [SerializeField] private GameObject greenBand;

    
    
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
            currentCoinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        }
    }

    private void ObjectBoughtUIUpdate()
    {
        priceText.SetActive(false);
        fly.SetActive(false);
        greenBand.SetActive(true);

    }
}
