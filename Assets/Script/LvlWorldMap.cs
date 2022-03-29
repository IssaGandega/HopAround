using DG.Tweening;
using UnityEngine;

public class LvlWorldMap : MonoBehaviour
{
    
    [SerializeField] private GameObject[] stars;
    [SerializeField] private LVLLoader loader;
    [SerializeField] private GameObject canvasLVLLauncher;
    

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
        canvasLVLLauncher.SetActive(true);
        canvasLVLLauncher.transform.localScale = Vector3.zero;
        canvasLVLLauncher.transform.DOScale(1, 0.5f);
        loader.lvlToLoad = gameObject.name;
    }
}
