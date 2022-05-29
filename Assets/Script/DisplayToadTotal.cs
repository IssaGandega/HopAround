using TMPro;
using UnityEngine;

public class DisplayToadTotal : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI text;
   private int totalToad;
   private void OnEnable()
   {
      for (int x = 0; x < 5; x++)
      {
         totalToad += PlayerPrefs.GetInt("LVL" + x + "Toad");
      }

      text.text = totalToad.ToString();
   }
}
