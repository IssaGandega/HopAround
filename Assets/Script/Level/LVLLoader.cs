using UnityEngine;
using UnityEngine.SceneManagement;

public class LVLLoader : MonoBehaviour
{
    public string lvlToLoad;
    
    public void LevelStart()
    {
        SceneManager.LoadScene(lvlToLoad);
    }
}
