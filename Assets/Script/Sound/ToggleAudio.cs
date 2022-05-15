using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    public void ToggleSound()
    {
       SoundManager.instance.ToggleEffects();
       SoundManager.instance.ToggleMusic();
    }
}
