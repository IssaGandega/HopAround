using UnityEngine;
using UnityEngine.UI;


public class WardRobeItem : MonoBehaviour
{
    [SerializeField] private int itemID;
    [SerializeField] private Button itemButton;
    [SerializeField] private Image itemImage;
    
    void OnEnable()
    {
        if (PlayerPrefs.GetInt("Item"+itemID.ToString()) == 1)
        {
            itemImage.color = Color.white;
            itemButton.interactable = true;
        }
        else
        {
            itemImage.color = Color.black;
            itemButton.interactable = false;
        }
    }
}
