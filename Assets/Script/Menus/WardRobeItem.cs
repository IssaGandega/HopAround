using UnityEngine;
using UnityEngine.UI;


public class WardRobeItem : MonoBehaviour
{
    [SerializeField] private int itemID;
    [SerializeField] private Button itemButton;
    [SerializeField] private GameObject itemCover;
    [SerializeField] private GameObject Selected;
    [SerializeField] private SkinnedMeshRenderer frogDisplay;
    [SerializeField] private Material itemMaterial;
    
    void OnEnable()
    {
        if (PlayerPrefs.GetInt("SkinEquip") == itemID)
        {
            Selected.SetActive(true);
            UpdateMeshDisplay();
        }
        if (PlayerPrefs.GetInt("Item"+itemID.ToString()) == 1)
        {
            itemCover.SetActive(false);
            itemButton.interactable = true;
        }
        else
        {
            itemCover.SetActive(true);
            itemButton.interactable = false;
        }
    }

    public void EquipSkin()
    {
        PlayerPrefs.SetInt("SkinEquip",itemID);
        UpdateMeshDisplay();
    }

    private void UpdateMeshDisplay()
    {
        frogDisplay.material = itemMaterial;
    }
    
}
