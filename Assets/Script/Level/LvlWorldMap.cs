using DG.Tweening;
using TMPro;
using UnityEngine;

public class LvlWorldMap : MonoBehaviour
{
    
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject[] toadFlag;
    [SerializeField] private LVLLoader loader;
    [SerializeField] private GameObject canvasLVLLauncher;
    [SerializeField] private GameObject lvlComplete;
    [SerializeField] private int lvlNo;
    [SerializeField] private TextMeshProUGUI lvlNoPopUp;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(gameObject.name+"Clear") == 1)
        {
            lvlComplete.SetActive(true);
            for (int y = 0; y < PlayerPrefs.GetInt(gameObject.name+"Toad"); y++)
            {
                toadFlag[y].SetActive(true);
            }
        }
    }

    public void OpenPopUp()
    {
        foreach (var go in stars)
        {
            go.SetActive(false);
        }
        for (int y = 0; y < PlayerPrefs.GetInt(gameObject.name+"Toad"); y++)
        {
            stars[y].SetActive(true);
        }

        lvlNoPopUp.text = "Level " + lvlNo;
        canvasLVLLauncher.SetActive(true);
        canvasLVLLauncher.transform.localScale = Vector3.zero;
        canvasLVLLauncher.transform.DOScale(1, 0.5f);
        loader.lvlToLoad = gameObject.name;
    }
}
